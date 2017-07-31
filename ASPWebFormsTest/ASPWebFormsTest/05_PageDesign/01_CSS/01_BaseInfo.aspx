<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._05_PageDesign._01_CSS._01_BaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование CSS стилей</title>
    <%--Подключения файла с CSS таблицей стилей--%>
    <link href="01_BaseInfo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <p>
        Таблица стилей CSS, может быть подключена к элементу управления с помощью свойства CssClass 
    </p>
    <form id="form1" runat="server">
        <div>
            <%--CssClass - атрибут, указывающий имя CSS правила, применяемого для элемента--%>
            <asp:Button Text="Кнопка" runat="server" CssClass="ButtonStyle" /> <br/>
            <asp:TextBox Text="Hello CSS!" runat="server" CssClass="TextBoxStyle" />
        </div>
    </form>
</body>
</html>
