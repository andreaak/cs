<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ImageField.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._03_ImageField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView ImageField Columns</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Название браузера" />
                    <%--ImageField - колонка для показа изображений--%>
                    <asp:ImageField DataImageUrlField="ImagePath" HeaderText="Логотип" />

                    <asp:BoundField DataField="Url" HeaderText="Ссылка для скачивания" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
