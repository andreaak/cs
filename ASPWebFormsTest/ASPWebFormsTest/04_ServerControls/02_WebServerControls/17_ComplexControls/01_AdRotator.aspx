<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="01_AdRotator.aspx.cs" Inherits="ASPWebFormsTest._04_ServerControls._02_WebServerControls._17_ComplexControls._01_AdRotator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AdRotator Control</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                Все настройки элемента управления находятся в файле AdvertismentFile.xml<br />
                Основные тэги:<br />
                ImageUrl - путь к изображению, которое будет отображаться<br />
                NavigateUrl - путь по которому будет происходить перенаправление в случае если пользователь кликнет баннер<br />
                AlternateText - альтернативный текст который будет отображаться если изображение не удалось загрузить или в браузере пользователя отключены изображения.<br />
                Impressions - Значимость объявления по сравнению с остальными объявлениями<br />
                Keyword - Категория рекламы.<br />
                При установке атрибута KeywordFilter объявления из файла будут отображаться только в том случае,
                    если содержит то же ключевое слово  в теге Keyword в файле AdvertismentFile.xml<br />
                Target="_blank" - открытие ссылки в новой вкладке
            </p>

            <asp:AdRotator ID="AdRotator1" runat="server" AdvertisementFile="01_AdRotator.xml"
                Target="_blank" />
        </div>
    </form>
</body>
</html>
