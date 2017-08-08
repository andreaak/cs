<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_WriteConfig.aspx.cs" Inherits="ASPWebFormsTest._15_ConfigFiles._04_WriteConfig._04_WriteConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button Text="Прочитать данные из web.config" runat="server" OnClick="ReadButton_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" />
        <br />
        <br />
        <asp:Button Text="Добавить запись в web.config" runat="server" OnClick="AddButton_Click" />
    </div>
    </form>
</body>
</html>
