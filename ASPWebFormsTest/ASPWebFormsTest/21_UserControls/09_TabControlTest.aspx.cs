using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _09_TabControlTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TabControlTest.Tabs.Add("Home");
            TabControlTest.Tabs.Add("Services");
            TabControlTest.Tabs.Add("Products");
            TabControlTest.Tabs.Add("Contact");
            TabControlTest.Tabs.Add("About");
        }

        protected void TabControl_OnSelectionChanged(object sender, _09_SelectionChangedEventArgs e)
        {
            LabelMsg.Text = "Selected tab position " + e.Position;
        }
    }
}