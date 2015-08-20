
namespace Note.ControlWrapper
{
    public interface IRtfWrapper
    {
        string PlainText
        {
            get;
        }

        string EditValue
        {
            get;
            set;
        }

        bool IsModified
        {
            get;
        }

        bool Enabled
        {
            set;
        }

        void SetDefinitionFormat();
        void SetMethodFormat();
        void SetClassFormat();
        void SetHeaderFormat();
        void SetInfoFormat();
        void SetCodeFormat();
        void RemoveWhiteSpace();
        void RemoveDoubleWhiteSpace();
        void RemoveLineBreak();

        void ChangeState(bool isNoteNode);
        void Clear();
    }
}
