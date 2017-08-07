<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._13_Routing._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        Подмена URL - изменения фактического адреса, по которому пришел запрос от пользователя, 
        на адрес который используется приложением. 
        Зачастую адрес, который будет подменен, ведет на несуществующую страницу или ресурс в веб приложении.<br />
        Например, пользователь вводит в адресной строке путь http://example.com/products/phones/10 , 
        а приложение получает запрос http://example.com/catalog.aspx?category=phones&amp;productId=10.
        <br />
        Способы подмены URL:
    </p>
    <ul>
        <li>Установка ISAPI расширения на сервере (в IIS или Apache), например, http://www.isapirewrite.com/. 
            Данный способ подмены URL самый производительный, но требует прав администратора на сервере.</li>
        <li>Использование HTTP модуля, например, UrlRewritingNet http://www.urlrewriting.net. 
            Наиболее простой в реализации и возможный к использованию на серверах, 
            на которых нет возможности установить расширения для веб сервера.</li>
        <li>Использование метода HttpContext.Current.RewritePath() в собственном HTTP модуле или файле Global.asax. 
            Данных подход самый трудоемкий.</li>
        <li>Использование стандартного системного модуля UrlRoutingModule доступного в .NET 4</li>
    </ul>
    <p>
        URL маршрутизация позволяет настраивать приложение на прием запрашиваемых адресов, 
        которые не соответствуют физическим файлам.<br />
        До маршрутизации<br />
        http://mysite.com/products.aspx?id=10&amp;category=software<br />
        После маршрутизации<br />
        http://mysite.com/products/software/10
    </p>
</body>
</html>
