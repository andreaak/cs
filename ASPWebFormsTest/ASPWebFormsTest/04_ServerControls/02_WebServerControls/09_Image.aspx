<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09_Image.aspx.cs" 
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._09_Image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image #1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--~/ - корень веб приложения--%>
            <asp:Image ImageUrl="Images/logo.jpg" runat="server" />
            <br />
            <asp:Image ID="Image1" runat="server" />
        </div>
    </form>
</body>
</html>
