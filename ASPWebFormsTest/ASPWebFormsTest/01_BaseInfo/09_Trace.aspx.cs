using System;
using System.Threading;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _09_Trace : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            // Добавление записи в блок "Trace Information" трассировочных данных
            Trace.Write("My", "----> Start");
            Thread.Sleep(2000);
            Trace.Write("My", "----> Stop");
        }
    }
}