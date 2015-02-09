using System;
using System.Collections.Generic;
using System.Text;

namespace DBLinks
{
    [Serializable]
    public class Table
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        Dictionary<string, Field> fields;
        public Dictionary<string, Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public Table(string name)
        {
            this.name = name;
            fields = new Dictionary<string, Field>();
        }

        public void AddField(Field field)
        {
            if (!fields.ContainsKey(field.Name))
            {
                field.Delete += new Field.DeleteField(DeleteField);
                fields.Add(field.Name, field);    
            }
        }

        public void RemoveField(Field field)
        {
            if (fields.ContainsKey(field.Name))
            {
                fields.Remove(field.Name);
            }
        }

        public override bool Equals(object obj)
        {
            Table tableComp = obj as Table;
            if (tableComp == null || tableComp.name != this.name || !fields.Equals(tableComp.Fields))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}", name);
        }

        private void DeleteField(Field field)
        { 
            if(fields.ContainsKey(field.Name))
            {
                fields.Remove(field.Name);
            }
        }
    }
}
