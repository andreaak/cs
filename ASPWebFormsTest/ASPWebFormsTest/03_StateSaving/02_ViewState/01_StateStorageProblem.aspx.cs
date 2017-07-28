using System;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    public partial class _01_StateStorageProblem : System.Web.UI.Page
    {
        /*
        При каждом запросе экземпляр этого класса создается повторно,
        соответственно при вызове любого из обработчиков (AddOneButton_Click или AddTwoButton_Click)
        значение  переменной _counter будет равное 0.
        */
        int _counter = 0;

        protected void AddOneButton_Click(object sender, EventArgs e)
        {
            _counter += 1;
            CounterLabel.Text = _counter.ToString();
        }

        protected void AddTwoButton_Click(object sender, EventArgs e)
        {
            _counter += 2;
            CounterLabel.Text = _counter.ToString();
        }
    }
}