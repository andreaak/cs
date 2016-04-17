using System.Windows.Forms;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    class FontItalicSwitch : Widget
    {
        public RadioButton ItalicBtn { get; private set; }

        public FontItalicSwitch(RadioButton italicBtn, FontDialogDirector director)
            : base(director)
        {
            ItalicBtn = italicBtn;
        }
    }
}
