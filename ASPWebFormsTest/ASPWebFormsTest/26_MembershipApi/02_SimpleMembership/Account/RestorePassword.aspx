<%@ Page Title="" Language="C#" MasterPageFile="../Site.Master" AutoEventWireup="true" CodeBehind="RestorePassword.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._02_SimpleMembership.Account.RestorePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--
        Элемент управления для восстановления пароля
        Для его работы в файл web.config добавлены настройки для отправки сообщений через SMTP
        --%>
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server"></asp:PasswordRecovery>

</asp:Content>

