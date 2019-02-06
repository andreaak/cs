using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceLogAnalyzer
{
    public class Parser
    {
        private const string START_ACTIVITY = "Start activity:";
        private const string END_ACTIVITY = "End activity:";
        private const string TIMEPOINT = "Timepoint:";
        private const string BACKGROUND_TIMEPOINT = "Timepoint.Background:";
        private const string CRITICAL_TIMEPOINT = "Timepoint.Critical:";
        private const int DEFAULT_TIME_RANGE = 10;
        private const char SEPARATOR = ' ';
        private static List<Event> events;
        private static List<TimePoint> points;
        private static int index;

        public static List<Event> Events { get { return events; } }
        public static List<TimePoint> Points
        {
            get
            {
                return points;
            }
        }
        
        public static void Parse(string file)
        {
            events = new List<Event>();
            points = new List<TimePoint>();
            index = 0;
            using (StreamReader rd = File.OpenText(file))
            { 
                string line = null;
                while((line = rd.ReadLine()) != null)
                {
                    ParseLine(line);
                }
            }
            ValidateEvents();
        }

        private static void ValidateEvents()
        {
            foreach (var evt in events.Where(item => item.EndDate == DateTime.MinValue))
            {
                evt.EndDate = evt.StartDate.AddMilliseconds(DEFAULT_TIME_RANGE);
            }
        }

        private static void ParseLine(string line)
        {
            if (line.Contains(START_ACTIVITY))
            {
                ParseStartActivity(line);
            }
            else if (line.Contains(END_ACTIVITY))
            {
                ParseEndActivity(line);
            }
            else if (line.Contains(TIMEPOINT))
            {
                ParseTimepoint(line);
            }
            else if (line.Contains(BACKGROUND_TIMEPOINT))
            {
                ParseTimepoint(line, isBackground: true);
            }
            else if (line.Contains(CRITICAL_TIMEPOINT))
            {
                ParseTimepoint(line, isCritical : true);
            }
        }

        private static void ParseTimepoint(string line, bool isBackground = false, bool isCritical = false)
        {
            string[] parts = line.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            string time = parts[0] + SEPARATOR + parts[1].Replace(',', '.');
            string message = string.Join(" ", parts.Skip(4));
            DateTime dt = DateTime.Parse(time);
            TimePoint point = new TimePoint(dt, message, isBackground, isCritical);
            points.Add(point);
        }

        private static void ParseStartActivity(string line)
        {
            string[] parts = line.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            string time = parts[0] + SEPARATOR + parts[1].Replace(',', '.');
            string method = parts[5];
            string service = parts[6];
            string threadId = null;
            if(parts.Length > 7)
            {
                threadId = parts[8];
            }
            string domainId = null;
            if (parts.Length > 9)
            {
                domainId = parts[10];
            }
            DateTime dt = DateTime.Parse(time);
            Event evnt = new Event(dt, method, service, index++, threadId, domainId);
            events.Add(evnt);
        }

        private static void ParseEndActivity(string line)
        {
            string[] parts = line.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            string time = parts[0] + SEPARATOR + parts[1].Replace(',', '.');
            string method = parts[5];
            string service = parts[6].TrimEnd(',');
            DateTime dt = DateTime.Parse(time);
            Event evnt = events.FirstOrDefault(item => item.Service == service && item.Method == method && item.EndDate == DateTime.MinValue);
            if (evnt != null)
            {
                evnt.EndDate = dt;
            }
        }
    }
}
