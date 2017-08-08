using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace GetImagesFromDb
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class _07_GetImageFromDBHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Устанавливаем тип ответа.
            context.Response.ContentType = "image/jpeg";

            try
            {
                ReadImage(context);
            }
            catch
            {
                context.Response.End();
            }
        }

        private static void ReadImage(HttpContext context)
        {
            // Получение GET параметра.
            string imageId = context.Request.QueryString["image"];
            string connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            // Подключение к базе данных.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Извлечение фотографии по ID.
                SqlCommand command = new SqlCommand("SELECT Image FROM Images WHERE ImageID=@ID", connection);
                command.Parameters.AddWithValue("ID", imageId);

                connection.Open();
                byte[] image = command.ExecuteScalar() as byte[];
                context.Response.OutputStream.Write(image, 0, image.Length);

                connection.Close();
            }
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