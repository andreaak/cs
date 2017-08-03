<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_TemplateField.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._05_TemplateField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView TemplateField Columns</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:ImageField DataImageUrlField="ImagePath" HeaderText="Логотип" />

                    <asp:TemplateField HeaderText="Скачать браузер">
                        <ItemTemplate>
                            <h3><%# Eval("Name") %></h3>
                            <a href='<%# Eval("Url") %>'>скачать</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
