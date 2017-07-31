<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_PlaceHolder.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._03_PlaceHolder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PlaceHolder</title>
</head>
<body>
    <p>Используйте PlaceHolder для того, что бы добавить элемент в определенное место на странице</p>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
