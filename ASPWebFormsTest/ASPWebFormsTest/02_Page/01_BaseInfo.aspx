<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._02_Page._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>

    <p>Все веб формы являются экземплярами класса System.Web.Ui.Page. Основные свойства класса Page.</p>
    <ul>
        <li>Session</li>
        <li>Application</li>
        <li>Cache</li>
        <li>Request</li>
        <li>Response</li>
        <li>Server</li>
        <li>User</li>
        <li>Trace</li>
    </ul>
    <p>
        <strong>Request</strong> - экземпляр класса System.Web.HttpRequest. 
        Этот объект представляет свойства и значения HTTP запроса, вызвавшие загрузку страницы.
    </p>
    <p><strong>Response</strong> - экземпляр класса System.Web.HttpResponse представляющий ответ сервера на запрос клиента.</p>
    <p>
        <strong>Server</strong> - экземпляр класса System.Web.HttpServerUtility. 
        Этот класс предоставляет разнообразные вспомогательные методы и свойства.
    </p>

    <h2>Этапы обработки web-форм</h2>
    <ol>
        <li>Запрос страницы.(Page Request)</li>
        <li>Старт(подготовка свойств страницы). (Start)</li>
        <li>Инициализация структуры страницы. (Initialization)           
            <ul>
                <li>PreInit</li>
                <li>Init</li>
                <li>InitComplete</li>
            </ul>
        </li>
        <li>Загрузка. (Load)
            <ul>
                <li>PreLoad</li>
                <li>Load</li>
            </ul>
        </li>
        <li>Обработка обратного запроса.(PostBackEvent Handling)</li>
        <li>Окончание загрузки
            <ul>
                <li>LoadComplete</li>
            </ul>
        </li>
        <li>Визуализация. (Rendering)
            <ul>
                <li>PreRender</li>
                <li>PreRenderComplete</li>
                <li>SaveStateComplete</li>
                <li>На странице и рекурсивно на каждом элементе управления вызывается метод Render, 
                    который преобразовывает объекты в HTML разметку.</li>
                <li>Отправка данных пользователю.</li>
            </ul>
        </li>
        <li>Очистка.(Unload)</li>
    </ol>
</body>
</html>
