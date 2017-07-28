using System;
using System.IO;

namespace ASPWebFormsTest._01_BaseInfo
{
    public partial class _07_AppData : System.Web.UI.Page
    {
        // Данная переменная выводится на странице с помощью выражения   <%=output %>
        public string output;

        protected void Page_Load(object sender, EventArgs e)
        {
            // С помощью метода Server.MapPath() виртуальный путь можно преобразовать в физический.
            // Относительный путь "App_Data\\TextFile.txt" преобразовывается в подобный C:\wwwroot\mysite\App_Data\TextFile.txt
            string filename = Server.MapPath(@"/App_Data/TextFile.txt");
            output = File.ReadAllText(filename);

            Person p = new Person("admin", "admin@example.com");

            output = output.Replace("<%LOGIN%>", p.Login);
            output = output.Replace("<%EMAIL%>", p.Email);
        }
    }
}