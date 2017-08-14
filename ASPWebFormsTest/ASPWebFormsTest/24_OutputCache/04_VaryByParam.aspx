<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_VaryByParam.aspx.cs" Inherits="ASPWebFormsTest._24_OutputCache._04_VaryByParam" %>

<%@ OutputCache Duration="30" VaryByParam="DropDownCategories" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VaryByParam Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 style="color: red"><%=DateTime.Now.ToLongTimeString() %></h1>
            <%--
            Выпадающий список будет отправлять на сервер значение каждый раз 
            при смене выбранного пункта.
            Имя POST параметра, который будет отправлен на сервер - DropDownCategories
            --%>
            <asp:DropDownList runat="server"
                ID="DropDownCategories"
                DataSourceID="NortwindDropDownDataSource"
                AutoPostBack="true"
                DataTextField="CategoryName"
                DataValueField="CategoryID">
            </asp:DropDownList>

            <br />
            <br />

            <asp:SqlDataSource ID="NortwindDropDownDataSource"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:NorthwindConnection %>"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT [CategoryID], [CategoryName] FROM [Categories]"></asp:SqlDataSource>

            <asp:GridView ID="GridView"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="ProductID"
                DataSourceID="NorthwindDataSource">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False"
                        ReadOnly="True" SortExpression="ProductID" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="NorthwindDataSource"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:NorthwindConnection %>"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT [ProductID], [ProductName], [UnitPrice] FROM [Products] WHERE CategoryID=@CategoryId">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownCategories" Name="CategoryId" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
