using System;
using System.Data;

namespace ASPWebFormsTest._12_Localization
{
    public partial class _04_ImplicitLocalizationInnerObjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("FirstName"));
            table.Columns.Add(new DataColumn("LastName"));

            table.LoadDataRow(new object[] { "Ivan", "Ivanov" }, true);
            table.LoadDataRow(new object[] { "Jhon", "Smith" }, true);
            table.LoadDataRow(new object[] { "Petr", "Petrov" }, true);

            GridViewLocalization.DataSource = table;
            GridViewLocalization.DataBind();
        }
    }
}