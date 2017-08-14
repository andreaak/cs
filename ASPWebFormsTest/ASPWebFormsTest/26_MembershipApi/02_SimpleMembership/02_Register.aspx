<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="02_Register.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._02_SimpleMembership._02_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="5" cellspacing="0">
        <tr>
            <td>Имя
            </td>
            <td>
                <asp:TextBox runat="server" ID="UserNameTextBox" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ErrorMessage="*"
                    ForeColor="Red" ControlToValidate="UserNameTextBox">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Пароль
            </td>
            <td>
                <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="*"
                    ForeColor="Red" ControlToValidate="PasswordTextBox" />
            </td>
        </tr>
        <tr>
            <td>Подтверждение пароля.
            </td>
            <td>
                <asp:TextBox runat="server" ID="PasswordConfirmTextBox" TextMode="Password" />
                <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="*"
                    ForeColor="Red" ControlToValidate="PasswordTextBox" ControlToCompare="PasswordConfirmTextBox" />
            </td>
        </tr>
        <tr>
            <td>EMail
            </td>
            <td>
                <asp:TextBox runat="server" ID="EmailTextBox" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" ControlToValidate="EmailTextBox"
                    ForeColor="Red" runat="server" ErrorMessage="*" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server"
                    ForeColor="Red" ErrorMessage="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="RegisterButton" Text="Register" runat="server" OnClick="RegisterButton_Click" />
            </td>
        </tr>
    </table>
    <asp:Label runat="server" ID="LabelMessage" Font-Bold="true" />
</asp:Content>
