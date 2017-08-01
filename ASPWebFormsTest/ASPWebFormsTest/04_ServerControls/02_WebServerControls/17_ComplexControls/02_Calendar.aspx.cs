using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls
{
    public partial class _02_Calendar : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            DateTime dateTime = Calendar1.SelectedDate;
            OutputLabel.Text += "Months = " + dateTime.Month;
            OutputLabel.Text += "<br />Date = " + dateTime.Day;
        }
    }
}