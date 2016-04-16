namespace Patterns._02_Structural.Adapter._001_Cats.WildCats
{
    interface IWildCat
    {
        string Breed { get; }
        void Growl();
        void Scratch();
    }
}
