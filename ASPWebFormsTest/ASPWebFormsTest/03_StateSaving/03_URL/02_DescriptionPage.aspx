<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_DescriptionPage.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._03_URL.DescriptionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Описание браузера</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="01_CatalogPage.aspx">Каталог</a>
            <br />
            <br />
            <asp:Label ID="DescriptionLabel" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
