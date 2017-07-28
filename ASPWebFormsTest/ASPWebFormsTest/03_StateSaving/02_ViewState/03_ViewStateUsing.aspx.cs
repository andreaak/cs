using System;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    public partial class _03_ViewStateUsing : System.Web.UI.Page
    {
        private const string key = "counter";

        private int Counter
        {
            get
            {
                // Чтение данных из ViewState
                object obj = ViewState[key];
                // Если ViewState хранит требуемый ключ - возвращаем его.
                if (obj != null)
                {
                    return (int)obj;
                }
                // Если ключ отсутствует - устанавливаем значение по умолчанию во ViewState
                else
                {
                    ViewState[key] = 0;
                    return 0;
                }
            }
            set
            {
                // Запись данных во ViewState
                ViewState[key] = value;
            }
        }

        protected void AddOneButton_Click(object sender, EventArgs e)
        {
            Counter += 1;
        }

        protected void AddTwoButton_Click(object sender, EventArgs e)
        {
            Counter += 2;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            CounterLabel.Text = Convert.ToString(ViewState["counter"]);
        }
    }
}