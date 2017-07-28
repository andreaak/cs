<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._02_Page._05_Request._01_BaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        <strong>Request</strong> - экземпляр класса System.Web.HttpRequest. 
        Этот объект представляет свойства и значения HTTP запроса, вызвавшие загрузку страницы.
    </p>

    <p><strong>QueryString</strong>(свойство объекта HttpRequest) - Представляет параметры, переданные в строке запроса.</p>
    <p>
        <strong>Form</strong>(свойство объекта HttpRequest) - Представляет коллекцию переменных формы, обратно отправляемых на страницу. 
        В большинстве случаев данная информация извлекается из свойств элементов управления вместо использования данной коллекции.
    </p>
    <div>
        <asp:Label ID="Label1" runat="server" />
    </div>
</body>
</html>
