using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ASPWebFormsTest._30_Examples._03_SendEmail
{
    public partial class Default : System.Web.UI.Page
    {
        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            // Получение данных из файла web.config
            string server = ConfigurationManager.AppSettings["server"];
            string port = ConfigurationManager.AppSettings["port"];
            string login = ConfigurationManager.AppSettings["login"];
            string password = ConfigurationManager.AppSettings["password"];

            // Настройка объекта сообщения
            MailMessage message = new MailMessage();
            message.Subject = TextBoxSubject.Text;
            message.From = new MailAddress(login);
            message.To.Add(new MailAddress(TextBoxEmail.Text));
            message.Body = TextBoxMsg.Text;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;

            // Настройка клиента для подключения к серверу
            SmtpClient client = new SmtpClient();
            client.Host = server;
            client.Port = Convert.ToInt32(port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(login, password);

            // Отправка сообщения.
            try
            {
                client.Send(message);
                LabelSuccess.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Ошибка при отправке сообщения: <br />" + ex.Message;
            }
        }
    }
}