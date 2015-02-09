using System;
using System.Collections.Generic;
using System.Text;

namespace DBLinks
{
    [Serializable]
    public class Field : IComparable
    {
        public delegate void DeleteField(Field field);
        public event DeleteField Delete;
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        List<Link> links;
        public List<Link> Links
        {
            get { return links; }
            set { links = value; }
        }

        bool isPrimaryKey;
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        bool isForeignKey;
        public bool IsForeignKey
        {
            get { return isForeignKey; }
            set { isForeignKey = value; }
        }

        public Field(string name)
        {
            this.name = name;
            links = new List<Link>();
        }

        public Field(string name, string type)
            : this(name)
        {
            this.type = type;
        }  
  
        public void AddLink(Link link)
        {
            if(!links.Contains(link))
            {
                links.Add(link);    
            }
        }

        public void RemoveLink(Link link)
        {
            if (links.Contains(link))
            {
                links.Remove(link);
                if (links.Count == 0)
                {
                    if (Delete != null)
                    { 
                        Delete(this);
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            Field fieldComp = obj as Field;
            if (fieldComp == null || fieldComp.name != this.name || fieldComp.type != this.type || !links.Equals(fieldComp.Links))
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}", name, type);
        }

        public bool Contains(Link link)
        {
            foreach (Link item in links)
            {
                if (link.TableName == item.TableName && link.FieldName == item.FieldName)
                {
                    return true;
                }
            }
            return false;
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            Field fieldComp = obj as Field;
            if (fieldComp == null)
            {
                return -1;
            }
            return this.Name.CompareTo(fieldComp.Name);
        }
        #endregion
    }
}
