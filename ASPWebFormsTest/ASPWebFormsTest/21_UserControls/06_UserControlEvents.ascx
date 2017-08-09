<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="06_UserControlEvents.ascx.cs"
    Inherits="ASPWebFormsTest._21_UserControls._06_UserControlEvents" %>

<asp:Panel ID="CalculatorPanel" runat="server" BorderColor="Black" BorderWidth="1px" BorderStyle="Dashed" Style="padding: 10px; float: left;">
    <asp:TextBox runat="server" ID="Summand1TextBox" />
    +
    <asp:TextBox runat="server" ID="Summand2TextBox" />
    =
    <asp:Label ID="ResultLabel" runat="server" />
    <br />
    <asp:Button ID="GetSumButton" Text="Получить сумму" runat="server" OnClick="GetSumButton_Click" />
</asp:Panel>
