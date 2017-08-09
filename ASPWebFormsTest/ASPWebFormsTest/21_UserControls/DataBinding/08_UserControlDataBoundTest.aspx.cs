using System;

namespace ASPWebFormsTest._21_UserControls.DataBinding
{
    public partial class _08_UserControlDataBoundTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _08_Person p = new _08_Person();
            p.A = "Иван";
            p.B = "Иванов";
            p.C = "Иванович";
            UserControlDataBounded1.DataItem = p;
            UserControlDataBounded1.DataBind();
        }
    }
}