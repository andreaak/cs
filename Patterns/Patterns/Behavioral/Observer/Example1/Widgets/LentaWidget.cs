using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer.Example1.News;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer.Example1.Widgets
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
