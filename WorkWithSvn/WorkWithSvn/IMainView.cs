using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithSvn
{
    public interface IMainView
    {
        Providers.ControlsData ControlsData { get; }
    }
}
