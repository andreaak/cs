using System;

namespace ASPWebFormsTest._30_Examples._01_Captcha
{
    public partial class _01_Captcha : System.Web.UI.Page
    {
        protected void OkButton_Click(object sender, EventArgs e)
        {
            string captcha = Session["captcha"] as string;
            string currentText = CaptchaTextBox.Text.ToUpper();

            if (captcha == currentText)
            {
                ResultLabel.Text = "Текст введен правильно";
            }
            else
            {
                ResultLabel.Text = "Ошибка";
            }
        }
    }
}