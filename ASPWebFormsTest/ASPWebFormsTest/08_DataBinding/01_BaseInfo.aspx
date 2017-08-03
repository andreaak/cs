<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <span class="bold">Привязка данных</span> - процесс, который позволяет ассоциировать источник данных с элементом управления 
        с целью автоматического отображения данных в этом элементе управления.
    </p>
    <h2>Привязка одного значения</h2>
    <img src="binding_single.png" />
    <p>
        Данная разметка создает элемент управления литерал, 
        который отображает значения свойства FirstName определенного в коде страницы.
    </p>

    <h2>Привязка множества значений</h2>
    <p>
        Некоторые элементы управления (классы, которые наследуются от BaseDataBoundControl) 
        поддерживают работу с источниками данных.<br />
       
        <span class="bold">DataSource</span>  - свойство, с помощью которого 
        можно определить источник данных для элемента управления.
        <br />
        <span class="bold">DataBind()</span> - метод, который запускает механизм привязки данных<br />
    </p>
    <p class="code">
        string[] values = { "Item 1", "Item 2", "Item 3", "Item 4" };<br />
        DropDownList1.DataSource = values;<br />
        DropDownList1.DataBind();
    </p>

    <table>
        <tr>
            <th>Свойство</th>
            <th>Описание</th>
        </tr>
        <tr>
            <td>DataSource</td>
            <td>Источник данных для элемента управления</td>
        </tr>
        <tr>
            <td>DataSourceId</td>
            <td>ID элемента источника данных, который размещен на этой странице, 
                например, SqlDataSource или ObjectDataSource</td>
        </tr>
        <tr>
            <td>DataTextField</td>
            <td>Имя столбца (в случае если источник таблица) или свойства (в случае если источник объект) 
                элемента данных, которое содержит значение, отображаемое на странице.</td>
        </tr>
        <tr>
            <td>DataTextFormatingString</td>
            <td>Указывает не обязательную строку формата, которую будет использовать элемент управления 
                для форматирования каждого DataTextField перед его отображением.</td>
        </tr>
        <tr>
            <td>DataValueField</td>
            <td>Данное свойство подобное DataTextFieldно устанавливает источник для valueэлемента в списке</td>
        </tr>
    </table>
</body>
</html>
