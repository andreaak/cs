<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_CacheProfile.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._07_CacheProfile" %>

<%--Настройки кэша для текущей страницы находятся в файле web.config--%>
<%@ OutputCache CacheProfile="TestProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CahceProfile</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Эта страница попала в кэш в
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" /></h1>
            <a href="07_CacheProfile.aspx">Reload</a>
        </div>
    </form>
</body>
</html>
