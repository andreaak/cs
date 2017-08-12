<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="10_CastomDependency.aspx.cs" Inherits="ASPWebFormsTest._23_CachingData._10_CastomDependency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Custom CacheDependency</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button Text="Write Data to cache" runat="server" OnClick="Button_Click" />
            <asp:Label ID="MessageLabel" runat="server" />
            <asp:Button Text="Reload" runat="server" />
        </div>
    </form>
</body>
</html>
