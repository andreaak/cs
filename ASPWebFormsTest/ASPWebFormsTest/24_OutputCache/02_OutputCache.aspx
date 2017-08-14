<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_OutputCache.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._02_OutputCache" %>

<%-- 
    Кэшировать данную страницу на 5 секунд. 
    На кэширование не влияют GET или POST параметры передаваемые при запросе.
    Для директивы OutputCache атрибуты Duration и VaryByParam являются обязательными.
--%>

<%@ OutputCache Duration="5" VaryByParam="None" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OutputCache</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Эта страница попала в кэш в
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" /></h1>
        </div>
    </form>
</body>
</html>
