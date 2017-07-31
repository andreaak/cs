<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        Серверные элементы управления всегда генерируются с помощью синтаксиса &lt;asp:controlname&gt;.
        <br />
        Ограничения серверных элементов управления:
    </p>
    <ul>
        <li>Каждый элемент управления должен иметь закрывающий дескриптор или /&gt; и иметь атрибут runat=&rdquo;server&rdquo;&nbsp;</li>
        <li>Все элементы должны находиться внутри серверной формы &lt;form runat=&rdquo;server&rdquo;&gt;&lt;/form&gt;</li>
    </ul>
</body>
</html>
