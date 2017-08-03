<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_ReadFormValues.aspx.cs"
    Inherits="ASPWebFormsTest._02_Page._05_Request._03_Form._01_RequestPropertyDestination" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Чтение из Request.Form["XXX"]</h2>
        <div>
            Логин:
            <asp:Label ID="LabelLogin" runat="server" />
            <br />
            Пароль:
            <asp:Label ID="LabelPassword" runat="server" />
        </div>
        <h2>Чтение из Request["XXX"]</h2>
        <div>
            Логин:
            <asp:Label ID="Label1" runat="server" />
            <br />
            Пароль:
            <asp:Label ID="Label2" runat="server" />
        </div>
    </form>
</body>
</html>
