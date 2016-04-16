using System.Diagnostics;
using Patterns._03_Behavioral.Observer._001_News.News;

namespace Patterns._03_Behavioral.Observer._001_News.Widgets
{
    class LentaWidget : IWidget
    {
        private string _lenta;

        public void Update(object sender, NewsEventArgs e)
        {
            _lenta = e.Lenta;
            Display();
        }

        public void Display()
        {
            Debug.WriteLine("Lenta: {0}", _lenta);
        }
    }
}
