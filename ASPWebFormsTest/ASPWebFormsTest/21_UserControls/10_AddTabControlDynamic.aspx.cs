using System;
using System.Drawing;
using ASPWebFormsTest._21_UserControls.Controls;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _10_AddTabControlDynamic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _09_TabControl control = LoadControl("Controls/09_TabControl.ascx") as _09_TabControl;
            control.SelectedTabBackColor = Color.Green;
            control.Tabs.Add("Products");
            control.Tabs.Add("Services");
            control.Tabs.Add("About");
            ControlPlaceHolder.Controls.Add(control);
        }
    }
}