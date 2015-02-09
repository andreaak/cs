using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseQuery
{
    public enum ParameterType { String, Number, None };
    public class Parameter
    {
        ParameterType type;
        internal ParameterType Type
        {
            get { return type; }
            set { this.type = value; }
        }

        object value;
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Parameter(ParameterType type, object value)
        {
            this.type = type;
            this.value = value;

        }

        public Parameter(string type, object value)
        {
            if (Enum.IsDefined(typeof(ParameterType), type))
            {
                this.type = (ParameterType)Enum.Parse(typeof(ParameterType), type);

            }
            this.value = value;
        }

        public static object[] GetParameterTypes()
        {
            List<string> lst = new List<string>(Enum.GetNames(typeof(ParameterType)));
            return lst.ToArray<object>();
        }

        public override string ToString()
        {
            if (type == ParameterType.String)
            {
                return string.Format("'{0}'", value.ToString());
            }
            return value.ToString();
        }
    }
}
