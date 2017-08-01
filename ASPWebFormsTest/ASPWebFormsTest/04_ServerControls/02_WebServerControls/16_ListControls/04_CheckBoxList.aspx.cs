using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls
{
    public partial class _04_CheckBoxList : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    message += item.Text + "<br />";
                }
            }
            OutputLabel.Text = message;
        }
    }
}