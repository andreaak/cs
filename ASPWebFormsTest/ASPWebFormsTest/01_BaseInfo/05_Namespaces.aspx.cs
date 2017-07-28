using System;
using System.IO;
// Подключение пространства имен.

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _05_Namespaces : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileInfo info = new FileInfo("C:\\windows\\explorer.exe");
            long size = info.Length;
            string output = string.Format("Размер фала explorer.exe <b>{0}</b> байт", size);
            Output.Text = output;
        }
    }
}