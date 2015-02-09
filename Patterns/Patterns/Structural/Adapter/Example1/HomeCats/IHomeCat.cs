namespace Patterns.Structural.Adapter.Example1.HomeCats
{
    interface IHomeCat
    {
        string Name { get; set; }
        void Meow();
        void Scratch();
    }
}
