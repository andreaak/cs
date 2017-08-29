<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPWebFormsTest._03_StateSaving._07_AuthenticationSample.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Добро пожаловать
        <asp:Label ID="LoginLabel" Text="Гость" runat="server" />
        <asp:HyperLink ID="LoginLink" NavigateUrl="Login.aspx" runat="server" >Вход</asp:HyperLink>
        <asp:HyperLink ID="LogoutLink" NavigateUrl="Logout.aspx" Visible="false" runat="server" >Выход</asp:HyperLink>
        <br />
        <br />
        <br />
        <a href="Closed.aspx">Секретная страница</a>
    </div>
    </form>
</body>
</html>
