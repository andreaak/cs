<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="12_HyperLink.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._12_HyperLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HyperLink Sample</title>
</head>
<body>
    <p>Класс HyperLink транслируется в &lt;a&gt;</p>
    <form id="form1" runat="server">
        <div>
            <a href="http://example.com">HTML Link</a>
            <br />
            <asp:HyperLink runat="server" NavigateUrl="http://example.com" Text="ASP Link"></asp:HyperLink>
        </div>
    </form>
</body>
</html>
