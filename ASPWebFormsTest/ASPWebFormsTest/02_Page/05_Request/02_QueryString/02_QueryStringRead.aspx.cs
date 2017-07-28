using System;

namespace ASPWebFormsTest._02_Page._05_Request._02_QueryString
{
    public partial class _02_QueryStringRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Request.QueryString[имя_параметра] - это свойство дает возможность получить данные из адресной строки.
            В первую очередь, при работе с GET параметрами, нужно проверить наличие значений при получении запроса. 
            После (если это требуется) проверить тип полученного значения. GET параметры могут быть изменены пользователем
            в следствии чего, неправильные данные в параметрах могут нарушить работу страницы.
            */
            string param = Request.QueryString["param"];

            if (string.IsNullOrEmpty(param))
            {
                Label1.Text = "В адресной строке нет GET параметра с именем <i>param</i>";
            }
            else
            {
                Label1.Text = "param = " + param;
            }
        }
    }
}