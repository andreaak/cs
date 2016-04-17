using System.Windows.Forms;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    class MyListBox : Widget
    {
        public ListBox listBoxWidget { get; private set; }

        public MyListBox(ListBox bindedWidget, FontDialogDirector director)
            : base(director)
        {
            listBoxWidget = bindedWidget;
        }

        internal void FindFontFamily(string p)
        {
            listBoxWidget.SelectedIndex = listBoxWidget.FindString(p);
        }
    }
}
