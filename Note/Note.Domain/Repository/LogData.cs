using System;

namespace Note.Domain.Repository
{
    public class LogData
    {
        public int EntityID { get; internal set; }

        public string Operation { get; internal set; }

        public string EntityDescription { get; set; }

        public DateTime ModDate { get; internal set; }
    }
}