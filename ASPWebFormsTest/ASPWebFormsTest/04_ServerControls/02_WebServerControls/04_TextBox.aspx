<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_TextBox.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._04_TextBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TextBox #2 (Выравнивание элементов управления с помощью CSS)</title>
    <style type="text/css">
        label {
            width: 90px;
            float: left;
        }

        div {
            margin: 4px;
        }
    </style>
</head>
<body>
    <p>
        Класс TextBox транслируется в &lt;input type=&rdquo;text&rdquo; /&gt;
    </p>
    <form id="form1" runat="server">
        <div>
            <div>
                <label>
                    Логин</label>
                <asp:TextBox runat="server" ID="LoginTextBox" />
            </div>
            <div>
                <label>
                    Пароль</label>
                <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" />
            </div>
            <div>
                <asp:Label ID="ErrorLabel" ForeColor="Red" Text="Ошибка!!!" runat="server" Visible="false" />
            </div>
            <div>
                <asp:Button Text="Вход" runat="server" OnClick="Button_Click" />
            </div>
        </div>
        <div>
            Поле с ограничением 
        <asp:TextBox ID="TextBox_1" runat="server" MaxLength="3" />
            <br />
            Поле для пароля
        <asp:TextBox ID="TextBox_2" runat="server" TextMode="Password" />
            <br />
            Многострочное поле
        <asp:TextBox ID="TextBox_3" runat="server" TextMode="MultiLine" Height="200" Width="200" />
        </div>
    </form>
</body>
</html>
