using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls
{
    public partial class _02_DropDownList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Click(object sender, EventArgs e)
        {
            WriteInfo(DropDownList1);
        }

        protected void List_IndexChanged(object sender, EventArgs e)
        {
            WriteInfo(DropDownList2);
        }

        private void WriteInfo(DropDownList list)
        {
            OutputLabel.Text += "Индекс выбранного элемента: " + list.SelectedIndex;
            OutputLabel.Text += "<br />";
            OutputLabel.Text += "Value выбранного элемента:  " + list.SelectedValue;
            //OutputLabel.Text += "Value выбранного элемента:  " + DropDownList1.SelectedItem.Value;
            OutputLabel.Text += "<br />";
            OutputLabel.Text += "Text выбранного элемента:   " + list.SelectedItem;
            //OutputLabel.Text += "Text выбранного элемента:   " + DropDownList1.SelectedItem.Text;
            OutputLabel.Text += "<br />";
        }
    }
}