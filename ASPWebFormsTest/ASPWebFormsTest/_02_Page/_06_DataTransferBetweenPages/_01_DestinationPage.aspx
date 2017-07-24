<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_01_DestinationPage.aspx.cs" Inherits="ASPWebFormsTest._02_Page._06_DataTransferBetweenPages._01_DestinationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Destination Form</h1>
        </div>
        TextBox Value: <%= Request.Form["TextBox1"] %> <br />
        TextBox Value2: <%= Request.Form["TextBox2"] %>
    </form>
</body>
</html>
