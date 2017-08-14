<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._26_MembershipApi._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        Membership API - платформа представляющая полный набор функций 
        управления пользователями.
    </p>
    <ul>
        <li>Создание и удаление пользователей.</li>
        <li>Переустановка паролей и отправка новых паролей пользователям.</li>
        <li>Автоматическая генерация паролей для пользователей.</li>
        <li>Поиск пользователей в лежащем в основе хранилище данных.</li>
        <li>Набор элементов управления для регистрации, входа, восстановления пароля и т.д.</li>
        <li>Уровень абстракции, который делает приложение независимым 
            от типа источника данных.</li>
    </ul>
    <p>
        Membership - основной класс для взаимодействия с Membership API.<br />
        MembershipUser - класс представляет отдельного пользователя записанного 
        в хранилище данных Membership API<br />
        MembershipUserCollection - коллекция всех пользователей.<br />
        MembershipProvider - базовый класс для реализации собственного Membership API<br />
        SqlMembershipProvider - реализация MembershipProviderдля SQL базы данных.<br />
        ActiveDirectoryMembershipProvider - реализация MembershipProvider 
        для работы со службой Active Directory
    </p>
    <img src="membership.png" />
    <h4>Свойства класса Membership</h4>
    <p>
        ApplicationName - Строка идентифицирующая приложение.<br />
        EnablePasswordReset - Указывает на то поддерживает ли провайдер сброс пароля.<br />
        EnablePasswordRetrieval - Поддерживает ли провайдер восстановление пароля.<br />
        MaxInvalidPasswordAttempts - Максимальное количество попыток ввести пароль 
        перед блокированием пользователя.<br />
        MinRequiredNonAlphanumericCharacters - Минимальное количество знаков 
        не являющихся цифрой или буквой.<br />
        MinRequiredPasswordLength - Минимальная длина пароля.<br />
        PasswordStrengthRegularExpression - Регулярное выражение проверяющее пароль 
        на сложность.<br />
        Provider - Возвращает экземпляр используемого провайдера.<br />
        Providers - Коллекция зарегистрированных провайдеров.<br />
        RequiresQuestionAndAnswer - True если при восстановлении или сбросе пароля 
        пользователю нужно ответить на вопрос.<br />
        UsersOnlineTimeWindow - Время в минутах, в течении которых, 
        пользователь будет считаться подключенным не проявляя активности на сайте.
    </p>

    <h4>Методы класса Membership</h4>
    <p>
        CreateUser - Создание нового пользователя, возвращает объект MembershipUser, 
        который является объектным представлением пользователя.<br />
        DeleteUser - Удаление пользователя.<br />
        FindUsersByEmail - Поиск пользователя по email<br />
        FindUsersByName - Поиск пользователя по имени.<br />
        GeneratePassword - Создание пароля заданной длины.<br />
        GetAllUsers - Коллекция всех пользователей.<br />
        GetNumberOfUsersOnline - Количество пользователей на сайте.<br />
        UpdateUser - Принимает объект MembershipUser и обновляет хранящуюся информацию 
        в базе.<br />
        ValidateUser - Аутентифицирует пользователя по заданным учетным
    </p>
    <p>
        Roles - класс для программного определения ролей, 
        а также для связывания с ними пользователей.<br />
        Во время выполнения страницы информация о пользователе, 
        а также о его роли может быть получена через свойство User контекста HTTP
    </p>
    <h4>Методы класса Roles</h4>
    <p>
        AddUsersToRole - Включает группу пользователей в состав роли<br />
        AddUsersToRoles - Включает группу пользователей в группу ролей<br />
        AddUserToRole - Включает пользователя в состав роли<br />
        AddUserToRoles - Включает пользователя в состав группы ролей<br />
        CreateRole - Создает новую роль<br />
        DeleteCookie - Удаляет cookie, которые менеджер ролей использовал 
        для кэширования всех данных указанной роли<br />
        DeleteRole - Удаляет роль<br />
        FindUsersInRole - Извлекает имена пользователей, принадлежащих к заданной роли<br />
        GetAllRoles - Возвращает список всех доступных ролей<br />
        GetRolesForUser - Возвращает строковый массив ролей, 
        к которым принадлежит заданный пользователь<br />
        GetUsersInRole - Возвращает строковый массив с именами пользователей, 
        принадлежащих к заданной роли<br />
        IsUserlnRole - Определяет, принадлежит ли указанный пользователь заданной роли<br />
        RemoveUserFromRole - Удаляет пользователя из заданной роли<br />
        RemoveUserFromRoles - Удаляет пользователя из группы заданных ролей<br />
        RemoveUsersFromRole - Удаляет пользователей из заданной роли<br />
        RemoveUsersFromRoles - Удаляет пользователей из группы заданных ролей<br />
        RoleExists - Возвращает значение true, если заданная роль существует
    </p>
</body>
</html>
