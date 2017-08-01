<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_DropDownList.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls._02_DropDownList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DropDownList</title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList runat="server">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
                <asp:ListItem Text="Item 5" />
            </asp:DropDownList>
        </div>
        <br />
        <p>
            Translate into:<br />
            &lt;select name="DropDownList1" id="DropDownList1"&gt;<br />
            &nbsp;&lt;option value="1"&gt;Internet Explorer&lt;/option&gt;<br />
            &nbsp;&lt;option value="2"&gt;Mozilla Firefox&lt;/option&gt;<br />
            &nbsp;&lt;option value="3"&gt;Opera&lt;/option&gt;<br />
            &nbsp;&lt;option value="4"&gt;Chrome&lt;/option&gt;<br />
            &nbsp;&lt;option value="5"&gt;Safari&lt;/option&gt;<br />
            &lt;/select&gt;
        </p>
        <br />
        <h2>Get selected item</h2>
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Text="Internet Explorer" Value="1" />
                <asp:ListItem Text="Mozilla Firefox" Value="2" />
                <asp:ListItem Text="Opera" Value="3" />
                <asp:ListItem Text="Chrome" Value="4" />
                <asp:ListItem Text="Safari" Value="5" />
            </asp:DropDownList>
            <asp:Button Text="Подробнее" runat="server" OnClick="Button_Click" />
            <br />
        </div>
        <h2>Handle OnSelectedIndexChanged</h2>
        <p>
            AutoPostBack=“true” - элемент управления будет производить обратный вызов с помощью JavaScript кода.<br />
            OnSelectedIndexChanged–метод обработчик события смены выбранного элемента в списочном контроле.
        </p>
        <div>
            <%--AutoPostBack="true" - заставляет элемент управления делать обратный запрос с помощью JavaScript при смене пункта--%>
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="List_IndexChanged">
                <asp:ListItem Text="Internet Explorer" Value="1" />
                <asp:ListItem Text="Mozilla Firefox" Value="2" />
                <asp:ListItem Text="Opera" Value="3" />
                <asp:ListItem Text="Chrome" Value="4" />
                <asp:ListItem Text="Safari" Value="5" />
            </asp:DropDownList>
            <br />
        </div>
        <br />
        <br />
        <asp:Label ID="OutputLabel" runat="server" EnableViewState="false" />
    </form>
</body>
</html>
