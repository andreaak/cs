<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_RepeaterBindValue.aspx.cs"
    Inherits="ASPWebFormsTest._08_DataBinding._08_Repeater._01_RepeaterBindValue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Repeater Sample #1</title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <p>
        <strong>Repeater</strong> - элемент управления поддерживающий привязку к данным и позволяющий генерировать разметку 
        по заранее определенному шаблону для каждого элемента из источника данных.
    </p>
    <p>Типы элементов Repeater: </p>
    <ul>
        <li>HeaderTemplate - шаблон, который будет использоваться для построения заголовка. 
            Шаблон применяется только один раз в самом начале генерации HTML разметки данным элементом управления.</li>
        <li>FooterTemplate - шаблон &laquo;подвала&raquo;, разметка которая будет сгенерирована в самом конце создания контрола, 
            после того, как будет сгенерирован основной контент.</li>
        <li>ItemTemplate - шаблон который будет использоваться для вывода на страницу каждого элемента данных из источника данных. 
            Для того что бы получить доступ к отдельным свойствам источника данных 
            следует использовать выражение &lt;% Eval(&ldquo;Имя свойства&rdquo;) %&gt;</li>
        <li>AlternatingItemTemplate - шаблон который будет чередоваться с шаблоном ItemTemplate.</li>
        <li>SeparatorTemplate - шаблон разметки, которая будет добавляться после разметки созданной ItemTemplate 
            и AlternatingItemTemplate.</li>
    </ul>
    <p>RepeaterControl работает по следующему алгоритму (Если указаны все шаблоны): </p>
    <ul>
        <li>Создание HeaderTemplate</li>
        <li>Создание ItemTemplate</li>
        <li>Создание SeparatorTemplate</li>
        <li>Создание AlternatingItemTemplate</li>
        <li>Возврат к созданию ItemTemplate и повторение операций до тех пор пока существуют элементы в источнике данных.</li>
        <li>Создание FooterTemplate</li>
    </ul>
    <br />
    <form id="form1" runat="server">
        <div>
            <%-- Repeater – элемент управления способный привязываться к данным и создающий
             разметку путем повторения указанных шаблонов для каждого элемента списка.--%>
            <asp:Repeater ID="Repeater1" runat="server">
                <%-- Шаблон, описывающий один элемент списка --%>
                <ItemTemplate>
                    <%--Container.DataItem - один элемент источника данных, в данном случае типа string--%>
                    <%# Container.DataItem %>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
