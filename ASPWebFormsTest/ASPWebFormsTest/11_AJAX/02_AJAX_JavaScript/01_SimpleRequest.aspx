<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_SimpleRequest.aspx.cs" 
    Inherits="ASPWebFormsTest._11_AJAX._02_AJAX_JavaScript._01_SimpleRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AJAX использование через JavaScript</title>
    <script type="text/javascript">
        var xhr;

        // обработчик события загрузки страницы.
        window.onload = function () {
            // Создание объекта запроса.
            xhr = new XMLHttpRequest();

            /*
            функция-обработчик срабатывает при изменении свойства readyState
            Значения свойства readyState:
            0 - Метод open() еще не вызывался
            1 - Метод open() уже был вызван, но метод send() еще не вызывался.
            2 - Метод send() был вызван, но ответ от сервера еще не получен
            3 - Идет прием данных от сервера. Для значения 3 Firefox вызывает обработчик события несколько раз IE только один раз.
            4 - Ответ от сервера полностью получен (Запрос успешно завершен).
            */
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        document.getElementById("output").innerHTML = xhr.responseText;
                    }
                }
            }

            var button1 = document.getElementById("button1");
            button1.onclick = clickHandler;
        }

        // обработчик нажатия кнопки.
        function clickHandler() {
            // Настройка объекта на отправку асинхронного запроса.
            xhr.open("GET", "01_SimpleRequestHandler.ashx");
            xhr.send(null);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" value="AJAX кнопка" id="button1" />
            <p id="output" style="color: Green; font-weight: bold;">
            </p>
        </div>
    </form>
</body>
</html>
