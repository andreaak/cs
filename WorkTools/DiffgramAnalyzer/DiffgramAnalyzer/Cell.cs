using System;
using System.Collections.Generic;
using System.Text;

namespace DiffgramAnalyzer
{
    public class Cell
    {
        public string name;
        public string value;
        public Cell(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
