<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_CustomValidator.aspx.cs" Inherits="ASPWebFormsTest._09_Validation._06_CustomValidator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CustomValidator</title>

    <script type="text/javascript">
        /*
        source - span, в который выводиться сообщение об ошибке.
        arguments - дополнительные параметры:
        Value - значение поля ввода.
        IsValid - корректность введенных данных в поле ввода.
        */
        function validate(source, arguments) {
            if (arguments.Value % 2 == 0) {
                arguments.IsValid = true;
            }
            else {
                source.innerHTML = "Значение должно быть четным числом";
                arguments.IsValid = false;
            }
        }
    </script>

</head>
<body>
    <p>
        ClientValidationFunction - функция для проверки данных на стороне клиента
        <br />
        OnServerValidate - метод для проверки данных на стороне сервера.
    </p>
    <br />
    <form id="form1" runat="server">
        <div>
            Введите четное значение 
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

            <asp:CustomValidator ID="CustomValidator1"
                runat="server"
                ControlToValidate="TextBox1"
                ClientValidationFunction="validate"
                OnServerValidate="CustomValidator1_ServerValidate"
                ForeColor="Red">
            </asp:CustomValidator>
            <br />
            <asp:Button ID="Button1" runat="server" Text="OK" OnClick="ButtonOk_Click" />
        </div>
    </form>
</body>
</html>
