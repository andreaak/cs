namespace Patterns._01_Creational.FactoryMethod._010_Generic.Creators
{
    interface ICreator
    {
        T CreateProduct<T>();
    }
}
