using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _05_ServerControlInheritanceButton : Button
    {
        public string Caption
        {
            get
            {
                object caption = ViewState["Caption"];
                if (caption == null)
                {
                    return string.Empty;
                }
                return caption.ToString();
            }
            set { ViewState["Caption"] = value; }
        }        
        
        // Переопределение метода, который формирует html контент для вывода на странице.
        protected override void Render(HtmlTextWriter writer)
        {
            // Вывод открывающих тэгов fieldset и legend.
            writer.WriteFullBeginTag("fieldset");
            writer.WriteFullBeginTag("legend");
            
            // Вывод данных из свойств caption текущего объекта
            writer.Write(Caption);

            // Вывод закрывающего тэга legend.
            writer.WriteEndTag("legend");

            // Запуск метода генерации html из базового класса (отрисовка кнопки).
            base.Render(writer);

            // Вывод закрывающего тэга fieldset.
            writer.WriteEndTag("fieldset");

            // Другой способ реализации метода Render.
            //writer.Write("<fieldset><legend>");
            //writer.Write(Caption);
            //writer.Write("</legend>");
            //base.Render(writer);
            //writer.Write("</fieldset>");
        }
    }
}
