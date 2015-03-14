<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_01_DestinationPage.aspx.cs" Inherits="ASPTest._02_Page._05_PageRedirection._01_DestinationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Destination Form</h1>
        Id = <%= Request.QueryString["id"] %>
    </div>
    </form>
</body>
</html>
