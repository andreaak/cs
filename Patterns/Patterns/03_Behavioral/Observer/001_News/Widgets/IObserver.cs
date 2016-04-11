using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Observer._001_News.Widgets
{
    public interface IObserver
    {
        void Update(string twitter, string lenta, string tv);
        void RemoveFromSubject();
    }
}
