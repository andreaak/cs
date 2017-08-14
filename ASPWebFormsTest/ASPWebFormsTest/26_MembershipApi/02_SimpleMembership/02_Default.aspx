<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="02_Default.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._02_SimpleMembership.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        Uncomment &lt;forms loginUrl="~/26_MembershipApi/02_SimpleMembership/Account/Login.aspx"&gt;&lt;/forms&gt; in Web.config
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="float: left; width: 5%; margin-right: 4px;">
            <div style="font-weight: bold;">Login</div>
            <div>admin</div>
            <div>manager</div>
            <div>user</div>
        </div>
        <div style="float: left; width: 5%; margin-right: 4px;">
            <div style="font-weight: bold;">Password</div>
            <div>qwerty</div>
            <div>qwerty</div>
            <div>qwerty</div>
        </div>
    </div>
    Default Page 
</asp:Content>
