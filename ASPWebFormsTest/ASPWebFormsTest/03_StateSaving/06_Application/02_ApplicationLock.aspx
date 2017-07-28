<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_ApplicationLock.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._06_Application.ApplicationLock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application #2</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="CounterLabel" runat="server" Text="0"></asp:Label>
        <br />
        <asp:Button ID="AddOneButton" runat="server" Text="+1" OnClick="AddOneButton_Click" />
    </div>
    </form>
</body>
</html>
