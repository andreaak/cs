using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer._001_News.News;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer._001_News.Widgets
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
