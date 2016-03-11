using System.Web.Mvc;

namespace _01_ASPMVCTest.Infrastructure
{
    /*
    Внешний вспомогательный метод – расширяющий метод класса HtmlHelper, который используется для создания HTML разметки. 
    Для получения доступа к методу используется свойство Html представления. 
    Внешний вспомогательный метод, в отличии от встраиваемых, может использоваться в нескольких представлениях. 
    
    TagBuilder – класс, который используется при создание вспомогательных методов для генерации HTML разметки. 
    Свойство или метод	            Описание
    InnerHtml	                    Устанавливает в качестве контента HTML строку. Значение не кодируется.
    SetInnerText(текс) 	            Устанавливает в качестве контента текст. Значение кодируется.
    AddCssClass(имя класса) 	    Добавляет к элементу атрибут class с определенным значением.
    MergeAttribute(имя, значение) 	Добавляет к элементу атрибута с указанным значением
    */
    public static class CustomHelperMethods
    {
        // Для того чтобы использовать данный вспомогательный метод, необходимо импортировать пространство имен _02_ExternalHelperMethods.Infrastructure.
        // В данном проекте, пространство имен импортировано с помощью файла Views/web.config
        public static MvcHtmlString UnorderedList(this HtmlHelper helper, string[] items)
        {
            // TagBuilder - класс для создания HTML элементов.
            TagBuilder tag = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder liTag = new TagBuilder("li");
                liTag.SetInnerText(item);
                tag.InnerHtml += liTag.ToString();
            }

            // MvcHtmlString - представляет HTML кодированную строку, которая не должна кодироваться повторно.
            // Необходимо самостоятельно позаботится о кодировании тех данных, с которыми связан риск XSS атаки
            return new MvcHtmlString(tag.ToString());
        }
    }
}

namespace _01_ASPMVCTest.Infrastructure2
{
    public static class CustomHelperMethods
    {
        public static MvcHtmlString ErrorMessage(this HtmlHelper helper, string message)
        {
            TagBuilder tag = new TagBuilder("p");
            tag.MergeAttribute("style", "color:red; padding:8px; border:1px solid red; border-radius:3px;");
            tag.SetInnerText(message);

            return new MvcHtmlString(tag.ToString());
        }
    }
}