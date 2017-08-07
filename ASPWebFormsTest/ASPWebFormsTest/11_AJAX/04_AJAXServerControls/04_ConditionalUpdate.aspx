<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_ConditionalUpdate.aspx.cs" Inherits="ASPWebFormsTest._11_AJAX._04_AJAXServerControls._04_ConditionalUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <%--UpdatePanel - задает область страницы, которая будет обновляться асинхронно--%>
            <%--Always - панель обновляется на любую обратную отправку, которая выполняется любым контролом на странице--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Red" ForeColor="White" ID="Label1" />
                    <asp:Button ID="Button1" Text="Обновить таймер" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />
            <br />

            <%--Conditional - обновление панели происходит в следующих случаях:
            1. Был явно вызван метод Update для UpdatePanel
            2. Сработал один из триггеров UpadtePanel
            3. Элемент управления находящийся в UpdatePanel выполнил обратную отправку
            --%>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Green" ForeColor="Yellow" ID="Label2" />
                    <asp:Button ID="Button2" Text="Обновить таймер" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />
            <br />

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" BackColor="Blue" ForeColor="White" ID="Label3" />
                    <asp:Button ID="Button3" Text="Обновить таймер" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
