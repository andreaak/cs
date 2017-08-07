<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_UpdatePanel.aspx.cs" Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._01_UpdatePanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpdatePanel Sample</title>
</head>
<body>
    <p>
        UpdatePanel - серверный контрол, позволяющий асинхронно обновлять часть страницы, не перегружая страницу полностью. 
        Когда элемент управления находящийся в UpdatePanel выполняет обратный запрос, 
        UpdatePanel перехватывает его и выполняет вместо этого асинхронный обратный вызов.<br />
        При использовании UpdatePanel, на странице обязательно должен находиться ScriptManager.<br />
        Когда Web-страница генерирует необработанное исключение, то ошибка перехватывается ScriptManager и отправляется клиенту<br />
        Timer - генерирует обратный запрос с определенным интервалом. 
        Интервал задается через свойство Interval и задается в миллисекундах. 
        Также Timer содержит событие Tick, которое будет вызываться с указанным интервалом.<br />
        UpdateProgress - элемент управления содержащий контент, который отображается в момент выполнения асинхронного запроса. 
        С помощью JavaScript функции можно остановить выполнение асинхронного обратного запроса.<br />
        Для того, что бы добавить ссылку на библиотеку в WebApplication, следует воспользоваться функцией 
        &ldquo;Add Reference&rdquo; в Solution Explorer. 
        Если выполнить такие же действия в проекте типа WebSite, 
        Visual Studio добавит в проект папку Bin и поместит в нее выбранную библиотеку.<br />
        Библиотеки проекта WebSite всегда должны находиться в ASP.NET директории bin.
    </p>

    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager runat="server" />

            <%--UpdatePanel - задает область страницы, которая будет обновляться асинхронно--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Red" ForeColor="White" ID="Label1" />
                    <asp:Button ID="Button1" Text="Обновить таймер №1" runat="server" OnClick="Button1_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />
            <br />

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Green" ForeColor="Yellow" ID="Label2" />
                    <asp:Button ID="Button2" Text="Обновить таймер №2" runat="server" OnClick="Button2_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />
            <br />

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Blue" ForeColor="White" ID="Label3" />
                    <asp:Button ID="Button3" Text="Обновить таймер №3" runat="server" OnClick="Button3_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
