using System;

namespace ASPWebFormsTest._02_Page
{
    public partial class _04_IsPostBackProp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Закомментировать
            if (!IsPostBack)
            {
                // Устанавливать значения для полей ввода только при первой загрузке страницы.
                // При postback запросе данный код не отработает.
                TextBox1.Text = "0";
                TextBox2.Text = "0";
            }
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(TextBox1.Text);
            int b = Convert.ToInt32(TextBox2.Text);
            TextBox3.Text = Convert.ToString(a + b);
        }
    }
}