<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="02_Login.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._02_SimpleMembership._02_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="5" cellspacing="0">
        <tr>
            <td>Логин
            </td>
            <td>
                <asp:TextBox runat="server" ID="UserNameTextBox" />
            </td>
        </tr>
        <tr>
            <td>Пароль
            </td>
            <td>
                <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" Text="LogIn" runat="server" OnClick="LogInButton_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="ErrorLabel" Text="Логин или пароль введены не верно." Visible="false"
        ForeColor="Red" runat="server" />
</asp:Content>
