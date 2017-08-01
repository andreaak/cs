<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs"
    Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._16_ListControls._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet"/>
</head>
<body>

    <img src="list_controls.png"/>

    <p>ListItem - класс который представляет отдельный элемент в списочном элементе управления.</p>

    <table>
        <tr>
            <th>Свойство</th>
            <th>Описание</th>
        </tr>
        <tr>
            <td>SelectedIndex</td>
            <td>порядковый номер выбранного элемента в списочном контроле</td>
        </tr>
        <tr>
            <td>SelectedValue</td>
            <td>значение атрибута value выбранного элемента управления</td>
        </tr>
        <tr>
            <td>SelectedItem</td>
            <td>ListItem выбранный в списочном элементе управления</td>
        </tr>
        <tr>
            <td>Items</td>
            <td>Коллекция всех элементов в контроле</td>
        </tr>
    </table>

    <br />
    <p>
        Списочные элементы управления - специализированные элементы управления Web,генерирующие окна списков, 
        выпадающие списки и другие контролы, которые можно привязать к источнику данных 
        или задать набор элементов программным способом.
    </p>
    <p>
        Большинство списочных элементов управления, позволяют выбрать один или несколько элементов списка, 
        за исключением BullettedList (статический нумерованный или маркированный список).
    </p>
    <p>Все списочные элементы унаследованы от класса System.Web.UI.WebControls.ListControls</p>
    <p>
        Класс System.Web.UI.WebControls.ListControls определяет событие SelectedIndexChanged, 
        которое запускается при изменении текущего выбора.
    </p>
    <p>Элементы, принадлежащие списку, находиться в свойстве Items.</p>
    <p>Отдельный элемент списка представлен типом ListItem</p>
    <p>Основные свойства ListItem:</p>
    <ul>
        <li>Text - значение, отображаемое пользователю</li>
        <li>Value - значение, которое можно получить программным путем. 
            Если явно не установлено, то совпадает с значением свойства Text</li>
        <li>Selected - в случае если элемент в списке может быть выбраненным 
            (DropDownList, CheckBoxList, RadioButtonList, ListBox)</li>
        <li>Enabled - активность элемента управления.</li>
    </ul>
</body>
</html>
