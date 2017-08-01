using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls
{
    public partial class _03_ListBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            ListItem item = new ListItem();
            item.Text = ItemText.Text;
            item.Value = ItemValue.Text;
            // Добавление нового элемента в список.
            // Такой же принцип редактирования остальных списковых элементов управления.
            BrowsersListBox.Items.Add(item);
        }

        protected void RemoveSingleButton_Click(object sender, EventArgs e)
        {
            ListItem selected = ListBoxSingle.SelectedItem;
            ListBoxSingle.Items.Remove(selected);
        }

        protected void RemoveMultipleButton_Click(object sender, EventArgs e)
        {
            // Перебираем коллекцию элементов в обратном порядке.
            for (int i = ListBoxMultiple.Items.Count - 1; i >= 0; i--)
            {
                ListItem current = ListBoxMultiple.Items[i];
                if (current.Selected)
                {
                    ListBoxMultiple.Items.Remove(current);
                }
            }
        }
    }
}