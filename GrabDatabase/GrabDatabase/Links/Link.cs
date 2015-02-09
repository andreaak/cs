using System;
using System.Collections.Generic;
using System.Text;

namespace DBLinks
{
    [Serializable]
    public class Link
    {
        string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        string fieldName;
        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        string fieldType;
        public string FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        public Link(string tableName, string fieldName)
        {
            this.tableName = tableName;
            this.fieldName = fieldName;
        }
        public Link(string tableName, string fieldName, string fieldType): this(tableName, fieldName)
        {
            this.fieldType = fieldType;
        }
        
        public override bool  Equals(object obj)
        {
 	        Link linkComp = obj as Link;
            if (linkComp == null || linkComp.tableName != this.tableName || linkComp.fieldName != this.fieldName || linkComp.fieldType != this.fieldType)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", tableName, fieldName);
        }
    }
}
