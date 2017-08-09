<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_LifeCycle.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._02_ViewState._05_LifeCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
    <asp:Label ID="Output" runat="server" EnableViewState="False" />
</body>
</html>
