using System;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    /*
    По умолчанию ViewState отправляется в не зашифрованном виде.
    Чтобы Включить шифрование используется следующий атрибут ViewStateEncryptionMode
        "Always" - шифровать все данные во ViewStat при каждом запросе/ответе
        "Never" - не шифровать данные
        "Auto" - (по умолчанию) шифровать только те данные ViewState, которые относятся к контролам запросившим шифрование
                своей части информации во ViewState
    */
    public partial class _04_ViewStateSaveObject : System.Web.UI.Page
    {
        protected void WriteButton_Click(object sender, EventArgs e)
        {
            // Объекты, сохраняемые в состояние вида, должны быть сериализуемого типа.
            // Класс должен иметь атрибут Serializable
            Customer cust = new Customer("admin", "admin@example.com");
            ViewState["customer"] = cust;
        }

        protected void ReadButton_Click(object sender, EventArgs e)
        {
            Customer cust = ViewState["customer"] as Customer;
            if (cust != null)
            {
                UserNameLabel.Text = cust.UserName;
                EmailLabel.Text = cust.Email;
            }
            else
            {
                UserNameLabel.Text = "<i>null</i>";
                EmailLabel.Text = "<i>null</i>";
            }
        }
    }
}