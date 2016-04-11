namespace Patterns.Structural.Adapter._001_Cats.HomeCats
{
    interface IHomeCat
    {
        string Name { get; set; }
        void Meow();
        void Scratch();
    }
}
