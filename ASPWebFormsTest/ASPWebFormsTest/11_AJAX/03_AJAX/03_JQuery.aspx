<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="03_JQuery.aspx.cs" Inherits="ASPWebFormsTest._11_AJAX._03_AJAX._03_JQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AJAX использование через JavaScript</title>
    <script src="../../Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">

        window.onload = function () {
            var url = "/11_AJAX/03_AJAX/WebService1.asmx";
            var output = document.getElementById("output");

            var button1 = document.getElementById("button1");
            button1.onclick = clickHandler;

            function OnSuccessCall(response) {
                output.innerHTML = response.d
            }

            function OnErrorCall(response) {
                output.innerHTML = response.status + " " + response.statusText
            }

            function clickHandler() {
                $.ajax({
                    type: "POST",
                    url: url + "/HelloWorld",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessCall,
                    error: OnErrorCall
                });
            };

            var button2 = document.getElementById("button2");
            button2.onclick = clickHandler2;

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4) {
                    if (xhr.status == 200) {
                        document.getElementById("output").innerHTML = xhr.response;
                    }
                }
            }

            // обработчик нажатия кнопки.
            function clickHandler2() {
                // Настройка объекта на отправку асинхронного запроса.
                xhr.open("POST", url + "/HelloWorld");
                xhr.send(null);
            }
        }

    </script>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <input type="button" value="AJAX JQuery кнопка" id="button1" /><br />
            <input type="button" value="AJAX Javascript кнопка" id="button2" />
            <p id="output" style="color: Green; font-weight: bold;">
            </p>
        </div>
    </form>
</body>
</html>
