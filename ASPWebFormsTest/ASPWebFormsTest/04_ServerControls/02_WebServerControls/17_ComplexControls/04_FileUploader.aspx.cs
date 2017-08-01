using System;
using System.Collections.Generic;
using System.IO;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls
{
    public partial class _04_FileUploaderSample : System.Web.UI.Page
    {
        // Допустимые расширения.
        List<string> _extensions = new List<string>() { ".rar", ".txt", ".doc", ".pdf", ".mp4" };

        // В ASP стоит ограничение на загружаймый файл равное 4096 Kb
        // Для того что бы увеличить ограничение в web.config нужно добавить строку <httpRuntime maxRequestLength="20480" />
        int _maxLength = 10485760; // Максимальный размер файла. 10 Mb (10 * 1024 * 1024)

        protected void UploadButton_Click(object sender, EventArgs e)
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
                
                // Создание уникального имени для файла.
                string newFileName = Guid.NewGuid().ToString();
                // Создание пути для сохранения файла на сервере.
                string newPathForSave = Path.Combine(Server.MapPath("Uploads"), newFileName + fileExtension);
                // Сохранение файла.
                fileUploader.SaveAs(newPathForSave);
                LabelForMessage.Text = "Файл успешно загружен.";
            }
            catch (Exception ex)
            {
                LabelForMessage.Text = ex.Message;
            }
        }
    }
}