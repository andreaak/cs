﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_BaseInfo.aspx.cs" Inherits="ASPWebFormsTest._23_CachingData._01_BaseInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>
        Кэширование - техника хранения в памяти некоторой информации, 
        которую дорого получать повторно или пересоздавать. 
        Например, можно кэшировать результаты сложных запросов 
        или ответы от удалённых сервисов.
    </p>
    <p>Типы кэширования в ASP.NET</p>
    <ul>
        <li>Кэширование данных</li>
        <li>Кэширование вывода</li>
    </ul>
    <p>
        Кэширование данных - один из типов кэширования в ASP.NET при котором 
        определенные объекты добавляются в специальный встроенный объект коллекции (Cache).
        Данный объект работает подобно объекту Application но имеет следующие отличия:
    </p>
    <ul>
        <li>Является безопасным в отношении потоков.</li>
        <li>Элементы из кэша удаляются автоматически.</li>
        <li>Элементы кэша поддерживают зависимости.</li>
    </ul>
    <p>
        Кэш привязан к процессу, это означает, 
        что он не будет существовать после перезапуска веб приложения.
    </p>
    <img src="cache.png"/>
    <h4>Свойства</h4>
    <p>
        Count - количество элементов в кэше.<br />
        Items - индексатор для доступа к записям кэша по ключу.<br />
        NoAbsoluteExpiration - значение указывает что кэшированная запись не устаревает.<br />
        NoSlidingExpiration - значение указывает что для кэшированной записи 
        отключен режим скользящего устаревания.
    </p>
    <h4>Методы</h4>
    <p>
        Add - Добавляет заданный элемент. Если элемент есть в кэше - возвращается этот элемент,
        но замена на новый не происходит.
        <br />
        Get - Получение значение заданного элемента. Если его нет возвращается значение null.
        <br />
        Insert - Добавляет в кэш заданный элемент. В отличие от Addделает замену, 
        если такой элемент кэширован.<br />
        Remove - Удаление указанного элемента по ключу.
    </p>
    <p>При добавлении записи в кэш можно указать следующие параметры: </p>
    <ul>
        <li>Ключ</li>
        <li>Значение</li>
        <li>Зависимость - объект, который описывает связь записи c внешним ресурсом 
            (например, с базой данных или файлом на диске). 
            При изменении ресурса кэшированная запись удаляется.</li>
        <li>Абсолютное время устаревания - фиксированное время жизни записи. 
            Задается с помощью DateTime</li>
        <li>Скользящее время устаревания - определяет &laquo;скользящее&raquo; 
            время жизни записи. При обращении к записи, если время не истекло, 
            то оно продлевается до максимального значения.
        </li>
        <li>Приоритет - приоритет записи, который может повлиять на то 
            как быстро запись будет удалена из кэша в том случае 
            если система начнет освобождать оперативную память. Задается с помощью TimeSpan.
        </li>
        <li>Функция обратного вызова - функция, которая будет запущена 
            в случае если запись удалиться из кэша.</li>
    </ul>
    <h4>CacheDependency</h4>
    <p>
        Зависимость кэша(класс CacheDependency) - объект, который позволяет сделать
        кэшированную запись зависимой от другого ресурса.
        При изменении этого ресурса запись автоматически удаляется.
    </p>
    <h4>Типы зависимостей:</h4>
    <ul>
        <li>Зависимость от файлов</li>
        <li>Зависимость от других кэшированных элементов</li>
        <li>Зависимость от базы данных</li>
    </ul>
    <p>Настройка кэширования при использовании SqlDataSource</p>
    <p>
        <strong>EnableCaching</strong> - true - кэширование включено. По умолчанию - false.<br />
        <strong>CacheExpirationPolicy-Absolute</strong> - время нахождения объекта в кэше 
        будет фиксированным.<br />
        <strong>Sliding</strong> - временное окно сбрасывается прикаждом обращении к кэшу.<br />
        <strong>CacheDuration</strong> - Время в секундах нахождения объекта в кэше.
    </p>
</body>
</html>