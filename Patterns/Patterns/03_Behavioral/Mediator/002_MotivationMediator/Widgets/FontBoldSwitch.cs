using System.Windows.Forms;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    class FontBoldSwitch : Widget
    {
        public RadioButton BoldBtn { get; private set; }

        public FontBoldSwitch(RadioButton boldBtn, FontDialogDirector director)
            : base(director)
        {
            BoldBtn = boldBtn;
        }
    }
}
