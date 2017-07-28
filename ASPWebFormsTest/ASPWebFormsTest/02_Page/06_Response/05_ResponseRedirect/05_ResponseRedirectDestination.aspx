<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_ResponseRedirectDestination.aspx.cs" 
    Inherits="ASPWebFormsTest._02_Page._06_Response._05_ResponseRedirect._05_ResponseRedirectDestination" %>

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
