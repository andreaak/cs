<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_DetailsViewEditable.aspx.cs" Inherits="ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding._02_DetailsViewEditable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DetailsView #2</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px"
                AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="SqlDataSource1"
                AllowPaging="True">
                <Fields>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="Login" HeaderText="Login" />
                    <asp:BoundField DataField="Password" HeaderText="Password" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
                </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                DeleteCommand="DELETE FROM [Users] WHERE [ID] = @ID"
                InsertCommand="INSERT INTO [Users] ([Login], [Password], [Email]) VALUES (@Login, @Password, @Email)"
                SelectCommand="SELECT * FROM [Users]"
                UpdateCommand="UPDATE [Users] SET [Login] = @Login, [Password] = @Password, [Email] = @Email WHERE [ID] = @ID">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Login" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Login" Type="String" />
                    <asp:Parameter Name="Password" Type="String" />
                    <asp:Parameter Name="Email" Type="String" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
