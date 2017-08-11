using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    // Тип контейнера обязательно должен реализовывать интерфейс INamingContainer.
    public class _05_HeaderTemplateContainer : WebControl, INamingContainer
    {
        private readonly _05_TemplatedControl _parent;

        /*
        В хост странице при задании разметки в элементе HeaderTemplate 
        можно использовать привязку к следующим свойствам.
        */
        public string Title { get { return _parent.Title; } }

        public string Author { get { return _parent.Author; } }

        public _05_HeaderTemplateContainer(_05_TemplatedControl parent)
        {
            _parent = parent;
        }
    }
}