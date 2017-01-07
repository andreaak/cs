using System.Collections.Generic;

namespace Note.ControlWrapper
{
    public class Node
    {
        public int ID
        {
            get;
            set;
        }

        public long Index
        {
            get;
            set;
        }

        public string EditValue
        {
            get;
            set;
        }

        public bool IsNote
        {
            get;
            set;
        }

        public List<Node> Nodes
        {
            get;
            set;
        }

        public int SiblingsCount
        {
            get;
            set;
        }
    }
}
