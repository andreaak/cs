using System.Diagnostics;
using Patterns._03_Behavioral.Observer._001_News.News;

namespace Patterns._03_Behavioral.Observer._001_News.Widgets
{
    class TvWidget : IWidget
    {
        private string _tv;

        public void Update(object sender, NewsEventArgs e)
        {
            _tv = e.Tv;
            Display();
        }

        public void Display()
        {
            Debug.WriteLine("TV: {0}", _tv);
        }
    }
}
