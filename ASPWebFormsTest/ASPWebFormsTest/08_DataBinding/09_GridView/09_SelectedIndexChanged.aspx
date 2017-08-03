<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="09_SelectedIndexChanged.aspx.cs" Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._09_SelectedIndexChanged" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView SelectedIndexChanged Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="EmployeeID" DataSourceID="NorthwindDataSource" ForeColor="#333333"
                GridLines="None"
                AllowPaging="true" PageSize="5">
                <%--Настройка колонок--%>
                <Columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="./Images/arrow.png" ShowSelectButton="true" />
                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" InsertVisible="False"
                        ReadOnly="True" SortExpression="EmployeeID" />
                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="TitleOfCourtesy" HeaderText="TitleOfCourtesy" SortExpression="TitleOfCourtesy" />
                    <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" SortExpression="BirthDate" />
                </Columns>
                <%--Настройка стиля--%>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <%--Источник данных--%>
            <asp:SqlDataSource ID="NorthwindDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnection %>"
                SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Title], [TitleOfCourtesy], [BirthDate] FROM [Employees]"></asp:SqlDataSource>

            <asp:Label runat="server" ID="Output"></asp:Label>
        </div>
    </form>
</body>
</html>
