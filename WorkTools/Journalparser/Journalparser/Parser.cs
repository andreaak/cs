using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalparser
{
    public class DateItem
    {
        public DateTime Date;
        public int Line;
    }

    public class FreezedItem
    {
        public DateTime Start;
        public int LineStart;
        public DateTime End;
        public int LineEnd;
        public int Time;
        public string Text;
    }

    public class Parser
    {
        public static List<FreezedItem> points;

        public static List<FreezedItem> Parse(string file, int minDuration)
        {
            points = new List<FreezedItem>();
            int index = 1;
            using (StreamReader rd = File.OpenText(file))
            {
                string line = null;
                var strings = new List<string>();
                DateItem prevItem = null;
                while ((line = rd.ReadLine()) != null)
                {
                    var item = ParseLine(line, index++);
                    if(item != null)
                    {
                        if (prevItem != null && IsFiltered(minDuration, prevItem, item))
                        {
                            strings.Add(line);
                            points.Add(new FreezedItem {

                                Start = prevItem.Date,
                                LineStart = prevItem.Line,
                                End = item.Date,
                                LineEnd = item.Line,
                                Time = (int)((item.Date - prevItem.Date).TotalSeconds),
                                Text = string.Join("\n", strings)
                            });
                        }

                        //
                        prevItem = item;
                        strings.Clear();
                        strings.Add(line);
                    }
                    else
                    {
                        strings.Add(line);
                    }
                }
            }
            return points;
        }

        private static bool IsFiltered(int minDuration, DateItem prevItem, DateItem item)
        {
            var diff = GetInterval(prevItem, item);
            return diff >= minDuration;
        }

        private static int GetInterval(DateItem prevItem, DateItem item)
        {
            return (int)((item.Date - prevItem.Date).TotalSeconds);
        }

        private static DateItem ParseLine(string line, int index)
        {
            var items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(items.Length == 0)
            {
                return null;
            }
            string str = (items.ElementAtOrDefault(1) + " " + items.ElementAtOrDefault(2)).TrimEnd(';');
            return DateTime.TryParse(str, out DateTime time) && items[0].StartsWith("'")?
                            new DateItem { Date = time, Line = index }:
                            null;
        }


    }
}
