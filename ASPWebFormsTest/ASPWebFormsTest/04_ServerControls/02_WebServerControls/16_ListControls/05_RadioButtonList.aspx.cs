using System;
using System.Web.UI;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls
{
    public partial class _05_RadioButtonList : Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            OutputLabel.Text = RadioButtonList1.SelectedItem.Text;
        }
    }
}