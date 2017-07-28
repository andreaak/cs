<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="04_ViewStateSaveObject.aspx.cs"
    Inherits="ASPWebFormsTest._03_StateSaving._02_ViewState._04_ViewStateSaveObject" ViewStateEncryptionMode="Always" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View State #4</title>
</head>
<body>
    <p>
        Объекты, сохраняемые в состояние вида, должны быть сериализуемого типа.
    </p>

    <p>
        По умолчанию ViewState отправляется в не зашифрованном виде.<br />
        Чтобы Включить шифрование используется следующий атрибут ViewStateEncryptionMode
    </p>
    <ul>
        <li>"Always" - шифровать все данные во ViewStat при каждом запросе/ответе</li>
        <li>"Never" - не шифровать данные</li>
        <li>"Auto" - (по умолчанию) шифровать только те данные ViewState, которые относятся к контролам запросившим шифрование
        своей части информации во ViewState</li>
    </ul>

    <form id="form1" runat="server">
        <div>
            <asp:Button ID="WriteButton" Text="Записать во ViewState" runat="server" OnClick="WriteButton_Click" />
            <asp:Button ID="ReadButton" Text="Прочитать из ViewState" runat="server" OnClick="ReadButton_Click" />
            <br />
            <br />
            <b>UserName: </b>
            <asp:Label ID="UserNameLabel" runat="server" /><br />
            <b>Email: </b>
            <asp:Label ID="EmailLabel" runat="server" /><br />
        </div>
    </form>
</body>
</html>
