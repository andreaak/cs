<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="07_CheckBox.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._07_CheckBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CheckBox</title>
</head>
<body>
    <p>Класс CheckBox транслируется в &lt;input type=&rdquo;checkbox&rdquo;/&gt;</p>
    <form id="form1" runat="server">
        <div>
            <asp:CheckBox ID="CheckBox1" Text="Mozilla" runat="server" />
            <br />
            <asp:CheckBox ID="CheckBox2" Text="Internet Explorer" runat="server" />
            <br />
            <asp:CheckBox ID="CheckBox3" Text="Chrome" runat="server" />
            <br />
            <asp:CheckBox ID="CheckBox4" Text="Opera" runat="server" />
            <br />
            <asp:CheckBox ID="CheckBox5" Text="Safari" runat="server" />
            <br />
            <asp:Button Text="Отобразить" runat="server" OnClick="Button_Click" />
            <br />
            <br />
            <asp:Label ID="OutputLabel" runat="server" />
        </div>
    </form>
</body>
</html>
