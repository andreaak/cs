<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrivatePage.aspx.cs" Inherits="TestSecurity.Private.PrivatePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <% if (User.IsInRole("Admin"))
        {%>
           
        <h1>Administrators content</h1>
     
        <%} else if (User.IsInRole("Manager"))
        {%>
           
        <h1>Managers content</h1>
     
        <%} else if (User.Identity.IsAuthenticated)
        {%>
           
            <h1>Logged user's content</h1>
     
        <%}%>

</asp:Content>
