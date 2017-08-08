using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ASPWebFormsTest._16_Handlers
{
    public partial class _07_InsertImage : System.Web.UI.Page
    {
        // Допустимые расширения.
        List<string> _extensions = new List<string>() {".png", ".jpg", ".gif" };

        // В ASP стоит ограничение на загружаймый файл равное 4096 Kb
        // Для того что бы увеличить ограничение в web.config нужно добавить строку <httpRuntime maxRequestLength="20480" />
        int _maxLength = 10485760; // Максимальный размер файла. 10 Mb (10 * 1024 * 1024)


        protected void ButtonUpload_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Получение расширения файла.
                string fileExtension = Path.GetExtension(fileUploader.PostedFile.FileName);
                // Получения размера файла.
                int fileLenght = fileUploader.PostedFile.ContentLength;

                if (!_extensions.Contains(fileExtension))
                {
                    throw new Exception("Файлы с таким расширением не принимаются для загрузки.");
                }
                if (fileLenght > _maxLength)
                {
                    throw new Exception("Размер файла не более 10 Mb.");
                }

                //// Создание уникального имени для файла.
                //string newFileName = Guid.NewGuid().ToString();
                //// Создание пути для сохранения файла на сервере.
                //string newPathForSave = Path.Combine(Server.MapPath("Uploads"), newFileName + fileExtension);
                //// Сохранение файла.
                //fileUploader.SaveAs(newPathForSave);
                SaveImage(fileUploader.FileBytes);
                LabelForMessage.Text = "Файл успешно загружен.";
            }
            catch (Exception ex)
            {
                LabelForMessage.Text = ex.Message;
            }
        }

        private void SaveImage(byte[] bytes)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            using (SqlConnection Conection = new SqlConnection(connectionString))
            {
                Conection.Open();
                using (SqlCommand Command = new SqlCommand("INSERT INTO Images VALUES(@Image);", Conection))
                {
                    Command.CommandType = CommandType.Text;
                    Command.Parameters.Add("@Image", SqlDbType.Binary/*, 8000*/).Value = bytes;
                    Command.ExecuteNonQuery();
                }
                Conection.Close();
            }

        }
    }
}