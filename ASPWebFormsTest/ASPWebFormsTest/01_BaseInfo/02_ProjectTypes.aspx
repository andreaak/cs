<%--Директива определяющая общие настройки страницы--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_ProjectTypes.aspx.cs" Inherits="ASPWebFormsTest._01_BaseInfo._02_ProjectTypes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <h1>Типы проектов:</h1>

    <h2>Web Application </h2>
    <ul>
        <li>Используется файл проекта *.sln и *.csproj.</li>
        <li>Весь код в проекте компилируется в одну сборку.</li>
        <li>Поддерживает работу с IIS и встроенный сервер ASP.NET Development Server.</li>
        <li>Поддерживает все возможности Visual Studio (refactoring, generics, etc.) и возможности ASP.NET 2.0 (master pages, membership and login, site navigation, themes, etc).</li>
    </ul>
    <br />

    <h2>Web Site</h2>
    <ul>
        <li>Нет файла проекта (структура проекта базируется на файловой системе).</li>
        <li>Каждая страница компилируется в отдельную библиотеку</li>
        <li>Динамическая компиляция без необходимости пересобрать весь сайт.</li>
        <li>Поддерживает работу с IIS и встроенный сервер ASP.NET Development Server.</li>
        <li>Различные модели кода Code-Behind и Single-File</li>
    </ul>
    <br />

    <h2>Рецепты</h2>

    <h4>Web Application</h4>
    <ul>
        <li>Необходимо мигрировать Visual Studio 2003 приложение в VS 2005</li>
        <li>Необходимо добавить pre-build и post-build шаги при компиляции приложения</li>
        <li>Необходимо построить веб приложение используя несколько веб проектов</li>
    </ul>

    <h4>Web Site</h4>
    <ul>
        <li>Необходимо редактировать любую директорию или файл без создания файла проекта</li>
        <li>Необходимо создавать по одной сборке на каждую страницу</li>
        <li>Необходима динамическая компиляция страницы сразу после редактирования без компиляции всего проекта</li>
        <li>Использование Single-File Code Model предпочитается больше чем Code-Behind</li>
    </ul>
</body>
</html>
