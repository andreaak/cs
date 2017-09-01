<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="11_SqlDependency.aspx.cs"
    Inherits="ASPWebFormsTest._24_OutputCache._11_SqlDependency" %>

<%-- 
Страница будет кэшироваться на 60 секунд 
но автоматически удалиться из кэша если будут внесены изменения в таблицу Products 
Нужно добавить в Web.config раздел sqlCacheDependency
--%>
<%@ OutputCache Duration="60" VaryByParam="None" SqlDependency="Database:Products" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SqlDependency</title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        Для того, чтобы активировать кэширование для базы данных, необходимо запустить утилиту Aspnet_regsql.exe, 
        которая находится в директории %windir%\Microsoft.NET\Framework\FrameworkVersion .
        Утилиту следует запустить со следующими параметрами:
    </p>
    <p>
        <span class="code">aspnet_regsql.exe -C "Data Source=(localDB)\v11.0;Database='Путь к безе';Integrated Security=True" -ed -et -t [Имя таблицы]</span>
        [Имя таблицы] таблица, для которой будет включено кэширование.<br />
        <span class="code">aspnet_regsql.exe -C "Data Source=.\SQLEXPRESS2014;Database='D:\TestSites\www\aspwebforms.com\App_Data\Database.mdf';Integrated Security=True" -ed -et -t Products</span>
    </p>
    <p>
        Также нужно добавить в Web.config раздел sqlCacheDependency.
    </p>
    <p class="code">
        &lt;sqlCacheDependency&gt;<br/>
        &nbsp;&lt;databases&gt;<br/>
        &nbsp;&nbsp;&lt;!-- pollTime - интервал, с котором будет происходить проверка изменений в базе --&gt;<br/>
        &nbsp;&nbsp;&lt;add name=&quot;Database&quot; pollTime=&quot;1000&quot; connectionStringName=&quot;local&quot; /&gt;<br/>
        &nbsp;&lt;/databases&gt;<br/>
        &lt;/sqlCacheDependency&gt;
    </p>
    <p>
        <span class="code">&lt;%@ OutputCache Duration=&quot;60&quot; VaryByParam=&quot;None&quot; SqlDependency=&quot;Database:Products&quot; %&gt;</span>
    </p>
    <p>
        Страница будет кэшироваться на 60 секунд но автоматически удалиться из кэша если будут внесены изменения в таблицу Products 
    </p>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>

            <br />
            <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="DatabaseSqlDataSource">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="DatabaseSqlDataSource"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:local %>"
                ProviderName="System.Data.SqlClient"
                SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>

