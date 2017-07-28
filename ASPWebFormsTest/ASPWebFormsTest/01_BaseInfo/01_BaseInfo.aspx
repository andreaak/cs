<%--Директива определяющая общие настройки страницы--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._01_BaseInfo._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet"/>
</head>
<body>

    <h2>Преимущества ASP.NET</h2>

    <ul>
        <li>Интеграция с .NET Framework.</li>
        <li>Код ASP.NET компилируется а не интерпретируется.</li>
        <li>ASP.NET поддерживает несколько языков.</li>
        <li>ASP.NET обслуживается CLR.</li>
        <li>ASP.NET объектно-ориентированная технология.</li>
        <li>Поддержка множества устройств и браузеров.</li>
        <li>Простота развертывания и конфигурирования.</li>
    </ul>
    <br />

    <img src="01_BaseInfo.png" alt="" />
    <br />
    <p>
        <span class="bold">Веб сервер</span> ответственный за получение и обработку запросов полученных через HTTP. 
        Веб сервер обрабатывает запрос и отправляет ответ обратно веб браузеру. 
        После отправки ответа веб сервер закрывает соединении с браузером и освобождает все ресурсы, 
        которые были задействованы при обработке запроса.
    </p>
    <p>
        <span class="bold">Веб браузер</span> - независимое от платформы приложение для запроса и отображения HTML страниц. 
        В обязанности браузера входит отображение информацииполученной с сервера 
        и получение информации от пользователя для отправки ее обратно на сервер.
    </p>
    <p>
        <span class="bold">HTTP</span> - это протокол прикладного уровня для передачи данных от браузера к серверу и обратно. 
        HTTP сообщение обычно передаются между сервером и браузером через порт 80 
        или 443 при использовании Secure HTTP (HTTPS).
    </p>

    <p>
        <span class="bold">Request</span><br />
        При запросе страницы браузер отправляет текстовую команду на сервер.<br />
        GET /default.aspx HTTP/1.1<br />
        Host: www.example.com<br />
        <br />

        <span class="bold">GET</span> - HTTP глагол (метод или команда) описывающая действие, которое должен выполнить веб сервер.<br />
        /default.aspx-запрашиваемая на сервере страница.<br />
        <span class="bold">HTTP/1.1</span> - версия протокола<br />
        <span class="bold">Host: www.example.com</span> - заголовок. Доменное имя сайта к которому выполняется запрос. 
                Полезно в том случае если на сервере одновременно работает несколько веб приложений.
        <br />
    </p>

    <table border="1px">
        <caption><span class="bold">Глаголы HTTP</span></caption>
        <tr>
            <td>HTTP Глагол</td>
            <td>Описание</td>
        </tr>
        <tr>
            <td>OPTIONS</td>
            <td>Используется клиентским приложением для получения списка доступных глаголов.</td>
        </tr>
        <tr>
            <td>GET</td>
            <td>Получение данных с сервера.</td>
        </tr>
        <tr>
            <td>HEAD</td>
            <td>Получение метаданных (заголовков) ресурса. При данном запросе ресурс не возвращается.</td>
        </tr>
        <tr>
            <td>POST</td>
            <td>Отправка данных на сервер для обработки. Обычно данные введенные пользователем в форме на странице.</td>
        </tr>
        <tr>
            <td>PUT</td>
            <td>Позволяет клиенту создать ресурс по указанному URL (создать файл на сервере)</td>
        </tr>
        <tr>
            <td>DELETE</td>
            <td>Удаление ресурса на сервере</td>
        </tr>
        <tr>
            <td>CONNECT</td>
            <td>Команда для использования с прокси серверами</td>
        </tr>
    </table>

    <p>
        <span class="bold">Response</span><br />
        HTTP/1.1 200 OK<br />
        Server: Microsoft-IIS/6.0<br />
        Content-Type: text/html<br />
        Content-Length:36<br />
        &lt;html&gt;&lt;body&gt;Hello world&lt;/body&gt;&lt;/html&gt;<br />
        <br />

        <span class="bold">HTTP/1.1</span> - версия протокола<br />
        <span class="bold">200</span> - status code
        <br />
        <span class="bold">OK</span> - описание статуса<br />
        <span class="bold">Server: Microsoft-IIS/6.0</span> - заголовок хранящий версию сервера<br />
        <span class="bold">Content-Type: text/html</span> - заголовок с MIME типом ответа. 
        Данное значение нужно для того, 
        что бы браузер правильно интерпретировал данные полученные от сервера<br />
        <span class="bold">Content-Length:36</span> - размер тела ответа в байтах<br />
        <span class="bold">&lt;html&gt;&lt;body&gt;Hello world&lt;/body&gt;&lt;/html&gt;</span> - тело ответа<br />
    </p>

    <table border="1px">
        <caption><span class="bold">Группы статус кодов</span></caption>
        <tr>
            <td>Группа</td>
            <td>Описание</td>
        </tr>
        <tr>
            <td>1xx</td>
            <td>Информационные</td>
        </tr>
        <tr>
            <td>2xx</td>
            <td>Успешное завершение</td>
        </tr>
        <tr>
            <td>3xx</td>
            <td>Команды перенаправлений</td>
        </tr>
        <tr>
            <td>4xx</td>
            <td>Клиентские ошибки</td>
        </tr>
        <tr>
            <td>5xx</td>
            <td>Серверные ошибки</td>
        </tr>
    </table>
    
</body>
</html>
