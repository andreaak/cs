namespace Patterns._03_Behavioral.Memento._003_NarrowAndWideInterfaces
{
    // Широкий интерфейс
    interface IWideInterface : INarrowInterface
    {
        string State { get;  set; }
    }
}
