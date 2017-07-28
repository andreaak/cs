<%--Общие настройки страницы--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="001_BaseInfo.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hello ASP.NET WebSite</title>
</head>
<body>
    <%--Форма для серверных элементов управления--%>
    <form id="form1" runat="server">
        <div>
            <%--Серверный элемент управления--%>
            <asp:Label ID="Output" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
