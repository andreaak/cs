using System;

namespace ASPWebFormsTest._02_Page._03_PageEventHandling
{
    public partial class _03_EventHandlingOverrideHandlers : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            Label1.Text = "Сработал обработчик события Load";
        }
    }
}