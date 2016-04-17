using System.Windows.Forms;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    class FontCondensedIndicator : Widget
    {
        public CheckBox CondensedIndicator { get; private set; }

        public FontCondensedIndicator(CheckBox condensedIndicator, FontDialogDirector director)
            : base(director)
        {
            CondensedIndicator = condensedIndicator;
        }
    }
}
