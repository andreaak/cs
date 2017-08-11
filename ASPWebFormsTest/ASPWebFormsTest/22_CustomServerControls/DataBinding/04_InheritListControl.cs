using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    public class _04_InheritListControl : ListControl
    {
        // Элемент управления, который будет повторяться при формировании списка.
        private HyperLink _repeatControl;
        private HyperLink RepeateControl
        {
            get
            {
                if (_repeatControl == null)
                {
                    _repeatControl = new HyperLink();
                }
                return _repeatControl;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                HyperLink control = RepeateControl;

                // Присвоение ссылке стилей контрола HyperLinkList.
                control.ApplyStyle(this.ControlStyle);

                // Items - элементы которые добавлены в список через дизайнер или через код.
                control.NavigateUrl = Items[i].Value;
                control.Text = Items[i].Text;

                // Вывод ссылки в HtmlTextWriter
                control.RenderControl(writer);

                writer.Write("<br />");
            }
        }
    }
}
