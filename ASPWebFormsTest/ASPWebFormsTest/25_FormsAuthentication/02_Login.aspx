<%@ Page Title="Login Page" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="02_Login.aspx.cs" Inherits="ASPWebFormsTest._25_FormsAuthentication._02_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Login:admin Pass:12345
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Login</td>
            <td>
                <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Password</td>
            <td>
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="MessageLabel" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
