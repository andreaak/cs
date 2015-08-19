using DataManager;
using Note;

namespace ControlWrapper
{
    public interface IWrapper
    {
        NoteDataManager DataManager { set; }
        
        long SelectedNodeId { get; }
        long Position { get; }
        long ParentId { get; }
        
        string RtfText { get; set; }
        string TextData { get;}
        string Data { get; set; }

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
        string GetConvertedData(string data, DocTypes inType, DocTypes outType);
        
        bool IsNodeSelect();
        bool IsNoteNode();
        void FocusSelectedNode();
        long GetParentId(bool isChildNode);

        void LoadEntityFromDB();
        void SaveNodesDataToFile();

        void InitHandlers();

        void FocusParentNode();

        void UpdateBinding();
    }
}
