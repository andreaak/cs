using System;

namespace ASPWebFormsTest._02_Page._05_Request._03_Form
{
    public partial class _01_RequestPropertyDestination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string login, password;
            
            // Request.Form[имя_параметра] - свойство дает возможность получить доступ к коллекции переменных формы,
            // которые передаются в HTTP заголовках вместе с запросом к странице. 
            login = Request.Form["loginParam"];
            password = Request.Form["passwordParam"];
            
            if (string.IsNullOrEmpty(login))
            {
                LabelLogin.Text = "Параметр loginParam не найден.";
            }
            else
            {
                LabelLogin.Text = login;
            }

            if (string.IsNullOrEmpty(password))
            {
                LabelPassword.Text = "Параметр passwordParam не найден.";
            }
            else
            {
                LabelPassword.Text = password;
            }
        }
    }
}