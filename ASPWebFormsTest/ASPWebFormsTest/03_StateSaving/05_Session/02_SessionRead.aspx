<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_SessionRead.aspx.cs" Inherits="ASPWebFormsTest._03_StateSaving._05_Session._02_SessionRead" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Чтение данных из сеанса</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="OutputLabel" Text="Сеанс не содержит ключ 'Key'" runat="server" />
            <asp:Button ID="ClearButton" Text="Очистить сеанс" runat="server" OnClick="ClearButton_Click" />
        </div>
    </form>
</body>
</html>
