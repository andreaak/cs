using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _03_PlaceHolder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button myButton = new Button();
            myButton.Text = "Кнопка";
            // События в файле кода называются без приставки 'On'
            myButton.Click += new EventHandler(myButton_Click);
            
            // Добавление элемента управления на страницу.
            PlaceHolder1.Controls.Add(myButton);

            // Используйте PlaceHolder для того, что бы добавить элемент в определенное место на странице
            // или следующий код, что бы добавить контрол в конец страницы.
            // form1.Controls.Add(myButton);
        }

        void myButton_Click(object sender, EventArgs e)
        {
            Response.Write("На сервере сработал обработчик события");
        }
    }
}