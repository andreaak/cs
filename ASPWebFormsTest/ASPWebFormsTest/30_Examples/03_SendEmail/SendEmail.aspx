<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="ASPWebFormsTest._30_Examples._03_SendEmail.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Отправка smtp сообщений</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                Тема сообщения
            </td>
            <td>
                <asp:TextBox ID="TextBoxSubject" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email получателя
            </td>
            <td>
                <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Текст сообщения
            </td>
            <td>
                <asp:TextBox ID="TextBoxMsg" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="ButtonSend" runat="server" Text="Отправить" OnClick="ButtonSend_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="ErrorLabel" runat="server" EnableViewState="false" ForeColor="Red" />
                <asp:Label ID="LabelSuccess" runat="server" Text="Сообщение отправлено" Visible="false" EnableViewState="false" ForeColor="Green" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
