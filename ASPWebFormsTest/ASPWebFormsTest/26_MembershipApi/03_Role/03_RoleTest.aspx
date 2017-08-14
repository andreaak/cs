<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="03_RoleTest.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._03_Role._03_RoleTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Uncomment &lt;forms loginUrl="~/26_MembershipApi/03_Role/Account/Login.aspx"&gt;&lt;/forms&gt; in Web.config
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <% if (User.IsInRole("Admin"))
        {%>
           
        <h1>Administrator's content</h1>
     
        <%} else if (User.IsInRole("Manager"))
        {%>
           
        <h1>Manager's content</h1>
     
        <%} else if (User.Identity.IsAuthenticated)
        {%>
           
            <h1>Logged user's content</h1>
     
        <%} else
        {%>
            <h1>Guest's content</h1>
        
            <div>
                <div style="float: left; width:5%; margin-right: 4px;" >
                    <div style="font-weight: bold;">Login</div>
                    <div>admin</div> 
                    <div>manager</div> 
                    <div>user</div> 
                </div>
                <div style="float: left; width:5%; margin-right: 4px;" >
                    <div style="font-weight: bold;">Password</div>
                    <div>qwerty</div> 
                    <div>qwerty</div> 
                    <div>qwerty</div> 
               </div>
            </div>
    
        <%}%>

</asp:Content>
