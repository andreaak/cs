using System;

namespace ASPWebFormsTest._02_Page._07_Server
{
    public partial class _02_MapPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Имя компьтера на котором запускается страница.
            Label1.Text += "MachineName  = " + Server.MachineName + "<br />";
            
            // Возрвращает физический путь соответствующий виртуальному.
            Label1.Text += "MapPath('02_MapPath.aspx')  = " + Server.MapPath("02_MapPath.aspx") + "<br />";
        }
    }
}