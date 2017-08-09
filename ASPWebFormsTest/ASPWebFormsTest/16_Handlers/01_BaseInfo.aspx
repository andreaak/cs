<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._16_Handlers._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <strong>HTTP обработчик</strong> - &laquo;конечная точка&raquo;, которая выполняет обработку запроса 
        к ASP.NET приложению.<br />
        IsReusable - Возвращает значение, которое определяет может ли один и тот же 
        экземпляр использоваться для обработки нескольких запросов.<br />
        ProcessRequest - Метод выполняется при обращении к обработчику.
    </p>
    <img src="handlers_modules.png" />

    <p>
        Для регистрации обработчика используется следующий код в файле web.config
    </p>
    <p class="code">
        &lt;system.webServer&gt;<br />
        &nbsp; &lt;handlers&gt;<br />
        &nbsp; &nbsp; &lt;add name="MyFirstHandler" verb="GET" path="MyHandler.aspx" type="HttpHandlers.MyFirstHandler"/&gt;<br />
        &nbsp; &lt;/handlers&gt;<br />
        &lt;/system.webServer&gt;
    </p>
    <p>
        <strong>name</strong> - имя обработчика.<br />
        <strong>verb</strong> - HTTP глаголы на которые будет реагировать обработчик (например, GET, POST, PUT).<br />
        <strong>path</strong> - адрес, по которому будет доступен обработчик.<br />
        <strong>type</strong> - тип, который реализовывает интерфейс System.Web.IHttpHandler 
        и создаётся для обработки запроса.
    </p>
    <p>Возможные примеры использования обработчиков: </p>
    <ul>
        <li>Чтение изображений из базы данных</li>
        <li>Генерирование изображения для CAPTCHA</li>
        <li>Создание RSS ленты</li>
        <li>Обработка запросов к файлам с определенным расширением.</li>
    </ul>
</body>
</html>
