namespace Utils.WorkWithFiles
{
    interface IWriter
    {
        void WriteLine(object data);
        void Write(object data);
        void Close();
    }
}
