<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._05_PageDesign._02_Themes.Default"
    Theme="Spring" %>

<!--Установка темы для текущей страницы-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Использование ASP.NET темы #1</title>
</head>
<body>
    <p>
        ASP.NET Тема - решение реализующееся на стороне сервера для определения наборов атрибутов стилей, 
        которые можно применять к элементам управления на многих страницах.
        Файлы тем должны находится в папке App_Theme в корневой директории сайта.   
    </p>
    <p>
        Особенности ASP.NET тем:
    </p>
    <ul>
        <li>Темы основаны на элементах управления.&nbsp;</li>
        <li>Темы применяются на стороне сервера.&nbsp;</li>
        <li>Темы могут применятся посредством конфигурационных файлов.&nbsp;</li>
        <li>Темы не выполняют такое каскадирование, как CSS.</li>
    </ul>

    <form id="form1" runat="server">
        <div>
            <asp:TextBox Text="Hello ASP.NET Theme!" runat="server" />
            <asp:Button Text="Кнопка" runat="server" />
        </div>

        <div>
            <p>
                Если в теме встречается несколько стилей для одного и того же серверного элемента управления,
                для определения стиля следует добавить атрибут SkinID в файл темы и в элемент, который тему использует.
            </p>
            <asp:TextBox ID="TextBox1" runat="server" Text="Hello ASP.NET Theme!" SkinID="TextBoxUsual" />
            <asp:Button ID="Button1" Text="Кнопка" runat="server" />
        </div>
    </form>
</body>
</html>
