using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;

namespace ASPWebFormsTest._30_Examples._01_Captcha
{
    public class _01_CaptchaHandler : IHttpHandler, IRequiresSessionState
    {
        Random rand = new Random();

        public void ProcessRequest(HttpContext context)
        {
            // Указываем тип ответа данного хэндлера.
            context.Response.ContentType = "image/jpeg";

            // Высота и ширина картинки
            int height = 60;
            int width = 100;

            // Объект изображения.
            Bitmap captchaImage = new Bitmap(width, height);
            // Объект для изменения пикелей изображения.
            Graphics gr = Graphics.FromImage(captchaImage);

            // Создание строки для вывода на изображении.
            string captchaString = GenerateCaptchaString();
            // Сохраняем текст, который будет выведен на изображении в Session
            HttpContext.Current.Session["captcha"] = captchaString;

            // Заполнение изображения цветом.
            gr.FillRectangle(GenerateBrush(), new Rectangle(0, 0, width, height));
            // Создание кисти для рисования текста.
            Brush textBrush = GenerateBrush();

            // Добавление в содержимое страницы текста.
            gr.DrawString(captchaString,
                new Font("Arial", 10.0f),
                textBrush,
                new Point(20, 20));

            // Сохранение изображения в ответ пользователю.
            captchaImage.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            // Завершение формирования ответа и отправка изображения пользователю.
            context.Response.End();
        }

        // Метод для создания текста каптчи.
        private string GenerateCaptchaString()
        {
            string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string captchaString = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int pos = rand.Next(0, symbols.Length);
                captchaString += symbols[pos];
            }
            return captchaString;
        }

        // Метод для создания случайного цвета.
        private Brush GenerateBrush()
        {
            int r = rand.Next(255);
            int g = rand.Next(255);
            int b = rand.Next(255);
            return new SolidBrush(Color.FromArgb(r, g, b));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}