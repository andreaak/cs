<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls.DataBinding._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        Базовыми классами для контролов, которые поддерживают привязку к данными, 
        являются классы BaseDataBoundControl и DataBoundControl.
    </p>
    <p>
        При использовании класса DataBoundControl нет необходимости реализовывать абстрактные методы,
        как в случае с классом BaseDataBoundControl
        Также, используя класс DataBoundControl, стирается разница 
        между использованием свойств DataSource и DataSourceId, 
        внутренняя реализация класса самостоятельно определит тип источника данных 
        и предоставит перечисляемый тип данных со всей информацией извлеченной из источника .
    </p>
    <p>
        Для того, чтобы воспользоваться данными, которые были переданы контролу 
        через свойства DataSource и DataSourceId, необходимо переопределить метод PerformDataBinding.
    </p>
    <p>
        Свойства DataSource и DataSourceId являются взаимоисключающими 
        и не могут быть установлены одновременно.
    </p>
    <p>
        [PersistenceMode(PersistenceMode.InnerProperty)] - атрибут который указывает 
        что свойство элемента управления должно инициализироваться через вложенный элемент в разметке.
        (По умолчанию свойства элемента задаются через атрибуты в разметке)
    </p>
</body>
</html>
