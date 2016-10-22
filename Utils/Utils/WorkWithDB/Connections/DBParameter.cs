using System.Data;

namespace Utils.WorkWithDB.Connections
{
    public class DBParameter
    {
        public string Name
        {
            get;
            private set;
        }

        public DbType Type
        {
            get;
            private set;
        }

        public object Value
        {
            get;
            private set;
        }

        public ParameterDirection Direction
        {
            get;
            private set;
        }

        public DBParameter(string name, DbType type, object value, 
            ParameterDirection direction = ParameterDirection.Input)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
            this.Direction = direction;
        }
    }
}
