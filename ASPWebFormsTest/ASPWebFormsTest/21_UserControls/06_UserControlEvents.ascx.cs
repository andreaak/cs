using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _06_UserControlEvents : System.Web.UI.UserControl
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
        public event EventHandler Error;

        protected virtual void OnError(EventArgs e)
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
            catch
            {
                OnError(EventArgs.Empty);
            }
        }
    }
}