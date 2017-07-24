using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClassUsing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Объявление класса Person находится в директории App_Code
        Person p = new Person("admin", "admin@example.com");
        Output.Text = p.GenerateHtml();
    }
}