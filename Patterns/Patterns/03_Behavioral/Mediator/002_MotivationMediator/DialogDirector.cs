using System;
using Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator
{
    abstract class DialogDirector
    {
        public void ShowDialog()
        {
            throw new InvalidOperationException();
        }
        public abstract void WidgetChanged(Widget wdgt);

        protected abstract void CreateWidgets();
    }
}
