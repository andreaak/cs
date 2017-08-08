using System;
using System.Configuration;
using System.Web.Configuration;

namespace ASPWebFormsTest._15_ConfigFiles._04_WriteConfig
{
    public partial class _04_WriteConfig : System.Web.UI.Page
    {
        // Чтение данных из файла web.config
        protected void ReadButton_Click(object sender, EventArgs e)
        {
            string path = @"system.web/customErrors";
            CustomErrorsSection section = WebConfigurationManager.GetSection(path) as CustomErrorsSection;

            foreach (CustomError item in section.Errors)
            {
                Label1.Text += item.StatusCode;
                Label1.Text += " ";
                Label1.Text += item.Redirect;
                Label1.Text += "<br />";
            }
        }

        // Запись данных в web.config
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration("/");
            CustomErrorsSection section = cfg.GetSection("system.web/customErrors") as CustomErrorsSection;

            CustomError tempError = new CustomError(501, "http://example.com/not_implemented.aspx");
            section.Errors.Add(tempError);

            cfg.Save();
        }
    }
}