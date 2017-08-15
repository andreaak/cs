<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._05_PageDesign._03_MasterPage._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        <strong>Мастер-страница</strong> - инструмент для стандартизации компоновки web-страниц. 
        Мастер-страница представляет собой шаблон web-страниц, 
        который может определять фиксированное содержимое и объявлять часть страницы, 
        в которую будет помещено специальное содержимое. 
    </p>
    <p>
        Только мастер-страница может использовать элемент управления ContentPlaceHolder<br />
        Существует возможность создавать вложенные мастер страницы, когда одна мастер-страница наследует другую мастер страницу.
    </p>
    <img src="master_page.png" />
</body>
</html>
