<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_BoundField.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._02_BoundField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView BoundField Columns</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Название браузера" />
                    <asp:BoundField DataField="ImagePath" HeaderText="Путь к логотипу" />
                    <asp:BoundField DataField="Url" HeaderText="Ссылка для скачивания" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
