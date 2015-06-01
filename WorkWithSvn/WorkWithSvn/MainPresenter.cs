using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithSvn
{
    public class MainPresenter
    {
        IMainView view;
        
        public MainPresenter(IMainView view)
        {
            this.view = view;
        }
    }
}
