<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppConfigUsing.aspx.cs" Inherits="ASPWebFormsTest._30_Examples._02_WebConfigUsing.AppConfigUsing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Чтение данных из AppSettings</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Имя проекта: 
        <asp:Literal runat="server" Text="<%$ AppSettings:ProjectName %>"></asp:Literal>
        <br />
        Версия проекта: 
        <asp:Literal ID="Literal1" runat="server" />
    </div>
    </form>
</body>
</html>
