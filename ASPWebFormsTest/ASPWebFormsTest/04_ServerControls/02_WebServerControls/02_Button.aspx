<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_Button.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls.ButtonSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Button #1</title>
</head>
<body>
    <p>
        Класс Button транслируется в &lt;input type=&rdquo;submit&rdquo;/&gt; 
            или &lt;input type=&rdquo;button&rdquo;/&gt; в зависимости от настроек элемента.
    </p>
    <form id="form1" runat="server">
        <div>
            <!--asp:Button визуализируется как <input type="submit" /> или <input type="button" /> -->
            <asp:Button ID="Button1"
                runat="server"
                Text="Кнопка"
                OnClick="Button1_CLick"
                ToolTip="Всплывающая подсказка"
                BackColor="LightBlue"
                ForeColor="BlueViolet"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1" />
            <br />
            <%--Изменение свойств элемента управления из файла кода--%>
            <asp:Button ID="Button2" runat="server" Text="Кнопка" />
            <br />

            <p>
                OnClientClick - позволяет задать JavaScript функцию или отдельный набор инструкций,
                которая должна выполнится до того, как на сервер будет отправлен postback запрос. 
                Если функция возвращает false - отправка postback запроса отменяется
            </p>
            <asp:Button Text="Кнопка"
                ID="Button3"
                runat="server"
                OnClick="Button3_Click"
                OnClientClick="return confirm('Вы уверены, что хотите запустить обработчик на сервере?')" />
        </div>
    </form>
</body>
</html>
