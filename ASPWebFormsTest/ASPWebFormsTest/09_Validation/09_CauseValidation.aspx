<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09_CauseValidation.aspx.cs"
    Inherits="ASPWebFormsTest._09_Validation._09_CauseValidation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>09_CauseValidation</title>
</head>
<body>
    <p>
        Каждая кнопка имеет свойство CauseValidation.<br />
        Если значение свойства равно false нажатие по кнопке не приводит к запуску проверки достоверности формы.<br />
        Если значение свойства true - каждый валидатор проверяет связанный элемент управления. 
        Если в процессе проверки обнаруживается ошибка, форма не отправляется на сервер. 
    </p>
    <br />
    <form id="form1" runat="server">
        <div>
            <!--Элемент управления для проверки-->
            Логин
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

            <asp:RequiredFieldValidator
                ID="RequiredFieldValidator1"
                runat="server"
                ControlToValidate="TextBox1"
                ErrorMessage="Это поле не может быть пустым!"
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="Button1" runat="server" Text="OK" OnClick="OkButton_Click" CausesValidation="False"/>
        </div>
    </form>
</body>
</html>
