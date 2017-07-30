using System;

namespace ASPWebFormsTest._03_StateSaving._05_Session
{
    public partial class _01_SessionWrite : System.Web.UI.Page
    {
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Запись значения в сессию.
            Session["Key"] = TextBox1.Text;

            // Значение будет храниться в памяти сервера 1 минуту. (по умолчанию значение храниться 20 минут)
            Session.Timeout = 1;
        }
    }
}