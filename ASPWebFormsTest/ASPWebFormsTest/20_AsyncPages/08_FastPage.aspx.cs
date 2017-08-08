using System;
using System.Threading;

namespace ASPWebFormsTest._20_AsyncPages
{
    public partial class _08_FastPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("FastResponse Thread Id " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}