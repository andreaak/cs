﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_RadioButtonList.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls._05_RadioButtonList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RadioButtonList Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Text="Item 1" />
                <asp:ListItem Text="Item 2" />
                <asp:ListItem Text="Item 3" />
                <asp:ListItem Text="Item 4" />
                <asp:ListItem Text="Item 5" />
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="Button1" Text="Отобразить выбраннй" runat="server" OnClick="Button_Click" />
            <br />
            <asp:Label ID="OutputLabel" runat="server" />
        </div>
        <br />
        <p>Translate into:<br />
            &lt;table id="RadioButtonList1"&gt;<br />
            &lt;tr&gt;<br />
            &lt;td&gt;&lt;input id="RadioButtonList1_0" type="radio" name="RadioButtonList1" value="Item 1" /&gt;&lt;label for="RadioButtonList1_0"&gt;Item 1&lt;/label&gt;&lt;/td&gt;<br />
            &lt;/tr&gt;&lt;tr&gt;<br />
            &lt;td&gt;&lt;input id="RadioButtonList1_1" type="radio" name="RadioButtonList1" value="Item 2" /&gt;&lt;label for="RadioButtonList1_1"&gt;Item 2&lt;/label&gt;&lt;/td&gt;<br />
            &lt;/tr&gt;&lt;tr&gt;<br />
            &lt;td&gt;&lt;input id="RadioButtonList1_2" type="radio" name="RadioButtonList1" value="Item 3" /&gt;&lt;label for="RadioButtonList1_2"&gt;Item 3&lt;/label&gt;&lt;/td&gt;<br />
            &lt;/tr&gt;&lt;tr&gt;<br />
            &lt;td&gt;&lt;input id="RadioButtonList1_3" type="radio" name="RadioButtonList1" value="Item 4" /&gt;&lt;label for="RadioButtonList1_3"&gt;Item 4&lt;/label&gt;&lt;/td&gt;<br />
            &lt;/tr&gt;&lt;tr&gt;<br />
            &lt;td&gt;&lt;input id="RadioButtonList1_4" type="radio" name="RadioButtonList1" value="Item 5" /&gt;&lt;label for="RadioButtonList1_4"&gt;Item 5&lt;/label&gt;&lt;/td&gt;<br />
            &lt;/tr&gt;<br />
            &lt;/table&gt;
        </p>
    </form>
</body>
</html>
