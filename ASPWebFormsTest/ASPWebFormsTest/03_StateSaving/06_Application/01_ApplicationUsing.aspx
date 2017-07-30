<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_ApplicationUsing.aspx.cs" Inherits="ASPWebFormsTest._03_StateSaving._06_Application._01_ApplicationUsing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application #1</title>
</head>
<body>
    <p>
        Состояние приложение – механизм позволяющий хранить данные в контексте приложения. 
        Данные доступны одновременно для всех пользователей и находятся в памяти на протяжении жизни приложения. 
        Обычно используется для хранения глобальных значений или счетчиков. 
        Например, счетчиков указывающих сколько раз была запрошена страница или сколько сеансов создано с момента запуска приложения.
    </p>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="CounterLabel" runat="server" Text="0"></asp:Label>
            <br />
            <asp:Button ID="AddOneButton" runat="server" Text="+1" OnClick="AddOneButton_Click" />
            <asp:Button ID="AddTwoButton" runat="server" Text="+2" OnClick="AddTwoButton_Click" />
        </div>
    </form>
</body>
</html>
