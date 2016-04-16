namespace Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Locator
{
    interface IServiceLocator
    {
        T GetService<T>();
    }
}
