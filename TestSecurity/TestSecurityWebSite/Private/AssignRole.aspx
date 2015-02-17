<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AssignRole.aspx.cs" Inherits="Private_AssignRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Assign Roles</legend>
        <ol>
            <li>
                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User Login</asp:Label>
                <asp:TextBox runat="server" ID="UserName" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                <asp:CustomValidator ID="CustomValidatorLogin" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="User is not exist" OnServerValidate="CustomValidatorLogin_ServerValidate"/>
            </li>
            <li>
                <asp:Label ID="Label2" runat="server" AssociatedControlID="ListBoxRoles">Roles</asp:Label>
                <asp:ListBox runat="server" ID="ListBoxRoles" SelectionMode="Multiple"/>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="" ControlToValidate="ListBoxRoles" CssClass="field-validation-error" ErrorMessage="Atleast one member required"  />
            </li>
        </ol>
        <asp:Button ID="Button1" runat="server" CommandName="Assign" Text="Set Role" OnClick="Button1_Click" />
    </fieldset>
</asp:Content>

