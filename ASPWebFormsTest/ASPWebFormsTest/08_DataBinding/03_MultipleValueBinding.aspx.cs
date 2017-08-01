using System;

namespace ASPWebFormsTest._08_DataBinding
{
    public partial class _03_MultipleValueBinding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] values = { "Item 1", "Item 2", "Item 3", "Item 4" };
            // Элементы управления, поддерживающие множественную привязку, содержат свойство
            // DataSource в которое можно поместить тип IEnumerable
            DropDownList1.DataSource = values;
            DropDownList1.DataBind();
        }
    }
}