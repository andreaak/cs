using System;
using System.Collections.Generic;

namespace ASPWebFormsTest._08_DataBinding
{
    public partial class _04_MultipleObjectsBinding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // При PostBack запросе выполнять привязку данных не нужно.
                return;
            }

            List<BrowserItem> items = new List<BrowserItem>();
            items.Add(new BrowserItem() { BrowserId = 1, BrowserName = "Mozilla Firefox" });
            items.Add(new BrowserItem() { BrowserId = 2, BrowserName = "Google Chrome" });
            items.Add(new BrowserItem() { BrowserId = 3, BrowserName = "Internet Explorer" });
            items.Add(new BrowserItem() { BrowserId = 4, BrowserName = "Opera" });
            items.Add(new BrowserItem() { BrowserId = 5, BrowserName = "Safari" });

            // Установка источника данных.
            DropDownList1.DataSource = items;
            // Имя свойства одного элемента из источника данных, которое будет использоваться для
            // отображения названия пункта в элементе управления.
            DropDownList1.DataTextField = "BrowserName";
            // Имя свойства для Value элемента в списке.
            DropDownList1.DataValueField = "BrowserId";

            // Привязка значений.
            DropDownList1.DataBind();
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            string text = "Text: " + DropDownList1.SelectedItem.Text + "<br />";
            text += "Value: " + DropDownList1.SelectedItem.Value + "<br />";
            OutputLabel.Text = text;
        }

        public class BrowserItem
        {
            public int BrowserId { get; set; }
            public string BrowserName { get; set; }
        }
    }
}