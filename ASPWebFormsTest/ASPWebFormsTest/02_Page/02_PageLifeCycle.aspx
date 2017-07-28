<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="02_PageLifeCycle.aspx.cs" Inherits="ASPWebFormsTest._02_Page._02_PageLifeCycle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/My.css" rel="stylesheet" />
</head>
<body>
    <a href="https://msdn.microsoft.com/ru-ru/library/ms178472.aspx">Page Life Cycle</a>
    <h2>События класса Page</h2>
    <ol>
        <li>PreInit</li>
        <li class="mostUsed">Init</li>
        <li>InitComplete</li>
        <li>PreLoad</li>
        <li class="mostUsed">Load</li>
        <li>LoadComplete</li>
        <li class="mostUsed">PreRender</li>
        <li>PreRenderComplete</li>
        <li>SaveStateComplete</li>
        <li class="mostUsed">Unload</li>
    </ol>
    <p>
        <span>PreInit</span> - происходит на ранней стадии жизненного цикла страницы. 
        После события PreInit, загружается информация о персонализации и тема страницы, если она имеется. 
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Установки мастер страницы.</li>
            <li>Установки темы страницы.</li>
            <li>Динамическое создание элементов  управления на странице.</li>
        </ul>
    </div>

    <p>
        <span>Init</span> - происходит при инициализации серверного элемента управления или страницы, 
        который находится на первом этапе его жизненного цикла.
        На данном этапе ViewState страницы еще не загружен.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Инициализации элементов управления.</li>
        </ul>
    </div>

    <p>
        <span>InitComplete</span> - происходит при завершении инициализации. 
        На этом этапе ViewState доступен, но данные еще не прочтены элементами управления.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Действий требующих полной инициализации элементов управления.</li>
            <li>Внесение изменений во ViewState.???</li>
        </ul>
    </div>

    <p>
        <span>PreLoad</span> - происходит после обработки всех данных полученных от пользователя 
        и до того как запуститься событие Load страницы. Используется если есть на странице пользовательские контролы.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Для выполнения действий в пользовательских элементах управления (UserControl) 
                до того как сработает событие Load у страницы. 
                Событие Load пользовательского элемента управления срабатывает позже чем Load страницы.</li>
        </ul>
    </div>

    <p>
        <span>Load</span> - событие указывает на то, что страница загружена 
        и все элементы управления готовы к работе.
        Если запрос является PostBack запросом, 
        после события Load срабатывает обработчик события элемента управления.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Чтения и инициализации свойств страницы и элементов управления.</li>
            <li>Создания подключений к базе данных.</li>
            <li>Для выполнения действий, которые повторяются при каждом запросе к странице.</li>
        </ul>
    </div>

    <p>
        <span>LoadComplete</span> - Происходит в конце этапа загрузки страницы. 
        Данное событие происходит после того, как сработают обработчики серверных элементов управления.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Операций требующих завершения всех обработчиков событий.</li>
        </ul>
    </div>
    элемента

    <p>
        <span>PreRender</span> - происходит перед тем 
        как элементы управления будут преобразованы в HTML разметку.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Обновлений элементов и ViewState перед тем как элементы управления будут превращены в HTML.</li>
        </ul>
    </div>

    <p>
        <span>PreRenderComplete</span> - происходит после того как все элементы управления 
        прочтут данные из источников данных,
        перед тем как элементы управления будут преобразованы в HTML разметку. 
        Это последнее событие, которое происходит перед тем как будет сохранен ViewState.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Используется при разработке асинхронных страниц.</li>
        </ul>
    </div>

    <p>
        <span>SaveStateComplete</span> - Завершение сохранение ViewState.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li></li>
        </ul>
    </div>

    <p>
        <span>Unload</span> - происходит при очистке объекта и выгрузки его из оперативной памяти.
        <br />
        Метод срабатывает даже в том случае если при обработке предыдущих событий страницы 
        были выброшены и не обработаны исключения. В данный момент нельзя вносить изменения в код страницы.
    </p>
    <div class="description">
        Используется для:
        <ul>
            <li>Освобождения ресурсов. 
                Например: закрытие подключений к базам данных, файловых дескрипторов и т.д.</li>
        </ul>

    </div>
    <br />
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
    <br />
    <asp:Label ID="Output" runat="server" EnableViewState="False" />
</body>
</html>
