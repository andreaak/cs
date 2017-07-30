<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_Cookie.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._04_Cookies._01_Cookie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование Cookie-наборов</title>
</head>
<body>
    <p>
        Cookie наборы – файлы, которые создаются на диске клиента или в памяти веб браузера 
        и содержат информацию полученную с сервера. 
        Данная информация передается каждый раз, когда браузер делает запрос к сайту, от которого получил cookie набор.
    </p>
    <div>
        <img src="01_Cookie.png" />
    </div>

    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="SaveButton" Text="Сохранить в cookies" runat="server" OnClick="SaveButton_Click" />
            <a href="02_CookieRead.aspx">Страница тестирования</a>
        </div>
    </form>
</body>
</html>
