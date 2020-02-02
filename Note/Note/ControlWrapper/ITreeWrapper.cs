using System;
using System.Collections.Generic;
using Note.Domain.Common;

namespace Note.ControlWrapper
{
    public interface ITreeWrapper
    {
        int SelectedNodeId
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
        int GetParentId(bool isChildNode);

        //void DataSource(BindingDataset.DescriptionDataTable table);
        void SetDataSource();

        void Insert(int id, int parentId, string description, DataTypes type);

        void Delete(int id);

        void ChangeNodeLocation(Func<int, int, Direction, bool> IsValidAction,
            Func<int, int, int, Direction, bool> PerformAction, Direction dir);
        
        void FocusParentNode();

        void FocusNode(int id);
    }
}
