<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._15_ConfigFiles._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>Machine.config - конфигурационный файл, который содержит настройки для текущего компьютера.</p>
    <p>%windir%\Microsoft.NET\Framework64\v4.0.30319\Config\Machine.config</p>
    <p>
        Web.config - главный конфигурационный файл для ASP.NET приложения.
        Корневой web.config файл расположен %windir%\Microsoft.NET\Framework64\v4.0.30319\Config\Web.config<br/> 
        и содержит настройки для всех веб приложений на сервере. В свою очередь каждое приложение содержит локальный web.config файл, 
        который наследует корневой файл и переопределяет настройки необходимые для конкретного приложения.
    </p>
    <img src="web_config.png"/>
</body>
</html>
