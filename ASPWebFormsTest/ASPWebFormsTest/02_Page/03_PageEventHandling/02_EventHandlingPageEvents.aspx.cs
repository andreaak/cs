using System;

namespace ASPWebFormsTest._02_Page._03_PageEventHandling
{
    public partial class _02_EventHandlingPageEvents : System.Web.UI.Page
    {
        public _02_EventHandlingPageEvents()
        {
            this.Load += new EventHandler(EventHandling2_Load);
        }

        void EventHandling2_Load(object sender, EventArgs e)
        {
            Label1.Text = "Сработал обработчик события Load";
        }
    }
}