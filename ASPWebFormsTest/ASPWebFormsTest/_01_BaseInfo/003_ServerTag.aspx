<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="003_ServerTag.aspx.cs" Inherits="ASPWebFormsTest._01_BaseInfo._001_ServerTag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Серверный тэг</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!--Серверные тэги позволяют внедрять в разметку страницы блоки C# кода-->
            <%
                for (int i = 0; i < 10; i++)
                {
                    Response.Write("hello world!<br />");
                }
            %>
        
            Сегодня
            <%Response.Write(DateTime.Now.ToLocalTime()); %>

            <br />
            Сегодня

            <!--Знак = после открывающего серверного тэга указывает, что все выражение должно передаватся как параметр в метод
            Write свойства Response и отображаться в указанном месте страницы-->
            <%= DateTime.Now.ToLocalTime() %>
        </div>
    </form>
</body>
</html>
