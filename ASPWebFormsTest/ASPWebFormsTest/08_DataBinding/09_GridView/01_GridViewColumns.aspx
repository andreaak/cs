<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_GridViewColumns.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._09_GridView._01_GridViewColumns" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView AutoGenerateColumns</title>
    <link href="~/Content/My.css" rel="stylesheet"/>
</head>
<body>
    <p>
        <strong>GridView</strong> - элемент управления, который отображает данные виде таблицы, 
        при этом каждая запись источника отображается как строка, а каждое поле как колонка. 
        Поддерживает отображение, сортировку, выделение, разбиение на страницы и редактирование.
    </p>
    <p>
        AutoGenerateColumns=&rdquo;true&rdquo; - когда установлено это свойство, 
        элемент управления использует рефлексию для исследования объекта данных и нахождения открытых свойств или полей. 
        Затем контрол создает столбцы для каждого из них в том порядке, в котором их обнаруживает.
    </p>
    <p>
        Для ручной настройки столбцов в GridView следует установить AutoGenerateColumns=&rdquo;false&rdquo; 
        и определить столбцы самостоятельно во вложенном в GridView элементе &lt;Columns&gt;.
    </p>
    <p>Типы колонок GridView:</p>
    <ul>
        <li>BoundField - отображает текстовое значение</li>
        <li>ButtonField - отображает кнопку</li>
        <li>CheckBoxField</li>
        <li>CommandField - отображает кнопку для запуска команд (select, edit, delete)</li>
        <li>HyperlinkField - отображает ссылку</li>
        <li>ImageField - отображает картинку</li>
        <li>TemplateField - шаблон для данных ячейки</li>
    </ul>
    <p>
        GridView поддерживает выбор строк, то есть пользователь может кликнуть по кнопке или ссылке, 
        что бы перевести строку таблицы в состояние выбранной строки и изменить ее внешнее представление.
    </p>
    <p>Событие RowDataBound используется для обработки момента создания элемента и привязки к нему данных в GridView.</p>
    <p>Событие SelectedIndexChanged происходит в момент смены выбранной строки в GridView.</p>
    <p>
        Для того что бы GridView автоматически разбивался на страницы установите свойству AllowPaging значение true, 
        а свойством PageSize укажите количество записей на одной странице.
    </p>
    <br />
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
