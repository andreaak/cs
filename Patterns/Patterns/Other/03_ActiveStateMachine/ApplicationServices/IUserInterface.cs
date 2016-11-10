using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
{
    interface IUserInterface
    {
        void LoadViewState(string viewState);
    }
}
