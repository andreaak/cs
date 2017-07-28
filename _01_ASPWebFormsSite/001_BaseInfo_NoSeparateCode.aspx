<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Output.Text = "Hello world! Time on server " + DateTime.Now.ToLongTimeString();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SingleFile Code Model</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Output" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>


<%-- 
    Web Application 

       > Используется файл проекта *.sln и *.csproj.
       > Весь код в проекте компилируется в одну сборку.
       > Поддерживает работу с IIS и встроенный сервер ASP.NET Development Server.
       > Поддерживает все возможности Visual Studio (refactoring, generics, etc.) и возможности ASP.NET 2.0 (master pages, membership and login, site navigation, themes, etc).

    Web Site

       > Нет файла проекта (структура проекта базируется на файловой системе).
       > Каждая страница компилируется в отдельную библиотеку
       > Динамическая компиляция без необходимости пересобрать весь сайт.
       > Поддерживает работу с IIS и встроенный сервер ASP.NET Development Server.
       > Различные модели кода Code-Behind и Single-File


    * Необходимо мигрировать Visual Studio 2003 приложение в VS 2005 -- Web Application
    * необходимо редактировать любую директорию или файл без создания файла проекта -- Web Site
    * Необходимо добавить pre-build и post-build шаги при компиляции приложения -- Web Application
    * Необходимо построить веб приложение используя несколько веб проектов -- Web Application
    * Необходимо создавать по одной сборке на каждую страницу -- Web Site
    * Необходима динамическая компиляция страницы сразу после редактирования без компиляции всего проекта -- Web Site
    * Использование Single-File Code Model предпочитается больше чем Code-Behnd -- Web Site

--%>
