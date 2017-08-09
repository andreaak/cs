using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _03_ServerControlStyle : WebControl
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // WebControl имеет ряд свойств позволяющих сгенерировать CSS стили.
            writer.AddStyleAttribute("color", ForeColor.Name);
            writer.AddStyleAttribute("background-color", BackColor.Name);
            if (!Width.IsEmpty)
            {
                writer.AddStyleAttribute("width", Width.ToString());
            }
            if (!Height.IsEmpty)
            {
                writer.AddStyleAttribute("height", Height.ToString());
            }

            // Создание элемента div с учетом атрибутов, которые были установлены кодом выше
            writer.RenderBeginTag("div");
            writer.Write("Hello I'm WebControl");
            writer.RenderEndTag();
        }
    }
}
