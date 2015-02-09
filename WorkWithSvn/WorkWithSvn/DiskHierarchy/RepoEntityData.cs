using System;
using System.IO;
using Utils.DiskHierarchy;

namespace WorkWithSvn.DiskHierarchy
{
    [Serializable]
    public class RepoEntityData
    {
        EntityData data;
        public EntityData Data
        {
            get { return data; }
            //set { baseData = value; }
        }
        
        private string revision;
        public string Revision
        {
            get { return revision; }
            set { revision = value; }
        }

        private string changeList;
        public string ChangeList
        {
            get { return changeList; }
            set { changeList = value; }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private bool isSwitched;
        public bool IsSwitched
        {
            get { return isSwitched; }
            set { isSwitched = value; }
        }

        public RepoEntityData(EntityData data)
        {
            this.data = data;
        }

        public virtual object LocalStatus
        { 
            get; set;
        }

        public virtual string LocalStatusDesc
        {
            get; set;
        }

        public virtual object RemoteStatus
        {
            get; set;
        }

        public virtual string RemoteStatusDesc
        {
            get; set;
        }

        public void Rename(string name)
        {
            Data.Name = name;
            Data.FullPath = Path.GetDirectoryName(Data.FullPath) + Path.DirectorySeparatorChar + name;
        }

        public virtual bool IsModified { get { return LocalStatusDesc == "Modified"; } }

        public virtual bool IsAdded { get { return LocalStatusDesc == "Added"; } }

        public virtual bool IsDeleted { get { return LocalStatusDesc == "Deleted"; } }

        public virtual bool IsConflicted { get { return LocalStatusDesc == "Conflicted"; } }

        public virtual bool IsUnchanged { get { return LocalStatusDesc == "Normal"; } }

        public virtual bool IsIgnoredEntity(RepoFileTypes list)
        {
            if (IsSwitched && list.isSwitched)
            {
                return false;
            }
            return true;
        }
    }
}
