using System;

namespace ASPWebFormsTest._03_StateSaving._05_Session
{
    public partial class _03_SessionRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string text = Session["Key"] as string;
            if (text != null)
            {
                OutputLabel.Text = text;
            }
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            //Session.Clear(); // Удаляет все значения из объекта коллекции.
            Session.Abandon(); // Удаляет объект коллекции.
            Response.Redirect(Request.Url.PathAndQuery);
        }
    }
}