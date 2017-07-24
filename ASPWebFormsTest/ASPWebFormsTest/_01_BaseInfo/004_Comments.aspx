<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="004_Comments.aspx.cs" Inherits="ASPWebFormsTest._01_BaseInfo.Comments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Комментарии</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!--HTML комментарий, передается в разметке клиенту-->

            <%--ASP.NET комментарий, остается в исходном коде страницы. Не передается клиенту. --%>

            <%--
                Многострочный ASP.NET комментарий
                Line 1
                Line 2
            --%>

            <%
                // Однострочный C# комментарий

                /*
                     Многострочный C# комментарий
                     Line 1
                     Line 2
                */        
            %>
        </div>
    </form>
</body>
</html>
