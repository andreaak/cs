<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_SimpleSqlDataBinding.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._05_SimpleSqlDataBinding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table>
                <tr>
                    <td>Продукт</td>
                    <td>
                        <%--В атрибутах DataTextField и DataValueField указаны имена колонок базы данных.--%>
                        <asp:DropDownList ID="DropDownList1" runat="server"
                            DataSourceID="CategoriesSqlDataSource"
                            DataTextField="CategoryName"
                            DataValueField="Id">
                        </asp:DropDownList>
                        <%--Источник данных--%>
                        <asp:SqlDataSource ID="CategoriesSqlDataSource"
                            runat="server"
                            ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                            SelectCommand="SELECT [Id], [CategoryName] FROM [Categories]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>Адрес доставки</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Заказ" />
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
