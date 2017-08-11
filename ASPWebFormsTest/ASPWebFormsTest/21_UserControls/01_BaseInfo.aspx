<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._21_UserControls._01_BaseInfo" %>

<%-- 
<!--Регистрация пользовательского элемента управления для WebSite-->
<%@ Register TagName="_01_UserControl" TagPrefix="test" Src="01_UserControl.ascx" %>
--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <strong>UserControl</strong> (пользовательский элемент управления) - это составной контрол, 
        который создается по принципу ASP.NET страницы 
        и может состоять из обычных серверных элементов управления и разметки.
    </p>
    <p>
        Пользовательские элементы управления используются для того, 
        чтобы избавится от дублирования разметки и логики на разных страницах одного веб приложения.
        Например, необходимо на разных странах выводить новостную ленту, 
        вместо дублирования кода разметки и логики получения новостей из базы на каждой странице,
        можно перенести эту логику в ascx файл, и использовать его на всех страницах.
    </p>
    <p>
        Отличия UserControl от страницы:
    </p>
    <ul>
        <li>Расширение файла пользовательского элемента управления .ascx</li>
        <li>Для настройки используется директива @Control</li>
        <li>Не может работать сам по себе, должен быть встроен в страницу.</li>
        <li>Пользовательский элемент не использует элементы html, body, form</li>
    </ul>
    <p>Регистрация контрола на странице </p>
    <ul>
        <li>Указание директивы Register на необходимой странице</li>
    </ul>
    <p class="code">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;%@ Register TagName="MyControl" TagPrefix="test" Src="~/WebUserControl.ascx" %&gt;</p>
    <ul>
        <li>Регистрация контрола для всех страниц через файл web.config</li>
    </ul>
    <p class="code">
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&lt;system.web&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;compilation debug="true" targetFramework="4.0" /&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&lt;pages&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;controls&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;add tagName="MyControl" tagPrefix="test" src="~/Controls/WebUserControl.ascx"/&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;/controls&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;/pages&gt;<br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &lt;/system.web&gt;
    </p>
    <ul>
        <li>Также, зарегистрировать контрол можно перенеся его из окна Solution Explorer 
            в дизайнер страницы.</li>
    </ul>
    <p>
        Каждое открытое свойство контрола может быть использовано как атрибут на странице, 
        которая использует контрол.
        Определив свойство в классе контрола появляется возможность 
        установить значение данного свойства декларативно(в разметке *.aspx файла через атрибут) 
        и программно(в .aspx.cs файле).
    </p>
    <p>
        Каждое событие созданное в контроле может быть использовано в разметке страницы, 
        которая использует контрол.<br />
        Наличие события в codebehind контрола, позволяет декларативно создать обработчик 
        для события через атрибут <strong>On[имя_события]</strong>
        Например, если событие в контроле называется Test, то на странице, 
        чтобы декларативно создать обработчик на это событие, 
        необходимо задать значение атрибута OnTest.
    </p>
    <p>
        Для того что бы программно создать элемент управления, необходимо сделать следующее:
    </p>
    <ul>
        <li>Определить директиву Reference на странице, которая будет динамически создавать контрол. 
            В директиве указать путь на создаваемый контрол.</li>
        <li>Вместо вызова конструктора пользовательского контрола 
            необходимо вызвать метод LoadControl.</li>
        <li>Созданный экземпляр контрола необходимо добавить, 
            как дочерний элемент другого контрола существующего на странице.</li>
    </ul>
</body>
</html>
