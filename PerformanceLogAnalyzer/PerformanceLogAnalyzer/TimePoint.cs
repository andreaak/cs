using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceLogAnalyzer
{
    public class TimePoint
    {
        public string Message
        {
            get;
            private set;
        }

        public DateTime Date
        {
            get;
            private set;
        }

        public bool IsBackground
        {
            get;
            private set;
        }

        public bool IsCritical
        {
            get;
            private set;
        }

        public TimePoint(DateTime dt, string message, bool isBackground, bool isCritical)
        {
            Date = dt;
            Message = message;
            IsBackground = isBackground;
            IsCritical = isCritical;
        }
    }
}
