using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _06_ServerControlRenderContents : Panel
    {
        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            writer.Write("Пользовательский контент панели");
        }
    }
}
