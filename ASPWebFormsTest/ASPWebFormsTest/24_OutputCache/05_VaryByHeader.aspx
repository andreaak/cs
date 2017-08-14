<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_VaryByHeader.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._05_VaryByHeader" %>

<%--
    На каждое значение заголовка Accept-Language будет создана отдельная запись в кэшэ
--%>
<%@ OutputCache Duration="60" VaryByHeader="Accept-Language" VaryByParam="none" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VaryByHeader Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Эта страница попала в кэш в
            <%: DateTime.Now.ToLongTimeString() %></h1>

            <h3>Значение HTTP заголовка Accept-Language
            <%: Request.Headers["Accept-Language"] %></h3>
        </div>
    </form>
</body>
</html>
