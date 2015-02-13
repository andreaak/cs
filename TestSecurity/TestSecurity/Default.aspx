<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestSecurity.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:LoginView ID="LoginView1" runat="server" ViewStateMode="Disabled">
        <LoggedInTemplate>
            Content for logged users 
        </LoggedInTemplate>
        <AnonymousTemplate>
            <h1>Guests content</h1>
            <h2>Predefined users</h2>
            <div>
                <div style="float: left; width:10%; margin-right: 4px;" >
                    <div style="font-weight: bold;">Login</div>
                    <div>admin</div> 
                </div>
                <div style="float: left; width:10%; margin-right: 4px;" >
                    <div style="font-weight: bold;">Password</div>
                    <div>admin!</div> 
                </div>
            </div>
           <br/>
            <h2>Predefined roles</h2>
            <div> 
                <div>Admin</div> 
                <div>Manager</div> 
                <div>SuperUser</div> 
            </div>
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Admin">
                <ContentTemplate>
                    <p>Content for Admin</p>
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Manager">
                <ContentTemplate>
                    <p>Content for Manager</p>
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="SuperUser">
                <ContentTemplate>
                    <p>Content for SuperUser</p>
                </ContentTemplate>
            </asp:RoleGroup>
         </RoleGroups>
    </asp:LoginView>
</asp:Content>
