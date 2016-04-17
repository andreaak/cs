using System.Windows.Forms;

namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    class FontSizeSelector : Widget
    {
        public DomainUpDown DomSize { get; private set; }

        public FontSizeSelector(DomainUpDown domSize, FontDialogDirector director)
            : base(director)
        {
            DomSize = domSize;
        }
    }
}
