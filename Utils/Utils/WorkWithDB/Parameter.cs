using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Utils.WorkWithDB
{
    public class Parameter
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        DbType type;
        public DbType Type
        {
            get { return type; }
            set { type = value; }
        }

        object value;
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        ParameterDirection direction;
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Parameter(string name, DbType type, object value, 
            ParameterDirection direction = ParameterDirection.Input)
        {
            this.name = name;
            this.type = type;
            this.value = value;
            this.direction = direction;
        }
    }
}
