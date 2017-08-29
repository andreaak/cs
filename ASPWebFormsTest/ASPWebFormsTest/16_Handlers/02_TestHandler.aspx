<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_TestHandler.aspx.cs" Inherits="ASPWebFormsTest._16_Handlers._02_TestHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Default</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="/02_Handler.aspx">HTTP обработчик зарегистрированный в Web.config</a><br />
        <a href="03_TextHandler.ashx?id=5">Text Handler</a><br />
        <a href="04_ImageHandler.ashx">Image Handler</a>
    </div>
    </form>
</body>
</html>
