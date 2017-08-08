<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPWebFormsTest._15_ConfigFiles._02_PathAttribute.User.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web.Config location</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Connection String from web.config: <asp:Label ID="Label1" Font-Bold="true" Text="<%$ ConnectionStrings:UserConnection %>" runat="server" />
        <%--<asp:Label ID="Label2" Text="<%$ ConnectionStrings:AdminConnection %>" runat="server" />--%>
    </div>
    </form>
</body>
</html>
