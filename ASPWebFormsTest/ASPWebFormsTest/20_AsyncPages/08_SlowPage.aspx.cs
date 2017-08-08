using System;
using System.Threading;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _08_SlowPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            Response.Write("SlowResponse Thread Id " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}