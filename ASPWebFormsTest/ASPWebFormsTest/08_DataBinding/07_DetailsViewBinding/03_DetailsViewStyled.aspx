<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_DetailsViewStyled.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._07_DetailsViewBinding._03_DetailsViewStyled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DetailsView #3</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--AutoGenerateRows="True" позволяет контролу сформировать структуру на основе всех значений, 
                которые возвращает источник данных--%>
            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="300" AutoGenerateRows="True"
                CellPadding="4" DataKeyNames="ID" AllowPaging="true" DataSourceID="SqlDataSource1"
                ForeColor="#333333" GridLines="None">
                <%--Элементы определяющие стиль DetailsView--%>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <EditRowStyle BackColor="#999999" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DatabaseConnection %>"
                SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
