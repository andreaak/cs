using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer.Example1.News;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer.Example1.Widgets
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
