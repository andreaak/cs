using System;
using System.IO;

namespace Utils.DiskHierarchy
{
    [Serializable]
    public class FileItem : DiskItem, IFile 
    {
        public override int Type
        {
            get
            {
                return DiskItem.FILE_TYPE;
            }
        }
        
        public FileItem(string fullName, long parentId)
            : base(fullName, Path.GetFileName(fullName), parentId)

        {
        }
    }
}
