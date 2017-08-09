using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _04_ServerControlInheritanceHyperLink : HyperLink
    {
        // Замещаем свойство NavigateUrl из базового класса HyperLink
        public new string NavigateUrl
        {
            get
            {
                return base.NavigateUrl;
            }
            set
            {
                base.NavigateUrl = value + "?test=value";
            }
        }
    }
}
