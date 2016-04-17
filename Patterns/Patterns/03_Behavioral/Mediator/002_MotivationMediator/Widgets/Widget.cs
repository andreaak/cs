
namespace Patterns._03_Behavioral.Mediator._002_MotivationMediator.Widgets
{
    abstract class Widget
    {
        protected FontDialogDirector Director;

        public Widget(FontDialogDirector director)
        {
            Director = director;
        }

        public void Changed()
        {
            Director.WidgetChanged(this);
        }
    }
}
