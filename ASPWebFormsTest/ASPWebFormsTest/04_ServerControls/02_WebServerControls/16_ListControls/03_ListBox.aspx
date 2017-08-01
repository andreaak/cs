<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_ListBox.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls._03_ListBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ListBox</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListBox runat="server">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
            </asp:ListBox>
        </div>
        <br />

        <p>
            Translate into:<br />
            &lt;select size="4" name="ctl02"&gt;<br />
            &nbsp;&lt;option value="Item 1"&gt;Item 1&lt;/option&gt;<br />
            &nbsp;&lt;option value="Item 2"&gt;Item 2&lt;/option&gt;<br />
            &nbsp;&lt;option value="Item 3"&gt;Item 3&lt;/option&gt;<br />
            &nbsp;&lt;option value="Item 4"&gt;Item 4&lt;/option&gt;<br />
            &lt;/select&gt;
        </p>
        <br />

        <h2>Add ListItem</h2>
        <div>
            <asp:ListBox ID="BrowsersListBox" runat="server">
                <asp:ListItem Text="Mozilla" />
                <asp:ListItem Text="Chrome" />
            </asp:ListBox>
            <asp:Panel runat="server" GroupingText="Добавление новго элемента">
                Text
                <asp:TextBox ID="ItemText" runat="server" />
                <br />
                Value
                <asp:TextBox ID="ItemValue" runat="server" />
                <br />
                <asp:Button Text="Добавить" runat="server" OnClick="AddButton_Click" />
            </asp:Panel>
        </div>
        <br />
        <h2>Selection mode</h2>
        <div>
            <p>SelectionMode=&quot;Single&quot;</p>
            <asp:ListBox runat="server" Height="200" Width="200" SelectionMode="Single">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
            </asp:ListBox>

            <p>SelectionMode=&quot;Multiple&quot; (При выборе зажмите клавишу Ctrl или Shift)</p>
            <asp:ListBox runat="server" Height="200" Width="200" SelectionMode="Multiple">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
            </asp:ListBox>
        </div>
        <br />
        <h2>Remove items</h2>
        <div>
            <p>
                SelectionMode=&quot;Single&quot;
            </p>
            <asp:ListBox ID="ListBoxSingle" runat="server" Height="200" Width="200" SelectionMode="Single">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
            </asp:ListBox>
            <br />
            <asp:Button ID="RemoveSingleButton" Text="Удалить" runat="server" OnClick="RemoveSingleButton_Click" />
            <p>
                SelectionMode=&quot;Multiple&quot; (При выборе зажмите клавишу Ctrl или Shift)
            </p>
            <asp:ListBox ID="ListBoxMultiple" runat="server" Height="200" Width="200" SelectionMode="Multiple">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
            </asp:ListBox>
            <br />
            <asp:Button ID="RemoveMultipleButton" Text="Удалить" runat="server" OnClick="RemoveMultipleButton_Click" />
        </div>
    </form>
</body>
</html>
