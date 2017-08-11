<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="08_AggregateCacheDependency.aspx.cs" Inherits="ASPWebFormsTest._23_CachingData._08_AggregateCacheDependency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>СacheDependency</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button Text="Write to cache" ID="Button1" runat="server" OnClick="Button_Click" />
            <asp:Label ID="MessageLabel" runat="server" />
            <asp:Button Text="Refresh" runat="server" />
        </div>
    </form>
</body>
</html>
