using System;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    /*
    MessageDataItem - класс будет использоваться для того, чтобы хранить данные, 
    которые извлекались из источника данных.
    При первом обращении к источнику данных значения будут помещены в экземпляр MessageDataItem 
    и сохранены во ViewState
    При PostBack запросах данные будут извлекаться из экземпляра MessageDataItem. 
    Повторное обращение к источнику данных происходить не будет.
    */
    [Serializable]
    public class _02_MessageDataItem
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public _02_MessageDataItem()
        {
        }

        public _02_MessageDataItem(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
