<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_Page2.aspx.cs" Inherits="ASPWebFormsTest._17_Modules._03_Page2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pafe 2</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Page 2
            <% System.Threading.Thread.Sleep(new Random().Next(5000)); %>
        </div>
    </form>
</body>
</html>
