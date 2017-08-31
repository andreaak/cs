<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_SimpleSqlDataBinding.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._05_SimpleSqlDataBinding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet"/>
</head>
<body>

    <h2>Привязка к данным в базе</h2>
    <p>
        При работе с базой данных строку подключения к базе следует хранить 
        в файле web.config в секции &lt;connectionStrings&gt;<br />
        <span class="code">ConnectionString</span> - свойство, для определения строки подключения.<br />
        <span class="code">$ConnectionStrings: &lt;имя строки подключения&gt;</span> - выражение позволяющее 
        прочитать значение строки подключения из файла web.config в файле разметки<br />
        Для получения доступа к строке подключения в code-behind используется следующий код : 
        <span class="code">ConfigurationManager.ConnectionStrings[&ldquo;Имя_строки_в_web_config&rdquo;].ConnectionString</span>;
        <br />
        <span class="code">SelectCommand</span> - запрос к базе для получения данных.
    </p>

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
                            ConnectionString="<%$ ConnectionStrings:local %>"
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
