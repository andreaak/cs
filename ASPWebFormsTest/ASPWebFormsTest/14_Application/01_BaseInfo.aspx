<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._14_Application._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>

    <p>
        Основные компоненты<br />
        <strong>HTTP.sys</strong> - прослушивает входящие HTTP запросы.<br />
        <strong>WWW Service(World Wide Web Publishing Service)</strong> - Адаптер для HTTP.sys<br />
        <strong>WAS(Windows Process Activation Service)</strong> - управляет рабочими процессами и конфигурирует пул приложений
    </p>
    <p>
        <strong>HTTP.sys</strong> - драйвер работающий в режиме ядра. 
        Его задача прослушивать все входящие HTTP запросы и вносить их в очередь соответствующего пула приложения.<br />
        Преимущества:
    </p>
    <ul>
        <li>Кэширование в режиме ядра. Ответы хранимые в кэше отправляются без переключения в пользовательский режим.</li>
        <li>Очередь на уровне ядра. Запрос находиться в очереди, пока рабочий процесс не начнет его обработку.</li>
    </ul>
    <p>
        <strong>WWW Service</strong> - это адаптер для HTTP.sys. Этот компонент IIS ответственный за настройку HTTP.sys<br />
        В IIS 6.0 WWW Service был ответственным за управление рабочими процессами. 
        Так как он был напрямую связан с HTTP.sys, IIS6 мог обрабатывать только HTTP и HTTPS запросы.
    </p>
    <p>
        <strong>WAS</strong> - Управляет пулом приложений и рабочими процессами.<br />
        При старте WAS считывает информацию из файла ApplicationHost.config 
        и передает информацию адаптеру для того что бы наладить взаимодействие между WAS и слушателем протокола (например HTTP.sys).<br />
        В зависимости от адаптера система может обрабатывать другие протоколы, например, net.tcp.
        В предыдущих версиях IIS сервер мог обрабатывать только протоколы HTTP и HTTPS.
    </p>
    <p>
        <br />
        <strong>Модули</strong> - отдельные функциональные блоки, которые использует сервер для обработки запросов.<br />
        Модули можно поделить на следующие группы:
    </p>
    <ul>
        <li>HTTP</li>
        <li>Security</li>
        <li>Content</li>
        <li>Compression</li>
        <li>Caching</li>
        <li>Logging and Diagnostics</li>
        <li>Managed modules</li>
    </ul>


    <p>
        От пользователя приходит запрос к сайту и его принимает HTTP.sys
    </p>
    <img class="image" src="Images/02.png" />

    <p>
        Сообщение приходит первый раз. HTTP.sys должен понять для кого это сообщение предназначено.
        Он обращается к WWW Service для того чтобы получить настройки. WWW Service обращается к WAS.
    </p>
    <img class="image" src="Images/03.png" />

    <p>
        WAS обращается к ApplicationHost.config
    </p>
    <img class="image" src="Images/04.png" />

    <p>
        При этом он производит конфигурирование WWW Service и конфигурирование HTTP.sys
    </p>
    <img class="image" src="Images/05.png" />

    <p>
        После завершения конфигурирования в  HTTP.sys создается очередь входящих запросов,
        а также объект в котором будет находится кеширование вывода
    </p>
    <img class="image" src="Images/06.png" />

    <p>
        Пока конфигурировался HTTP.sys развернулось приложение, были загружены модули указанные в настройках,
        а также загружены библиотеки разработанные в рамках приложения

    </p>
    <img class="image" src="Images/07.png" />

    <p>
        После того как Application Pool будет запущен, он возмет сообщение из очереди, пропустит через ряд модулей,
        выполнит запуск обработчика и на выходе приложение отдаст HTML разметку.
    </p>
    <img class="image" src="Images/08.png" />

    <p>
        Если разметка отмечена как предназначенная для кеширования, то разметка будет помещена  в кеш вывода HTTP.sys и отдастся пользователю.
        Если от пользователя приходит повторный запрос к этой странице и страница помещена в кеш,
        то сервер сразу же отдаст ответ извлекая его из кеша.
    </p>
    <img class="image" src="Images/09.png" />
</body>
</html>
