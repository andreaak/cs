using System.Web.UI;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _02_SimpleServerControl : Control
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
            
            writer.Write("Hello I'm Specialized Control!");
        }
    }
}
