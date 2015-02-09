using Patterns.Structural.Adapter.Example1.HomeCats;
using Patterns.Structural.Adapter.Example1.WildCats;

namespace Patterns.Structural.Adapter.Example1.Adapters
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
