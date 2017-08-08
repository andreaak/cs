<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_AsyncHandlerTest.aspx.cs" Inherits="ASPWebFormsTest._20_AsyncPages._08_AsyncHandlerTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование асинхронного обработчика</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="08_AsyncHandler.ashx">Async Handler</a>
            <br />
            <a href="08_FastPage.aspx">FastPage.aspx</a>
            <br />
            <a href="08_SlowPage.aspx">SlowPage.aspx</a>
            <br />
        </div>
    </form>
</body>
</html>
