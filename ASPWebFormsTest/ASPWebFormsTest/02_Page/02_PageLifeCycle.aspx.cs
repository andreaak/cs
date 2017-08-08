using System;

namespace ASPWebFormsTest._02_Page
{
    public partial class _02_PageLifeCycle : System.Web.UI.Page
    {
        // Событие используется для динамической установки MasterPage, смены темы.
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        // Для чтения или инициализации свойств элементов управления.
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        // Для действий, которые должны произвестись после всех инициализаций. 
        // До этого момента данные сохраненные во ViewState не сериализуються.
        protected void Page_InitComplete(object sender, EventArgs e)
        {
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //}

        // Для выполнения действий после загрузки ViewState и до события Load.
        protected void Page_PreLoad(object sender, EventArgs e)
        {
        }

        // Используется для установки и инициализаций свойств открытия подключений к базам данных.
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //protected void Page_ProcessPostData2(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ChangedEvents(object sender, EventArgs e)
        //{
        //}

        //protected void Page_PostBackEvent(object sender, EventArgs e)
        //{
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        // Для действий, которые должны происходить после выполнения обработчиков событий элементов управления.
        private void Page_LoadComplete(object sender, EventArgs e)
        {
        }

        // Для внесения окончательных изменений перед рендерингом страницы.
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        // Используется при разработке асинхронных страниц.
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //}

        // Сохранение ViewState. Последнее событие перед рендерингом.
        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
        }

        //protected void Page_Render(object sender, EventArgs e)
        //{
        //}

        // Рендеринг страницы. Все элементы управления превращаются в HTML, CSS и JavaScript, который будет отправлен клиенту.

        protected void Page_Unload(object sender, EventArgs e)
        {
            // Освобождение ресурсов, которые использовала страница.
        }

        protected void Page_Disposed(object sender, EventArgs e)
        {
        }
    }
}