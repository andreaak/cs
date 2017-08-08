<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_Page1.aspx.cs" Inherits="ASPWebFormsTest._17_Modules._03_Page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page 1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Page 1
            <% System.Threading.Thread.Sleep(1000); %>
        </div>
    </form>
</body>
</html>
