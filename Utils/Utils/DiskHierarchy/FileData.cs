using System;
using System.IO;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public class FileData
    {
        public EntityData Data
        {
            get;
            private set;
        }
        
        public FileData(string fullPath, long parentId)
        {
            Data = new EntityData(fullPath, Path.GetFileName(fullPath), Path.GetDirectoryName(fullPath),
                parentId);
        }
    }
}
