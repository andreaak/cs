<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_Label.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._05_Label" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Label #1</title>
</head>
<body>
    <p>Класс Label транслируется в элемент &lt;span&gt;</p>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonA" Text="Button A" runat="server" OnClick="ButtonA_Click" />
            <asp:Button ID="ButtonB" Text="Button B" runat="server" OnClick="ButtonB_Click" />
            <br />
            <br />
            <br />
            <asp:Label ID="TestLabel" runat="server"
                ForeColor="Maroon"
                EnableViewState="true" />
            <!-- попробовать EnableViewState="false"-->
        </div>

        <div>
            <p>
                AccessKey - клавиша, которую нужно зажать вместе с ALT 
                при этом AssociatedControlID - элемент управления, который получит фокус
            </p>
            <asp:Label ID="FirstNameLabel" runat="server" AccessKey="F" AssociatedControlID="TextBox1"
                Text="<u>F</u>irst name: ">
            </asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="LastNameLabel" runat="server" AccessKey="L" AssociatedControlID="TextBox2"
                Text="<u>L</u>ast name: ">
            </asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
