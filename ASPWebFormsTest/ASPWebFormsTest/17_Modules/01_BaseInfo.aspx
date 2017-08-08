<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._17_Modules._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <strong>HTTP модуль</strong> - объект, который выполняет действие при каждом обращении к ASP.NET приложению.<br />
        Dispose - для удаления ресурсов, которые использовал модуль.<br />
        Init - для определения обработчиков событий жизненного цикла ASP.NET приложения.
    </p>

    <p>
        Для регистрации модуля используется следующий код в файле web.config
    </p>
    <p class="code">
        &lt;system.webServer&gt;<br />
        &nbsp; &lt;modules&gt;<br />
        &nbsp; &nbsp; &lt;add name="FirstModule" type="HttpModule.FirstHttpModule"/&gt;<br />
        &nbsp; &lt;/modules&gt;<br />
        &lt;/system.webServer&gt;
    </p>
    <p>
        name - имя HTTP модуля<br />
        type - тип, который реализовывает интерфейс System.Web.IHttpModule 
        и создает обработчики на события жизненного цикла приложения.
    </p>

    <p>Возможные примеры использования модулей: </p>
    <ul>
        <li>Безопасность - Авторизация и аутентификация пользователей</li>
        <li>Статистика - анализ входящих запросов, замер производительности.</li>
        <li>Добавление пользовательских заголовков в ответ.</li>
    </ul>
</body>
</html>
