<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_WorkingWithDatabase.aspx.cs"
    Inherits="ASPWebFormsTest._06_WorkingWithDatabase._09_WorkingWithDatabase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Работа с базой данных</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!--Чтение одного значения из базы-->
            <asp:Button ID="ReadOneValueButton" Text="Чтение одной записи" runat="server" OnClick="ReadOneValueButton_Click" />
            Результат:
        <asp:Label ID="ReadOneValueOutput" EnableViewState="false" runat="server" />
            <hr />
            <!--Построчное считыванеи информации-->
            <asp:Button ID="ReadAllButton" Text="Чтение всех записи" runat="server" OnClick="ReadAllButton_Click" />
            Результат:
        <br />
            <asp:Label ID="ReadAllOutput" EnableViewState="false" runat="server" />
            <hr />
            <!--Построчное считывание информации-->
            Login
        <asp:TextBox ID="LoginTextBox" runat="server"></asp:TextBox>
            <br />
            Passowrd
        <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
            <br />
            Email
        <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox><br />
            <asp:Button ID="AddNewEntryButton" Text="Добавить новую запись" runat="server" OnClick="AddNewEntryButton_Click" />
            <asp:Label ID="ErrorOutput" runat="server" EnableViewState="false" />
            <hr />
            Id
        <asp:TextBox ID="IdToRemoveTextBox" runat="server" />
            <!--OnClientClick JavaScript функция, которая выполняется на стороне клиента до того как на сервер будет отправлен запрос.
        Если функция возвращает значение false - отправка запроса отменяется-->
            <asp:Button ID="RemoveByIdButton" Text="Удалить запись по ID" runat="server" OnClientClick="return confirm('Вы уверены, что хотите удалить запись?')"
                OnClick="RemoveByIdButton_Click" />
            <asp:Label ID="ErrorOutput2" runat="server" EnableViewState="false" />
        </div>
    </form>
</body>
</html>
