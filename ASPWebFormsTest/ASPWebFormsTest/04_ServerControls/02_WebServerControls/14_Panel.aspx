<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="14_Panel.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._14_Panel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Panel</title>
    <style type="text/css">
        .bord {
            border: 1px solid black
        }
    </style>
</head>
<body>
    <p>
        Класс Panel транслируется в &lt;div&gt;
        <br />
        Свойство DefaultButton класса Panel используется для задания кнопки по умолчанию, которая будет активироваться когда фокус находится в одном из элементов управления панели.
        <br />
        Свойство DefaultButton элемента серверной формы используется для задания кнопки по умолчанию для всей страницы.
        <br />
    </p>
    <p>
        DefaultButton - кнопка по умолчанию. Она будет в фокусе при загрузке страницы 
        и выполнит postback запрос при нажатии на клавишу Enter
    </p>
    <br />
    <br />
    <form id="form1" runat="server" defaultbutton="Button1">
        <div>
            <asp:Panel runat="server" Height="200" Width="200" ScrollBars="Auto" CssClass="bord">
                <asp:Button ID="Button1" Text="Button 1" runat="server"/>
                <p>
                    Text 1
                </p>
                <br />
                <br />
                <p>
                    Text 2
                </p>
                <br />
                <br />
                <p>
                    Text 3
                </p>
                <br />
                <br />
                <p>
                    Text 4
                </p>
                <br />
                <br />
                <p>
                    Text 5
                </p>
                <br />
                <br />
                <p>
                    Text 6
                </p>
                <br />
                <br />
                <p>
                    Text 7
                </p>
                <asp:Button ID="Button2" Text="Button 2" runat="server" />
            </asp:Panel>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel runat="server" GroupingText="Форма регистрации" Width="200" Height="200" CssClass="bord">
                Имя<asp:TextBox ID="TextBox1" runat="server" /><br />
                Email<asp:TextBox ID="TextBox2" runat="server" /><br />
                Пароль<asp:TextBox ID="TextBox3" runat="server" /><br />
            </asp:Panel>
        </div>
        <br />
        <br />
        <div>
            <asp:Button ID="Button3" Text="Default" runat="server" OnClick="DefaultButton_Click" />

            <p>
                DefaultButton - указывает ID кнопки, которая будет делать postback запрос 
                при нажатии на Enter, в случае если фокус находиться в одном 
                из элементов управления панели
            </p>

            <asp:Panel runat="server" GroupingText="Panel 1" DefaultButton="ButtonA" CssClass="bord">
                <asp:TextBox runat="server" ID="TextBox4" />
                <asp:Button ID="ButtonA" Text="Button A" runat="server" OnClick="ButtonA_Click" />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server" GroupingText="Panel 2" DefaultButton="ButtonB" CssClass="bord">
                <asp:TextBox runat="server" ID="TextBox5" />
                <asp:Button ID="ButtonB" Text="Button B" runat="server" OnClick="ButtonB_Click" />
            </asp:Panel>

            <asp:Button ID="ClearButton" Text="Очистить поля вводов" runat="server" OnClick="ClearButton_Click" />
        </div>
    </form>
</body>
</html>
