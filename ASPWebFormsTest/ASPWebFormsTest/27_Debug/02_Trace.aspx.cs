using System;
using System.Threading;

namespace ASPWebFormsTest._27_Debug
{
    public partial class _02_Trace : System.Web.UI.Page
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