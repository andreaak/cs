namespace Patterns.Structural.Adapter.Example1.WildCats
{
    interface IWildCat
    {
        string Breed { get; }
        void Growl();
        void Scratch();
    }
}
