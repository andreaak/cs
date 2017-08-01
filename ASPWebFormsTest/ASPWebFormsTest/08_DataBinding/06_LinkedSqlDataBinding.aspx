<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_LinkedSqlDataBinding.aspx.cs" 
    Inherits="ASPWebFormsTest._08_DataBinding._06_LinkedSqlDataBinding" %>

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
                    <td>Категория</td>
                    <td>
                        <asp:DropDownList
                            ID="CategoriesDropDownList"
                            runat="server"
                            AutoPostBack="True"
                            DataSourceID="CategoriesSqlDataSource"
                            DataTextField="CategoryName"
                            DataValueField="Id">
                        </asp:DropDownList>

                        <asp:SqlDataSource ID="CategoriesSqlDataSource"
                            runat="server"
                            ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                            SelectCommand="SELECT [Id], [CategoryName] FROM [Categories]"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>Товар</td>
                    <td>
                        <asp:DropDownList ID="ProductsDropDownList"
                            runat="server" DataSourceID="ProductsSqlDataSource"
                            DataTextField="Name"
                            DataValueField="Id">
                        </asp:DropDownList>

                        <asp:SqlDataSource ID="ProductsSqlDataSource"
                            runat="server"
                            ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                            SelectCommand="SELECT [Id], [Name] FROM [Products] WHERE ([CategoryId] = @CategoryId)">

                            <SelectParameters>
                                <asp:ControlParameter
                                    ControlID="CategoriesDropDownList"
                                    Name="CategoryId"
                                    PropertyName="SelectedValue"
                                    Type="Int32" />
                            </SelectParameters>

                        </asp:SqlDataSource>
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
                        <asp:Button ID="Button1" runat="server" Text="Сделать заказ" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
