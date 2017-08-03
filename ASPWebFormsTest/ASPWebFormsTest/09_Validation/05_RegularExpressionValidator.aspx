<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_RegularExpressionValidator.aspx.cs"
    Inherits="ASPWebFormsTest._09_Validation._05_RegularExpressionValidator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RegularExpressionValidator</title>
</head>
<body>
    <p>
        RegularExpressionValidator - Проверка значения в поле ввода с помощью регулярного выражения<br />
        Пустое значение проходит проверку всегда.
    </p>
    <br />
    <form id="form1" runat="server">
        <div>
            Email<asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator1"
                runat="server"
                ControlToValidate="TextBoxEmail"
                ErrorMessage="Введите Email адрес еще раз."
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
            <br />
            <asp:Button ID="ButtonOk" runat="server" Text="Ok" OnClick="ButtonOk_Click" />
        </div>
    </form>
</body>
</html>
