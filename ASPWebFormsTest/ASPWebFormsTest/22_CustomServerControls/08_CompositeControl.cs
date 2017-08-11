using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    /*
     Наследуя класс CompositControl гарантируется отображение элемент управления 
     в дизайнере студии при разработке.
     При наследовании от класса WebControl, для этого нужно переопределить метод Render. 

     INamingContainer реализация интерфейса, является гарантией того, что элемент управления станет 
     пространством имен для своих дочерних элементов управления. 
     Если на странице будет два контрола MyCompositeControl ID их
     вложенных элементов управления будут уникальными.
    */

    public class _08_CompositeControl : CompositeControl, INamingContainer
    {
        public event EventHandler TestEvent;

        private Label _label;
        private TextBox _textBox;
        private Button _button;
        private Label _labelResult;

        protected override void CreateChildControls()
        {
            if (DesignMode) // Если дизайнер Visual Studio
            {
                Label label = new Label();
                label.Text = "[_08_CompositeControl]";
                Controls.Add(label);
            }
            else // Если проект выполняется
            {
                CreateControls();
            }

            base.CreateChildControls();
        }

        private void CreateControls()
        {
            _label = new Label();
            _label.Text = "Введите имя ";

            _textBox = new TextBox();
            _textBox.ID = "TextBox1";

            _button = new Button();
            _button.ID = "Button1";
            _button.Text = "Click me!";
            _button.Click += new EventHandler(_button_Click);

            _labelResult = new Label();
            _labelResult.ID = "Label1";

            Controls.Add(_label);
            Controls.Add(_textBox);
            Controls.Add(_button);
            Controls.Add(new LiteralControl("<br />"));
            Controls.Add(_labelResult);
        }

        void _button_Click(object sender, EventArgs e)
        {
            _labelResult.Text = "Привет " + _textBox.Text;
        }
    }
}
