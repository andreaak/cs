<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_HyperlinkField.aspx.cs" Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._04_HyperlinkField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView HypreLinkField Columns</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Название браузера" />
                    <asp:ImageField DataImageUrlField="ImagePath" HeaderText="Логотип" />

                    <%--HyperLinkField - колонка содержащяя ссылки--%>
                    <asp:HyperLinkField
                        DataNavigateUrlFields="Url"
                        DataTextField="Name"
                        DataTextFormatString="Скачать {0}"
                        HeaderText="Ссылка для скачивания" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
