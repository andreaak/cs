<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_BullettedList.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls._06_BullettedList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BullettedList Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--BulletStyle="LowerAlpha" - стиль маркера для элементов списка--%>
            <asp:BulletedList BulletStyle="Disc" runat="server">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
                <asp:ListItem Text="Item 5" />
            </asp:BulletedList>
        </div>
        <br />
        <p>Translate into:<br />
            &lt;ul&gt;<br />
            &nbsp;&lt;li&gt;Item 1&lt;/li&gt;<br />
            &nbsp;&lt;li&gt;Item 2&lt;/li&gt;<br />
            &nbsp;&lt;li&gt;Item 3&lt;/li&gt;<br />
            &nbsp;&lt;li&gt;Item 4&lt;/li&gt;<br />
            &nbsp;&lt;li&gt;Item 5&lt;/li&gt;<br />
            &lt;/ul&gt;
        </p>
        <h2>Change Style</h2>
        <div>
            <asp:BulletedList runat="server" ID="BullettedList1" BulletImageUrl="image.gif">
                <asp:ListItem Text="Internet Explorer" Value="1" />
                <asp:ListItem Text="Mozilla Firefox" Value="2" />
                <asp:ListItem Text="Opera" Value="3" />
                <asp:ListItem Text="Chrome" Value="4" />
                <asp:ListItem Text="Safari" Value="5" />
            </asp:BulletedList>
            <p>Стиль списка:</p>
            <asp:RadioButtonList runat="server" ID="StylesList">
            </asp:RadioButtonList>
            <p>
                <asp:Button Text="Применить" runat="server" OnClick="Button_Click" />
            </p>
        </div>
    </form>
</body>
</html>
