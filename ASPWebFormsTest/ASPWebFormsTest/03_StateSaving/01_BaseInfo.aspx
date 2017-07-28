<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        HTTP - протокол, который не поддерживает хранения состояния.<br />
        В веб приложениях хранение состояния можно организовать на:
    </p>
    <ul>
        <li>
            <div><strong>На стороне пользователя</strong></div>
            <div class="image">
                <img src="state_user.png" />
            </div>
        </li>
        <li>
            <div><strong>На стороне сервера</strong></div>
            <div class="image">
                <img src="state_server.png" />
            </div>
        </li>
    </ul>

    <p>Способы хранения состояния:</p>
    <ol>
        <li>Состояние вида. (ViewState)</li>
        <li>Хранение состояния в адресной строке.(URL)</li>
        <li>Cookie-файлы.(Cookies)</li>
        <li>Сессия. (Session)</li>
        <li>Состояние приложения. (Application)</li>
    </ol>

</body>
</html>
