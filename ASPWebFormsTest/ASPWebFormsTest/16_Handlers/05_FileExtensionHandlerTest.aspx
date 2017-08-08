<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="05_FileExtensionHandlerTest.aspx.cs" Inherits="ASPWebFormsTest._16_Handlers._05_FileExtensionHandlerTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>*.test file handler</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                В данном приложении показано как с помощью HTTP обработчиков наладить обработку запросов к определенному типу ресурсов.
            </p>
            <p>
                В данном примере обработчик будет срабатывать при запросах к файлам с расширением *.test
                При этом обработчик будет считывать контент файла и преобразовывать его в HTML разметку
            </p>
            <a href="Files/File1.test">File1.test</a><br />
            <a href="Files/File2.test">File2.test</a><br />
            <a href="Files/File3.test">File3.test</a><br />
        </div>
    </form>
</body>
</html>