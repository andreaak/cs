using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._09_Validation
{
    public partial class _06_CustomValidator : System.Web.UI.Page
    {
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("Сработал обработчик в коде страницы.");
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int value;
            if (int.TryParse(args.Value, out value))
            {
                if (value % 2 == 0)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    (source as Label).Text = "Значение должно быть четным";
                }
            }
            else
            {
                args.IsValid = false;
                (source as Label).Text = "Введите число";
            }
        }
    }
}