<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="10_DesignSupportTest.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._10_DesignSupportTest" %>

<%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Design Support</title>
</head>
<body>
    <p>
        Атрибуты - метаданные позволяют добавить описательную информацию 
        к свойствам элемента управления или к самому элементу, 
        которую в последствии будет читать Visual Studio 
        для того чтобы выводить дополнительные подсказки в интерфейс 
        или определять особое поведение для отдельных свойств.
    </p>
    <p>
        Bindable - указывает может ли свойство быть связанно с данными.
    </p>
    <p>
        Browsable - видимость свойства в окне Properties.
    </p>
    <p>
        Category - Имя категории, к которой принадлежит свойство. 
        Данный атрибут используется для группировки свойств в окне Properties.
    </p>
    <p>
        DefaultEvent - событие элемента управления, обработчик которого 
        создается при двойном щелчке по элементу управления в окне дизайнера.
    </p>
    <p>
        DefaultProperty - Свойство элемента управления, 
        которое будет выделено при открытии окна Properies.
    </p>
    <p>DefaultValue - Значение по умолчанию для определенного свойства.</p>
    <p>
        Description - описание, которое выводиться в окне Properties 
        при выборе свойства.
    </p>

    <form id="form1" runat="server">
        <div>

            <cc1:_10_DesignSupport ID="TestControl1" runat="server">
            </cc1:_10_DesignSupport>

        </div>
    </form>
</body>
</html>
