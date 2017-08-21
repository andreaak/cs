using System.IO;
using System.Web.Mvc;

namespace _01_ASPMVCTest.Areas._01_Controller.Controllers
{
    public class _09_FileResultController : Controller
    {
        //
        // GET: /_01_Controller/_09_FileResult/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult C_01_BaseInfo()
        {
            return View();
        }

        public ActionResult C_02_DownloadFile()
        {
            string filename = Server.MapPath("/Content/File.pdf");
            // MIME Type image/png  image/jpg
            string contentType = "application/pdf"; 
            string downloadName = "PDF File";
            // Если имя файла для скачивания не указано и если 
            // браузер поддерживает тип файла, файл откроется в самом браузере.
            //downloadName = null; 
            return File(filename, contentType, downloadName);
        }

        public ActionResult C_03_DownloadBytes()
        {
            string filename = Server.MapPath("/Content/File.pdf");
            string contentType = "application/pdf";

            byte[] data = System.IO.File.ReadAllBytes(filename);

            return File(data, contentType);
        }

        public ActionResult C_04_DownloadStream()
        {
            string filename = Server.MapPath("/Content/File.pdf");
            string contentType = "application/pdf";

            FileStream stream = System.IO.File.OpenRead(filename);

            return File(stream, contentType);
        }
    }
}
