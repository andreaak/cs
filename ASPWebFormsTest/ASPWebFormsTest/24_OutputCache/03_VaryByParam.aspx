<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_VaryByParam.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._03_VaryByParam" %>

<!--
    VaryByParam="ID" на каждое значение параметра Id, 
    будет создана отдельная копия страницы в кэше.
    '*' - отдельный кэш для каждой комбинации параметров
    В случае нескольких имен параметров они передаются с разделителем ';'
-->

<%@ OutputCache Duration="30" VaryByParam="Id" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VaryByParam Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>
                <asp:Label ID="MessageLabel" runat="server" /></h1>
            <a href="03_VaryByParam.aspx?id=1">Link 1</a><br />
            <a href="03_VaryByParam.aspx?id=2">Link 2</a><br />
            <a href="03_VaryByParam.aspx?id=3">Link 3</a><br />
        </div>
    </form>
</body>
</html>
