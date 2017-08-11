<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_InheritListControlTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls.DataBinding._04_InheritListControlTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls.DataBinding" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HyperLinkList</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <cc1:_04_InheritListControl ID="HyperLinkList1" runat="server" ForeColor="green">
                <asp:ListItem Text="Google" Value="http://google.com" />
                <asp:ListItem Text="Yandex" Value="http://ya.ru" />
                <asp:ListItem Text="Yahoo" Value="http://yhoo.com" />
            </cc1:_04_InheritListControl>

        </div>
    </form>
</body>
</html>
