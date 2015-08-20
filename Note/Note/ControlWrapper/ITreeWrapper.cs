using System;
using System.Collections.Generic;
using DataManager.Repository;
using Note.ControlWrapper.Binding;

namespace Note.ControlWrapper
{
    public interface ITreeWrapper
    {
        long SelectedNodeId
        {
            get;
        }

        IList<Node> Nodes
        {
            get;
        }

        bool Enabled
        {
            set;
        }

        void EnableFocusing();
        void DisableFocusing();

        bool IsNodeSelect();
        bool IsNoteSelect();
        long GetParentId(bool isChildNode);

        void DataSource(BindingDataset.DescriptionDataTable table);

        void Insert(long id, long parentId, string description, DataTypes type);

        void Delete(long id);

        void ChangeNodeLocation(Func<int, long, Direction, bool> IsValidAction,
            Func<int, long, long, Direction, bool> PerformAction, Direction dir);
        
        void FocusParentNode();
    }
}
