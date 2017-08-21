using System.Web.Mvc;

namespace _01_ASPMVCTest.Infrastructure
{
    public static class CustomHelperMethods
    {
        /* 
        Для того чтобы использовать данный вспомогательный метод, 
        необходимо импортировать пространство имен _02_ExternalHelperMethods.Infrastructure.
        В данном проекте, пространство имен импортировано 
        с помощью файла Views/web.config
        */
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

            /*
            MvcHtmlString - представляет HTML кодированную строку, 
            которая не должна кодироваться повторно.
            Необходимо самостоятельно позаботится о кодировании тех данных, 
            с которыми связан риск XSS атаки
            */
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString ErrorMessage(this HtmlHelper helper, string message)
        {
            TagBuilder tag = new TagBuilder("p");
            tag.MergeAttribute("style", "color:red; padding:8px; border:1px solid red; border-radius:3px;");
            tag.SetInnerText(message);

            return new MvcHtmlString(tag.ToString());
        }
    }
}