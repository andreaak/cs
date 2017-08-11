<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._22_CustomServerControls._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css"/>
</head>
<body>
    <p>
        <strong>Custom Server Control</strong> - элемент управления, 
        который компилируется в отдельную сборку 
        и используются как любые другие стандартные серверные элементы управления 
        (Button, TextBox и т.д.)
    </p>
    <p>
        Для создание пользовательского контрола можно использовать проект типа Custom Server Control
        либо проект Class Library подключив к проекту ссылку на сборку System.Web
    </p>
    <p>Способы создания Custom Control:</p>
    <ul>
        <li>Наследовать существующий системные элемент управления</li>
        <li>Создать новый контрол объединив несколько стандартных элементов управления</li>
        <li>Создать класс производный от одного из базовых классов контрола</li>
    </ul>
    <p>
        Для регистрации серверного контрола используется тот же подход, 
        что и для регистрации пользовательского контрола. 
    </p>
    <ul>
        <li>Регистрация через директиву Register на странице где будет использоваться контрол.</li>
        <li>Регистрация в файле web.config для определения элемента управления 
            для определенной части веб приложения.</li>
    </ul>
    <p>
        При регистрации контрола необходимыми атрибутами является:
    </p>
    <ul>
        <li>Assembly - имя сборки в которой находится элемент управления</li>
        <li>Namespace - имя пространства имен в котором определены контроля</li>
        <li>TagPrefix - префикс для имен тэгов элементов управления.</li>
    </ul>
    <p>Регистрация элементов управления из сборки ASPWebFormsTest</p>
    <p class="code">&lt;%@ Register Assembly="ASPWebFormsTest" Namespace="ASPWebFormsTest._22_CustomServerControls" TagPrefix="cc1" %&gt;</p>
    <p>Использование элемента управления</p>
    <p class="code">
        &lt;cc1:_02_SimpleServerControl ID="MyControl1" runat="server"&gt;<br />
        &lt;/cc1:_02_SimpleServerControl&gt;
    </p>
    <img src="server_controls.png"/>

    <p>
        <strong>System.Web.UI.Control</strong> - базовый класс для всех ASP.NET элементов управления.
        Определяет методы для генерации разметки элемента управления. 
        Свойства для определения дочерних контролов, работы с ViewState, ID и прочие.
    </p>
    <p>Основные свойства:</p>
    <ul>
        <li>ID</li>
        <li>Parent</li>
        <li>Controls</li>
        <li>EnableViewState</li>
        <li>Page</li>
        <li>ViewState</li>
    </ul>
    <p>Основные методы:</p>
    <ul>
        <li>Render</li>
        <li>RenderChildren</li>
        <li>DataBind</li>
        <li>CreateChildControls</li>
    </ul>
    <p>Cобытия:</p>
    <ul>
        <li>Init</li>
        <li>Load</li>
        <li>PreRender</li>
        <li>Unload</li>
        <li>DataBinding</li>
        <li>Disposed</li>
    </ul>
    <p>
        <strong>System.Web.UI.WebControls.WebControl</strong> - базовый класс большинства элементов управления.
        Добавляет свойства для стилизации элементов управления.
    </p>
    <p>Основные свойства:</p>
    <ul>
        <li>Height</li>
        <li>Width</li>
        <li>IsEnabled</li>
        <li>Font</li>
        <li>Style</li>
        <li>TabIndex</li>
        <li>Tooltip</li>
    </ul>
    <p>Основные методы:</p>
    <ul>
        <li>ApplyStyle</li>
        <li>AddAtributedToRederer</li>
        <li>RenderContents</li>
        <li>RenderBeginTag</li>
        <li>RenderEndTag</li>
    </ul>
    <p>Производные классы: Button, TextBox, Calendar, Label, Image &hellip;</p>
    <p>
        <strong>System.Web.UI.WebControls.BaseDataBoundControl</strong> - 
        Базовый тип данных для тех контролов, которые используют привязку к данным.
    </p>
    <p>Основные свойства:</p>
    <ul>
        <li>DataSource</li>
        <li>DataSourceId</li>
        <li>Initialized</li>
    </ul>
    <p>Основные методы:</p>
    <ul>
        <li>DataBind</li>
        <li>PerformSelect</li>
        <li>ValidateDataSource</li>
        <li>OnDataBound</li>
    </ul>
    <p>Cобытия:</p>
    <ul>
        <li>DataBound</li>
    </ul>
    <p>Производные классы: DataBoundControl, HierarchicalDataBoundControl</p>
    <p>
        <strong>System.Web.UI.WebControls.DataBoundControl</strong> - Базовый класс 
        для всех серверных элементов управления, которые отображают данные 
        в виде списка или таблицы.
    </p>
    <p>Основные свойства:</p>
    <ul>
        <li>SelectMethod</li>
        <li>ItemType</li>
        <li>SelectArguments</li>
    </ul>
    <p>Основные методы:</p>
    <ul>
        <li>GetData</li>
        <li>GetDataSource</li>
        <li>PerformDataBinding</li>
    </ul>
    <p>Cобытия:</p>
    <ul>
        <li>CallingDataMethods</li>
        <li>CreatingModelDataSource</li>
    </ul>
    <p>Производные классы: AdRotator, ListView, CompositeDataBoundControl</p>
    <p>
        <strong>System.Web.UI.WebControls.CompositeDataBoundControls</strong> - 
        Представляет базовый тип для контролов, которые отображают данные в табличном представлении 
        и состоят из других серверных элементов управления.
    </p>
    <p>Основные свойства:</p>
    <ul>
        <li>DeleteMethod</li>
        <li>InsertMethod</li>
        <li>UpdateMethod</li>
    </ul>
    <p>Основные методы:</p>
    <ul>
        <li>CreateChildControls</li>
        <li>GetDataSource</li>
        <li>PerformDataBinding</li>
    </ul>
    <p>Cобытия:</p>
    <ul>
        <li>CallingDataMethods</li>
        <li>CreatingModelDataSource</li>
    </ul>
    <p>Производные классы: DetailsView, FormView, GridView</p>
    <p>
        <strong>CompositeControl</strong> - Задает базовую функциональность для элементов, 
        которые состоят из дочерних элементов. 
        (Производные классы: ChangePassword, Login, LoginStatus, SiteMapPath)
    </p>
    <p><strong>ListControl</strong> - задает методы, свойства и события для списочных элементов управления. (Производные классы: DropDownList, ListBox, BulletedList)</p>
    <p><strong>HierarchicalDataBoundControl</strong> - базовый класс для элементов управления, которые отображают данные в иерархической структуре. (Производные классы: Menu, TreeView)</p>
    <p>
        <strong>BaseDataList</strong> - представляет базовую функциональность 
        для контролов листинга данных (например, DataList, DataGrid).
    </p>
</body>
</html>
