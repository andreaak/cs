<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_IsPostBackProp.aspx.cs" Inherits="ASPWebFormsTest._02_Page._04_IsPostBackProp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Свойство IsPostBack</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" />
            +
            <asp:TextBox ID="TextBox2" runat="server" />
            =
            <asp:TextBox ID="TextBox3" runat="server" />
            <asp:Button ID="Button1" Text="Сумма" runat="server" OnClick="Button1_OnClick" />
        </div>
    </form>
</body>
</html>
