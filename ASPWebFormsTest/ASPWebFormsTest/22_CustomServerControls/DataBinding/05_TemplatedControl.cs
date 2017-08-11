using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    public class _05_TemplatedControl : WebControl
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string MessageBody { get; set; }

        // Эта ссылка будет хранить шаблон для заголовка сообщения.
        ITemplate _headerTemplate;
        /*
        TemplateContainer - задает тип контейнера именования, 
        в котором будет хранится установленный шаблон.
        PersistenceMode - указывает, как осуществляется сохранение значения 
        данного свойства в хост странице.
        */
        [TemplateContainer(typeof(_05_HeaderTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate HeaderTemplate
        {
            get { return _headerTemplate; }
            set { _headerTemplate = value; }
        }

        // Эта ссылка будет хранить шаблон для тела сообщения.
        ITemplate _messageTemplate;

        [TemplateContainer(typeof(_05_MessageTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        // Шаблон сообщения, которое выводит элемент  управления.
        public ITemplate MessageTemplate
        {
            get { return _messageTemplate; }
            set { _messageTemplate = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Если шаблон был установлен в хост странице.
            if (_headerTemplate != null)
            {
                /*
                Инициализируем контейнер для данного шаблона.
                При вызове конструктора передаем указатель на текущий объект,
                так как в HeaderTemplateContainer могут потребоваться данные из этого объекта.
                */
                _05_HeaderTemplateContainer template = new _05_HeaderTemplateContainer(this);

                // Инициалищируем шаблон в указанном контейнере.
                HeaderTemplate.InstantiateIn(template);

                // Добавляем контейнер в дерево элементов управления.
                Controls.Add(template);
            }
            else
            {
                Label control = new Label();
                control.Text = Title + " [ " + Author + " ] ";
                Controls.Add(control);
            }
            if (_messageTemplate != null)
            {
                _05_MessageTemplateContainer templateContainer = new _05_MessageTemplateContainer(this);
                MessageTemplate.InstantiateIn(templateContainer);
                Controls.Add(templateContainer);
            }
            else
            {
                Label control = new Label();
                control.Text = "<br />" + MessageBody;
                Controls.Add(control);
            }

            /*
            Для того что бы сработали все выражения привязок <%# ... %>, 
            используемых в шаблоне, вызываем метод DataBind.
            */
            DataBind();

            base.Render(writer);
        }
    }
}
