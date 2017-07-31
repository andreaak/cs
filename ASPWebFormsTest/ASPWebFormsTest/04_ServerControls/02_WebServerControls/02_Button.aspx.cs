using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class ButtonSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button2.BackColor = Color.Green;
            Button2.ForeColor = Color.Yellow;
            Button2.Font.Underline = true;
            Button2.Font.Bold = true;
        }

        protected void Button1_CLick(object sender, EventArgs e)
        {
            (sender as Button).Text = "Кнопка нажата";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Write("На сервере сработал обработчик события");
        }
    }
}