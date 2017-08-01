<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_DetailsView.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding._01_DetailsView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DetailsView #1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False"
                DataKeyNames="ID" DataSourceID="SqlDataSource1" AllowPaging="True">
                <%--Каждый элемент BoundField привязывает элемент разметки к данным из источника данных--%>
                <Fields>
                    <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="Login" HeaderText="Login" />
                    <asp:BoundField DataField="Password" HeaderText="Password" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                </Fields>
            </asp:DetailsView>
            <%--строка подключения извлекается из файла web.config--%>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
