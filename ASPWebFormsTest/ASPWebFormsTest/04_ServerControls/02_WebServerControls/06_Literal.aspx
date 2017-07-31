<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_Literal.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._06_Literal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Literal #1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label Text="Label преобразуется в span" runat="server" />
            <br />
            <br />
            <asp:Literal Text="Literal - внедряется в страницу как чистый текст" runat="server" />
        </div>
    </form>
</body>
</html>
