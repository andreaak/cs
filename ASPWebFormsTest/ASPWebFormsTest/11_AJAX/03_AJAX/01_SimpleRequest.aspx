<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_SimpleRequest.aspx.cs"
    Inherits="ASPWebFormsTest._11_AJAX._03_AJAX._01_SimpleRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AJAX использование в ASP.NET</title>

    <script type="text/javascript">
        window.onload = function () {
            var label = document.querySelector("#output");

            function onComplete(result) {
                label.innerHTML = result;
            }

            function onError(error) {
                label.innerHTML = error._message;
            }

            /*
            Вызываем метод HelloWorld на сервисе WebService1
            Первый параметр - callback функция, которая запустится в случае успешной обработки запроса
            Второй параметр - callback функция, которая запустится в случае ошибки на сервере
            */
            ASPWebFormsTest._11_AJAX._03_AJAX.WebService1.HelloWorld(onComplete, onError);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <%--ScriptManager генерирует ссылки на библиотеки ASP.NET AJAX JavaScript--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <%--Определяем место расположение службы 
                    ссылку на прокси JavaScript код которой будет добавлена ScriptManager'ом--%>
                <Services>
                    <asp:ServiceReference Path="./WebService1.asmx" />
                </Services>
            </asp:ScriptManager>

            <asp:Label runat="server" ID="output"></asp:Label>
        </div>
    </form>
</body>
</html>

