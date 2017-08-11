using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    // Тип контейнера обязательно должен реализовывать интерфейс INamingContainer.
    public class _05_MessageTemplateContainer : WebControl, INamingContainer
    {
        private _05_TemplatedControl _parent;

        public string MessageBody { get { return _parent.MessageBody; } }

        public _05_MessageTemplateContainer(_05_TemplatedControl parent)
        {
            _parent = parent;
        }
    }
}