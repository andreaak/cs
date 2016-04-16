using System.Diagnostics;
using Patterns._03_Behavioral.Observer._001_News.News;

namespace Patterns._03_Behavioral.Observer._001_News.Widgets
{
    class TwitterWidget : IWidget
    {
        private string _twitter;

        public void Update(object sender, NewsEventArgs e)
        {
            _twitter = e.Twitter;
            Display();
        }

        public void Display()
        {
            Debug.WriteLine("Twitter: {0}", _twitter);
        }
    }
}
