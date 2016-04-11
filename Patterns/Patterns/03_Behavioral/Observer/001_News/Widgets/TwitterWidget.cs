using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer._001_News.News;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer._001_News.Widgets
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
