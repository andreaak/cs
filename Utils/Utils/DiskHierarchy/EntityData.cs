using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public class EntityData
    {
        public static long idCount = 0;
        public const byte FILE_TYPE = 1;
        public const byte DIR_TYPE = 0;

        protected long id;
        public long Id
        {
            get { return id; }
            //set { id = value; }
        }

        protected long parentId;
        public long ParentId
        {
            get { return parentId; }
            //set { parentId = value; }
        }

        protected string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        protected string path;
        public string Path_
        {
            get { return path; }
            //set { fileName = value; }
        }

        protected string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set { fullPath = value; }
        }

        public EntityData(string fullPath, string name, string path, long parentId)
        {
            this.fullPath = fullPath;
            this.name = Path.GetFileName(fullPath);
            this.path = Path.GetDirectoryName(fullPath);
            this.id = EntityData.idCount++;
            this.parentId = parentId;
        }
        

    }
}
