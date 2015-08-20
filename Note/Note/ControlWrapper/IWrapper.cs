using DataManager;
using Note;

namespace ControlWrapper
{
    public interface IWrapper
    {
        DatabaseManager DataManager { set; }
        
        long SelectedNodeId { get; }
        int Position { get; }
        long ParentId { get; }
        
        string PlainText { get;}
        string EditValue { get; set; }

        void EnableControls(bool isEnable);
        void EnableFocusing();
        void DisableFocusing();
        
        void SetDefinitionFormat();
        void SetMethodFormat();
        void SetClassFormat();
        void SetHeaderFormat();
        void SetInfoFormat();
        void SetCodeFormat();
        void RemoveWhiteSpace();
        void RemoveDoubleWhiteSpace();
        void RemoveLineBreak();
        
        void BeginUpdateRtfControl();
        void EndUpdateRtfControl();
        
        bool IsNodeSelect();
        bool IsNoteNode();
        void FocusSelectedNode();
        long GetParentId(bool isChildNode);

        void LoadEntityFromDB();
        void Export();

        void InitHandlers();

        void FocusParentNode();

        void UpdateBinding();
    }
}
