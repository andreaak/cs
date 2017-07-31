using System;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _07_CheckBox : System.Web.UI.Page
    {
        protected void Button_Click(object sender, EventArgs e)
        {
            string output = string.Empty;
            if (CheckBox1.Checked)
            {
                output += CheckBox1.Text + "<br />";
            }
            if (CheckBox2.Checked)
            {
                output += CheckBox2.Text + "<br />";
            }
            if (CheckBox3.Checked)
            {
                output += CheckBox3.Text + "<br />";
            }
            if (CheckBox4.Checked)
            {
                output += CheckBox4.Text + "<br />";
            }
            if (CheckBox5.Checked)
            {
                output += CheckBox5.Text + "<br />";
            }

            OutputLabel.Text = output;
        }

        //or
        //protected void Button_Click(object sender, EventArgs e)
        //{
        //    string output = string.Empty;
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        // FindControl поиск элемента с указанным ID на странице.
        //        CheckBox checkBox = FindControl("CheckBox" + i) as CheckBox;
        //        if (checkBox != null && checkBox.Checked == true)
        //        {
        //            output += checkBox.Text + "<br />";
        //        }
        //    }
        //    OutputLabel.Text = output;
        //}
    }
}