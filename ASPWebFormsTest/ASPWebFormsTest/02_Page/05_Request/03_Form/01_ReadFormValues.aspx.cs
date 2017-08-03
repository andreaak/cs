using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._02_Page._05_Request._03_Form
{
    public partial class _01_RequestPropertyDestination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Request.Form[имя_параметра] - свойство дает возможность получить доступ 
            к коллекции переменных формы,
            которые передаются в HTTP заголовках вместе с запросом к странице. 
            */
            string login = Request.Form["loginParam"];
            string password = Request.Form["passwordParam"];
            
            WriteInfo(login, password, LabelLogin, LabelPassword);

            login = Request["loginParam"];
            password = Request["passwordParam"];

            WriteInfo(login, password, Label1, Label2);
        }

        private void WriteInfo(string login, string password, Label loginL, Label passwordL)
        {
            loginL.Text = string.IsNullOrEmpty(login) ? "Параметр loginParam не найден." : login;

            passwordL.Text = string.IsNullOrEmpty(password) ? "Параметр passwordParam не найден." : password;
        }
    }
}