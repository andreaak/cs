using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceLogAnalyzer
{
    public  class Event
    {
        public const char SEPARATOR = '_';
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Service { get; set; }
        public string Method { get; set; }
        public int Index { get; set; }
        public string ThreadId { get; set; }
        public string DomainId { get; set; }

        public Event(DateTime dt, string method, string service, int index, string threadId, string domainId)
        {
            this.StartDate = dt;
            this.Method = method;
            this.Service = service;
            this.Index = index;
            this.ThreadId = threadId;
            this.DomainId = domainId;
        }

        public override bool Equals(object obj)
        {
            Event evt = obj as Event;
            if (evt != null)
            {
                return this.StartDate.Equals(evt.StartDate) &&
                        this.Method == evt.Method &&
                        this.Service == evt.Service;
            }
            return false;
        }

        public override string ToString()
        {
            return Service + SEPARATOR + Method + SEPARATOR + Index + SEPARATOR + ThreadId + SEPARATOR + DomainId;
        }
    }
}
