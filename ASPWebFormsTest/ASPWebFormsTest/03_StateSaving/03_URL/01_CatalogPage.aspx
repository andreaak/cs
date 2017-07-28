<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_CatalogPage.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._03_URL.CatalogPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Список браузеров</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul>
            <li><a href="02_DescriptionPage.aspx?id=1">Mozzila Firefox</a></li>
            <li><a href="02_DescriptionPage.aspx?id=2">Google Chrome</a></li>
            <li><a href="02_DescriptionPage.aspx?id=3">Internet Explorer</a></li>
            <li><a href="02_DescriptionPage.aspx?id=4">Opera</a></li>
            <li><a href="02_DescriptionPage.aspx?id=5">Safari</a></li>
        </ul>
    </div>
    </form>
</body>
</html>