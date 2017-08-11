using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    public class _02_SimpleDataBoundControl : DataBoundControl
    {
        #region Properties

        public _02_MessageDataItem DataItem
        {
            get
            {
                object data = ViewState["DataItem"];
                if (data == null)
                {
                    _02_MessageDataItem temp = new _02_MessageDataItem();
                    ViewState["DataItem"] = temp;
                    return temp;
                }
                return (_02_MessageDataItem)data;
            }
            set { ViewState["DataItem"] = value; }
        }

        // Имя свойства из источника данных, значение которого должно быть записано в Title
        public virtual string DataTitleField
        {
            get
            {
                object data = ViewState["DataTitleField"];
                if (data == null)
                {
                    return String.Empty;
                }
                return (string)data;
            }
            set { ViewState["DataTitleField"] = value; }
        }

        // Имя свойства из источника данных, значение которого должно быть записано в Message
        public virtual string DataMessageField
        {
            get
            {
                object data = ViewState["DataMessageField"];
                if (data == null)
                {
                    return String.Empty;
                }
                return (string)data;
            }
            set { ViewState["DataMessageField"] = value; }
        }
        #endregion

        /*
        Данный метод вызывается в случае вызова метода DataBind на объекте элемента управления.
        В качестве параметра в метод будет передаваться объект, который был получен 
        либо через свойство DataSource либо через свойство DataSourceId
        Задача данного метода произвести чтение данных и преобразовать их в информацию, которая будет отображена контролом.
        */
        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);

            IEnumerator e = data.GetEnumerator();
            e.MoveNext();

            string displayTitle = string.Empty;
            string displayMessage = string.Empty;

            if (DesignMode)
            {
                displayTitle = "[Databound]";
                displayMessage = "[Databound]";
            }
            else
            {
                if (!string.IsNullOrEmpty(DataTitleField))
                {
                    // DataBinder.GetPropertyValue(экземпляр, свойство) - метод с помощью рефлексии извлекает значение указанного свойства из определенного экземпляра.
                    displayTitle = (string)DataBinder.GetPropertyValue(e.Current, DataTitleField);
                }

                if (!string.IsNullOrEmpty(DataMessageField))
                {
                    displayMessage = (string)DataBinder.GetPropertyValue(e.Current, DataMessageField);
                }
            }

            DataItem.Title = displayTitle;
            DataItem.Message = displayMessage;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ChildControlsCreated = false;
            /*
            EnsureChildControls - Проверяет наличие дочерних контролов у текущего контрола, 
            если дочерних элементов нет то они создаются.
            */
            EnsureChildControls();
            base.Render(writer);
        }

        // Создает дочерние элементы управления.
        protected override void CreateChildControls()
        {
            Controls.Clear();

            Table table = new Table();
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();

            TableCell cell1Row1 = new TableCell();
            TableCell cell2Row1 = new TableCell();

            cell1Row1.Text = "Заголовок:";
            cell2Row1.Text = DataItem.Title;

            row1.Cells.Add(cell1Row1);
            row1.Cells.Add(cell2Row1);

            TableCell cell1Row2 = new TableCell();
            TableCell cell2Row2 = new TableCell();

            cell1Row2.Text = "Сообщение:";
            cell2Row2.Text = DataItem.Message;

            row2.Cells.Add(cell1Row2);
            row2.Cells.Add(cell2Row2);

            table.Rows.Add(row1);
            table.Rows.Add(row2);

            Controls.Add(table);
        }
    }
}

