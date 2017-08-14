<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="02_FormsTest.aspx.cs" Inherits="ASPWebFormsTest._25_FormsAuthentication._02_FormsTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Uncomment &lt;forms loginUrl="~/25_FormsAuthentication/02_Login.aspx"&gt;&lt;/forms&gt; in Web.config
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Меню--%>
    <a href="02_FormsTest.aspx">Home 2</a> | 
    <a href="02_Private.aspx">Private 2</a><br />
    Default Page 2 
</asp:Content>
