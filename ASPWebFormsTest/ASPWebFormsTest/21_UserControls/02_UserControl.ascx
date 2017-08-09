<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="02_UserControl.ascx.cs" Inherits="ASPWebFormsTest._21_UserControls._02_UserControl" %>

<asp:TextBox ID="TestTextBox" runat="server"></asp:TextBox>
<asp:Button Text="Click Me" runat="server" ID="TestButton"
    OnClick="TestButton_Click" />