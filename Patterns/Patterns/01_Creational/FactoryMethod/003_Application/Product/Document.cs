
namespace Patterns._01_Creational.FactoryMethod._003_Application.Product
{
    // Product
    public abstract class Document
    {
        public abstract void Open();
        public abstract void Close();
        public abstract void SaveAs();
        public abstract void Clear();
    }
}
