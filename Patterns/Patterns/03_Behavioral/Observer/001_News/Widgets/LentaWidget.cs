using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer._001_News.News;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer._001_News.Widgets
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
