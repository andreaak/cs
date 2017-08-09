using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _07_UserControlEventArgs : System.Web.UI.UserControl
    {
        // Свойства котрола можно инициализировать через атрибуты в разметке
        public int Height
        {
            get { return (int)CalculatorPanel.Height.Value; }
            set { CalculatorPanel.Height = value; }
        }

        public int Width
        {
            get { return (int)CalculatorPanel.Width.Value; }
            set { CalculatorPanel.Width = value; }
        }

        // Обработчик на это событие можно добавить через разметку с помощью атрибута OnError.
        public event EventHandler<_07_EventArgs> Error;

        protected virtual void OnError(_07_EventArgs e)
        {
            if (Error != null)
            {
                Error.Invoke(this, e);
            }
        }

        protected void GetSumButton_Click(object sender, EventArgs e)
        {
            try
            {
                int summand1 = Convert.ToInt32(Summand1TextBox.Text);
                int summand2 = Convert.ToInt32(Summand2TextBox.Text);
                ResultLabel.Text = (summand1 + summand2).ToString();
            }
            catch(Exception ex)
            {
                OnError(new _07_EventArgs(ex.Message));
            }
        }
    }
}