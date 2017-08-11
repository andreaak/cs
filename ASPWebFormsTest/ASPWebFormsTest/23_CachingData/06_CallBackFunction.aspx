<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="06_CallBackFunction.aspx.cs" Inherits="ASPWebFormsTest._23_CachingData._06_CallBackFunction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        Callback метод будет срабатывать в том случае если запись будет удалена из кэша.
    </p>
    <p>
        Возможные причины: 
    </p>
    <ul>
        <li>DependencyChanged - Изменен объект от которого зависит данный объект.</li>
        <li>Underused - Элемент удален для освобождения памяти.</li>
        <li>Removed - Удален из кэша программным способом ( с помощью метода Remove)</li>
        <li>Expired - Элемент устарел.</li>
    </ul>
    <br />

    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" Text="Добавить значение в кэш" runat="server" OnClick="AddButton_Click" />
            <asp:Button ID="Button2" Text="Прочитать значение" runat="server" OnClick="ReadButton_Click" />
            <asp:Button ID="Button3" Text="Удалить значение из кэша" runat="server" OnClick="RemoveButton_Click" />
            <br />
            <hr />
            <br />
            <asp:Label ID="MessageLabel" runat="server" />
        </div>
    </form>
</body>
</html>
