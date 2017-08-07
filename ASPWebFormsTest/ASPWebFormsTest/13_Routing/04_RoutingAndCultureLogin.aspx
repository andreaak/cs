<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_RoutingAndCultureLogin.aspx.cs" 
    Inherits="ASPWebFormsTest._13_Routing._04_RoutingAndCultureLogin" meta:resourcekey="PageResource1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LoginLabel" runat="server" meta:resourcekey="LoginLabelResource1" />
            <asp:TextBox ID="Username" runat="server" />
            <br />

            <asp:Label ID="PasswordLabel" runat="server" meta:resourcekey="PasswordLabelResource1" />
            <asp:TextBox ID="Password" runat="server" TextMode="Password" />
            <br />

            <asp:Button runat="server" meta:resourcekey="ButtonResource1" />
        </div>
    </form>
</body>
</html>
