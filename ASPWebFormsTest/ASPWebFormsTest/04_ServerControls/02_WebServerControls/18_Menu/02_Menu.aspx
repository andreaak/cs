<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_Menu.aspx.cs" 
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._18_Menu._02_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu пример использования</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Menu runat="server"
                Orientation="Horizontal"
                BackColor="#F7F6F3"
                DynamicHorizontalOffset="2"
                Font-Names="Verdana"
                Font-Size="0.8em"
                ForeColor="#7C6F57"
                StaticSubMenuIndent="10px">

                <%-- Структура пунктов меню --%>
                <Items>
                    <asp:MenuItem Text="Item 1">
                        <asp:MenuItem Text="Sub Menu 1"></asp:MenuItem>
                        <asp:MenuItem Text="Sub Menu 2"></asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="Item 2">
                        <asp:MenuItem Text="Sub Menu 1"></asp:MenuItem>
                        <asp:MenuItem Text="Sub Menu 2"></asp:MenuItem>
                        <asp:MenuItem Text="Sub Menu 3">
                            <asp:MenuItem Text="Sub Sub Menu 1"></asp:MenuItem>
                            <asp:MenuItem Text="Sub Sub Menu 2"></asp:MenuItem>
                        </asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="Item 3"></asp:MenuItem>
                    <asp:MenuItem Text="Item 4"></asp:MenuItem>
                </Items>

                <%--Стили для выпадающих пунктов меню--%>
                <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <DynamicMenuStyle BackColor="#F7F6F3" />
                <DynamicSelectedStyle BackColor="#5D7B9D" />

                <%--Стили для статических(постоянно видимых) пунктов меню--%>
                <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticSelectedStyle BackColor="#5D7B9D" />
            </asp:Menu>
        </div>
    </form>
</body>
</html>
