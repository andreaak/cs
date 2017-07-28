using System;

namespace ASPWebFormsTest._02_Page._03_PageEventHandling
{
    public partial class _01_EventHandlingWireup : System.Web.UI.Page
    {
        /*
        При наличии атрибута AutoEventWireup="true" директивы @Page, 
        методы с именем Page_ИмяСобытия, автоматически становятся обработчиками событий страницы.
        */
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Сработал обработчик события Load";
        }
    }
}