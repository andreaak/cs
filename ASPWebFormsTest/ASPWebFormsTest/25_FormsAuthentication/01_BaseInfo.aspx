<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._25_FormsAuthentication._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <strong>Аутентификация</strong> - процедура проверки подлинности пользователя 
        путем проверки его логина и пароля в базе данных.
    </p>
    <p>
        <strong>Авторизация</strong> - процедура присвоения аутентифицированному 
        пользователю прав на выполнение определенного действия.
    </p>
    <img src="authentication.png" />
    <p>
        <strong>Аутентификация с помощью форм</strong> основана на билетах(ticket). 
        Когда пользователь входит на сайт он получает билет с базовой информацией о себе. 
        Билет сохраняется в защищенном cookie-наборе. 
        При каждом последующем запросе билет будет проверятся на сервере.
    </p>
    <p>
        FormsAuthentication - Основной класс для взаимодействия с системой аутентификации 
        с помощью форм
    </p>
    <img src="cookie.png" />

    <h4>Реализация аутентификации </h4>
    <ol>
        <li>Добавить настройки в web.config
            <p class="code">
                &lt;authentication mode="Forms"&gt;<br />
                &nbsp; &lt;forms loginUrl="~/25_FormsAuthentication/03_Login.aspx"&gt;&lt;/forms&gt;<br />
                &lt;/authentication&gt;
            </p>
        </li>
        <li>Разрешить анонимный доступ к веб приложению (по умолчанию разрешен)
            <div>
                <img src="anonim_access.png" />
            </div>
        </li>
        <li>Реализовать страницу входа.</li>
    </ol>

    <h4>Механизмы аутентификации запросов IIS</h4>
    <ul>
        <li>Anonimus - рабочий процесс использует заданную учетную запись.</li>
        <li>Basic - Браузер запрашивает логин и пароль, после чего, 
            рабочий процесс работает от имени соответствующей учетной записи. 
            Имя и пароль передаются в виде текста закодированного в Base64.</li>
        <li>Digist - тоже что и Basic но с шифрованием.</li>
        <li>Windows Integrated - заключается в обмене информацией между сервером 
            и браузером пользователя, при этом пользователю, логин и пароль вводить не нужно,
            для этого пользователь должен иметь учетную запить на сервере 
            или в доверяемом домене.</li>
    </ul>

    <h4>Свойства класса FormsAuthentication</h4>
    <table>
        <tr>
            <th>Имя свойства</th>
            <th>Описание</th>
        </tr>
        <tr>
            <td>CookieDomain</td>
            <td>Возвращает домен, заданный для аутентификационного билета</td>
        </tr>
        <tr>
            <td>CookieMode</td>
            <td>Указывает на то где будет храниться билет – в cookies или в адресной строке</td>
        </tr>
        <tr>
            <td>CookiesSupported</td>
            <td>Возвращает true если текущий запрос поддерживает cookies</td>
        </tr>
        <tr>
            <td>DefaultURL</td>
            <td>Страница, на которую нужно вернуться в случае успешной аутентификации</td>
        </tr>
        <tr>
            <td>EnableCrossAppRedirects</td>
            <td>Указывает, разрешено ли перенаправлять пользователя к другому Web-приложению</td>
        </tr>
        <tr>
            <td>FormsCookieName</td>
            <td>Возвращает заданное в конфигурационном файле имя cookie, используемое в текущем приложении; по умолчанию имеет значение ASPXAUTH</td>
        </tr>
        <tr>
            <td>FormsCookiePath</td>
            <td>Возвращает заданный в конфигурационном файле путь к файлу cookie, используемый в текущем приложении; по умолчанию имеет значение “/”</td>
        </tr>
        <tr>
            <td>LoginUrl</td>
            <td>Адрес страницы входа</td>
        </tr>
        <tr>
            <td>RequireSSL</td>
            <td>Указывает, обязательно ли передавать cookie через HTTPS-соединение</td>
        </tr>
        <tr>
            <td>SlidingExpiration</td>
            <td>Если true это означает, что счетчик времени текущей сессии автоматически сбрасывается на начальное значение в момент запроса от пользователя</td>
        </tr>
    </table>

    <h4>Методы класса FormsAuthentication</h4>
    <table>
        <tr>
            <th>Имя метода</th>
            <th>Описание</th>
        </tr>
        <tr>
            <td>Authenticate</td>
            <td>Сравнивает данные с данными в секции &lt;credentials&gt;</td>
        </tr>
        <tr>
            <td>Decrypt</td>
            <td>Получает действительный билет и возвращает экземпляр класса FormAuthenticationTicket</td>
        </tr>
        <tr>
            <td>Encrypt</td>
            <td>Генерирует строку символов представляющую билет</td>
        </tr>
        <tr>
            <td>GetAuthCookie</td>
            <td>Создание билета для заданного имени пользователя</td>
        </tr>
        <tr>
            <td>GetRedirectUrl</td>
            <td>Возвращает адрес к странице на которой был пользователь до запроса страницы аутентификации</td>
        </tr>
        <tr>
            <td>HashPasswordForStoringInConfigFile</td>
            <td>Метод для хеширования пароля по указанному алгоритму</td>
        </tr>
        <tr>
            <td>RedirectToLoginPage</td>
            <td>Перенаправляет пользователя к странице для аутентификации.  По умолчанию default.aspx</td>
        </tr>
        <tr>
            <td>RedirectFromLoginPage</td>
            <td>Перенаправляет аутентифицированного пользователя</td>
        </tr>
        <tr>
            <td>RenewTicketIfOld</td>
            <td>Обновляет время жизни билета</td>
        </tr>
        <tr>
            <td>SetAuthCookie</td>
            <td>Создает билет и присоединяет его к текущему запросу, не перенаправляет пользователя</td>
        </tr>
        <tr>
            <td>SignOut</td>
            <td>Удаляет билет</td>
        </tr>
    </table>
</body>
</html>
