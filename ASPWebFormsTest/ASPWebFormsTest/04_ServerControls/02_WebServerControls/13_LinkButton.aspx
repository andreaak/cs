<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="13_LinkButton.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._13_LinkButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LinkButton Sample</title>
</head>
<body>
    <p>
        Класс LinkButton транслируется в &lt;a href=&rdquo;javascript:__doPostBack()&rdquo;&gt;. 
        Это ссылка, которая вызывает серверный код по нажатию.
    </p>
    <form id="form1" runat="server">
        <div>
            <p>
                LinkButton - ссылка, которая работает как кнопка - вместо перенаправления браузера, шлет postback запрос и
            вызывает обработчик на стороне сервера.
            </p>

            <asp:LinkButton ID="LinkButton1" Text="Button A" runat="server" OnClick="LinkButton_Click" />
            <br />
            <asp:LinkButton ID="LinkButton2" Text="Button B" runat="server" OnClick="LinkButton_Click" />
            <br />
            <br />
            <br />
            <asp:Label ID="OutputLabel" runat="server" />
        </div>
    </form>
</body>
</html>
