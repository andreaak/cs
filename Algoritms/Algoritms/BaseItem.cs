using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    [Serializable]
    public enum DataType
    {
        method, 
        branch,
        branchCondition,
        branchElse,
        cycle, 
        //aux, 
        property,
        variable

    }
    
    [Serializable]
    public enum Language
    {
        CS, CPP, Java
    }

    [Serializable]
    public abstract class BaseItem
    {
        public static string root = "Start";
        public static int idCount = -1;
        public static Dictionary<Language, string> langCollection = new Dictionary<Language, string>();
        
#region PROPERTIES

        protected List<string> namespace_;
        public List<string> Namespace_
        {
            get { return namespace_; }
            set { namespace_ = value; }
        }

        public string NamespaceStr
        {
            set 
            {
                ParseNamespace(value, ref namespace_);
            }
            get
            {
                if (namespace_ == null || namespace_.Count == 0)
                {
                    return "";
                }
                string temp = null;

                foreach (string item in namespace_)
                {
                    temp += (item.Trim() + '.');
                }
                return temp.TrimEnd('.');
            }
        }

        protected DataType itemType = DataType.method;
        public DataType Type_
        {
            get { return itemType; }
            set { itemType = value; }
        }

        string language;
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        protected string class_;
        public string Class_
        {
            get { return class_; }
            set { class_ = value; }
        }

        protected bool static_;
        public bool Static_
        {
            get { return static_; }
            set { static_ = value; }
        }

        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        protected string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private int startLine;
        public int StartLine
        {
            get { return startLine; }
            set { startLine = value; }
        }

        private int endLine;
        public int EndLine
        {
            get { return endLine; }
            set { endLine = value; }
        }

        private int currentLine;
        public int CurrentLine
        {
            get { return currentLine; }
            set { currentLine = value; }
        }
        
        protected List<BaseItem> items;
        public List<BaseItem> Items
        {
            get { return items; }
        }

        private BaseItem parent;
        public BaseItem Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private int top;
        public int Top
        {
            get { return (int)(top * scaleFactor); }
            set { top = value; }
        }

        private int left;
        public int Left
        {
            get { return (int)(left * scaleFactor); }
            set { left = value; }
        }

        private static double scaleFactor = 1;
        public static double ScaleFactor
        {
            get { return scaleFactor; }
            set { scaleFactor = value; }
        }

        public virtual int Width { get { return 0; } }

        public virtual int Height { get { return 0; } }

        public virtual bool Drawable { get { return false; } }

#endregion
        static BaseItem()
        {
            langCollection = new Dictionary<Language, string>();
            langCollection.Add(Algoritms.Language.CS, @"C#");
            langCollection.Add(Algoritms.Language.Java, "Java");
            langCollection.Add(Algoritms.Language.CPP, "C++");    
        }

        public BaseItem()
        {
            this.Id = idCount++;
            items = new List<BaseItem>();
            parent = null;
        }

        protected void SetAsRoot()
        {
            this.name = root;
            idCount = -1;
            this.Id = idCount++;
        }

        public BaseItem(string namespaceStr, string class_, string name, DataType itemType, bool static_, string language,
            int startLine, int endLine, int currentLine, string fileName, BaseItem parent)
        {
            ParseNamespace(namespaceStr, ref this.namespace_);
            this.class_ = class_;
            this.name = name;
            this.itemType = itemType;
            
            this.static_ = static_;
            this.language = language;
            this.id = idCount++;
            this.itemType = itemType;

            this.startLine = startLine;
            this.endLine = endLine;
            this.currentLine = currentLine;
            this.fileName = fileName;
            
            items = new List<BaseItem>();
            this.parent = parent;
        }


        #region STATE
        
        public bool IsStartOfItem
        {
            get
            {
                if (startLine == currentLine)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsEndOfItem
        {
            get
            {
                if (endLine == currentLine)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsRoot()
        {
            if (Id == -1)
            {
                return true;
            }
            return false;
        }

        #endregion

        public void AddItem(BaseItem item)
        {
            item.Parent = this;
            items.Add(item);
        }

        public void SetItems(List<BaseItem> temp)
        {
            items = temp;
        }

        public virtual void Copy(List<BaseItem> temp)
        {
            items.Clear();
            foreach (BaseItem item in temp)
            {
                items.Add(item);
            }
        } 

        public void RemoveItem(BaseItem item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }

        #region PARSER

        public static string[] ParseItemName(string allData, ref string itemName)
        {
            if (string.IsNullOrEmpty(allData))
            {
                return null;
            }

            char[] spl = { '.' };
            string[] temp = allData.Trim().Split(spl, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length == 0)
            {
                return null;
            }
            itemName = temp[temp.Length - 1].Trim();

            string[] ret = new string[temp.Length - 1];
            for (int j = 0; j < ret.Length; j++)
            {
                ret[j] = temp[j];
            }
            return ret;
        }

        public static void ParseNamespace(string namespaceStr, ref List<string> namespaceTemp)
        {
            namespaceTemp = new List<string>();
            if (string.IsNullOrEmpty(namespaceStr))
            {
                return;
            }
            string[] temp = namespaceStr.Trim().Split(',');

            for (int j = 0; j < temp.Length; j++)
            {
                string tempStr = temp[j].Trim();
                if (tempStr.Equals(string.Empty))
                {
                    continue;
                }
                namespaceTemp.Add(tempStr);
            }
        }

        public static void ParseNamespaceAndClass(string[] namespace_class, ref List<string> namespaceTemp, ref string class_)
        {
            if (namespace_class == null || namespace_class.Length == 0)
            {
                return;
            }
            class_ = namespace_class[namespace_class.Length - 1];

            if (namespace_class.Length == 1)
            {
                return;
            }

            namespaceTemp = new List<string>();
            for (int j = 0; j < namespace_class.Length - 1; j++)
            {
                string tempStr = namespace_class[j].Trim();
                if (tempStr.Equals(string.Empty))
                {
                    continue;
                }
                namespaceTemp.Add(tempStr);
            }
        }
 
        #endregion

        public string GetKey()
        {
            return Id.ToString();
        }

        #region ID

        public virtual BaseItem GetItemById(string id)
        {
            if (id.Equals(this.Id.ToString()))
            {
                return this;
            }
            foreach (BaseItem item in items)
            {
                BaseItem temp = item.GetItemById(id);
                if (temp != null)
                {
                    return temp;
                }
            }
            return null;
        }

        public bool RemoveItemById(string id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Id.ToString() == id)
                {
                    Items.RemoveAt(i);
                    return true;
                }
                if (Items[i].RemoveItemById(id))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetMaxId()
        {
            int max = Id;
            foreach (BaseItem item in items)
            {
                int temp = item.GetMaxId();
                if (temp > max)
                {
                    max = temp;
                }
            }
            return max;
        }


        public static int GetMaxId(List<BaseItem> methods)
        {
            int max = 0;
            foreach (BaseItem item in methods)
            {
                int temp = item.GetMaxId();
                if (temp > max)
                {
                    max = temp;
                }
            }
            return ++max;
        }

        #endregion


        public virtual string GetParameters()
        {
            return "";
        }

        public abstract string GetDescription();

        public abstract void SetParam(string param);

        public void ClearParent()
        {
            this.parent = null;
            foreach (BaseItem item in items)
            {
                item.ClearParent();
            }
        }

        public void SetParent(BaseItem parent)
        {
            this.parent = parent;
            foreach (BaseItem item in items)
            {
                item.SetParent(this);
            }
        }
        
    }
}
