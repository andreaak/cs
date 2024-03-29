﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_ApplicationLifeCycle.aspx.cs" Inherits="ASPWebFormsTest._14_Application._02_ApplicationLifeCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>

    <h2>Пользователь запрашивает ресурс приложения с веб-сервера.</h2>

    <p>
        Для того чтобы при получении запроса, запрос обрабатывался в ASPX странице, а не статическом HTML,
        нужно добавить набор функций ASP.NET ISAPI

    </p>
    <p>
        Сайт будет работать в отдельном процессе ASP.NET.
        В этом процессе находится компонент ApplicationManager который конфигурирует, запускает
        и выгружает сайты из памяти.

    </p>
    <img class="image" src="Images/20.png" />

    <p>
        Жизненный цикл приложения ASP.NET начинается с запроса, отправленного обозревателем на веб-сервер 
        (для приложений ASP.NET, как правило, - службу IIS). ASP.NET является расширением ISAPI на веб-сервере. 
        Когда веб-сервер получает запрос, он проверяет расширение имени запрошенного файла, определяет, 
        какое расширение ISAPI должен обработать запрос, и затем передает запрос соответствующему расширению ISAPI. 
        ASP.NET обрабатывает расширения имени файла, сопоставленные ему, такие как aspx, ascx, ashx и asmx.
    </p>
    <img class="image" src="Images/21.png" />
    <p>
        Если расширение имени файла не сопоставлено ASP.NET, приложение не получит запрос. 
        Это важно понимать относительно приложений, использующих проверку подлинности ASP.NET. 
        Например, поскольку htm-файлы обычно не сопоставлены ASP.NET, 
        это приложение не выполняет проверку подлинности или авторизацию запросов htm-файлов. 
        Поэтому даже если файлы содержат статическое содержимое, если требуется, 
        чтобы приложение ASP.NET выполняло проверку подлинности, создайте файл с использованием расширения имени файла, 
        сопоставленного ASP.NET, например - aspx.<br />
        Если создается пользовательский обработчик для обработки отдельного расширения имени файла, 
        необходимо сопоставить расширение приложению ASP.NET в IIS, 
        а также зарегистрировать обработчик в файле Web.config приложения.<br />
    </p>

    <h2>ASP.NET получает первый запрос для приложения.</h2>

    <p>
        Когда ASP.NET получает первый запрос для какого-либо ресурса в приложении, 
        класс с именем ApplicationManager создает домен приложения. 
        Домены приложений обеспечивают изоляцию между приложениями для глобальных переменных 
        и позволяют каждому приложению выгружаться отдельно. В домене приложения создается экземпляр класса HostingEnvironment, 
        обеспечивающий доступ к сведениям о приложении, таким как имя папки, в которой хранится приложение.<br />
    </p>
    <p>
        Эти отношения показаны на следующей схеме:
    </p>
    <img class="image" src="Images/22.png" />

    <br />
    <img src="Images/app_domain.gif" />

    <p>
        ASP.NET также компилирует элементы верхнего уровня в приложении, если требуется, включая код приложения, в папке App_Code.
    </p>

    <h2>Для каждого запроса создаются базовые объекты ASP.NET.</h2>

    <p>
        После того как создан домен приложения и экземпляр объекта HostingEnvironment ASP.NET создает и инициализирует базовые объекты, 
        такие как HttpContext, HttpRequest и HttpResponse. 
        Класс HttpContext содержит отдельные объекты для каждого текущего запроса предложения, 
        такие как объекты HttpRequest и HttpResponse. 
        Объект HttpRequest содержит сведения о текущем запросе, включая файлы "cookies" и сведения об обозревателе. 
        Объект HttpResponse содержит ответ, который отправляется клиенту, включая все отображенные выводы и файлы "cookies".
    </p>

    <h2>Объект HttpApplication, назначен запросу</h2>

    <p>
        После того как все объекты приложения инициализированы, приложение запускается созданием экземпляра класса HttpApplication. 
        Если приложение содержит файл Global.asax, вместо этого ASP.NET создает экземпляр класса Global.asax, 
        который происходит из класса HttpApplication, и использует производный класс для представления приложения.
    </p>
    <p>
        При первом запросе в приложении страницы или процесса ASP.NET создается новый экземпляр HttpApplication. 
        Однако в целях максимальной производительности экземпляры HttpApplication могут повторно использоваться для нескольких запросов.
    </p>
    <p>
        При создании экземпляра класса HttpApplication создаются также любые сконфигурированные модули. 
        Например, если в приложении сконфигурировано такое действие, ASP.NET создает модуль SessionStateModule. 
        После создания всех сконфигурированных модулей вызывается метод Init класса HttpApplication.<br />
    </p>
    <p>
        Эти отношения показаны на следующей схеме:
    </p>
    <img src="Images/http_context.gif" />

    <h2>Запрос обрабатывается конвейером HttpApplication.</h2>

    <p>
        Во время обработки запроса класс HttpApplication создает события. Эти события особенно интересны для разработчиков, 
        которые хотят расширить класс HttpApplication.
    </p>
</body>
</html>
