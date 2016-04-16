using Patterns._02_Structural.Adapter._001_Cats.HomeCats;
using Patterns._02_Structural.Adapter._001_Cats.WildCats;

namespace Patterns._02_Structural.Adapter._001_Cats.Adapters
{
    class HomeCatAdapter : IHomeCat
    {
        private IWildCat _wildCat;

        public HomeCatAdapter(IWildCat wildCat)
        {
            _wildCat = wildCat;
        }

        public string Name
        {
            get { return _wildCat.Breed; } 
            set { }
        }

        public void Meow()
        {
            _wildCat.Growl();
        }

        public void Scratch()
        {
            _wildCat.Scratch();
        }
    }
}
