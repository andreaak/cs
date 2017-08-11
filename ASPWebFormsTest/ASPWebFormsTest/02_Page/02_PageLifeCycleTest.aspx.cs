using System;

namespace ASPWebFormsTest._02_Page
{
    public partial class _02_PageLifeCycleTest : System.Web.UI.Page
    {
        // Событие используется для динамической установки MasterPage, смены темы.
        private void Page_PreInit(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_PreInit"));
            Response.Write(PrepareMessageTextBox());
        }

        // Для чтения или инициализации свойств элементов управления.
        private void Page_Init(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_Init"));
            Response.Write(PrepareMessageTextBox());
        }

        // Для действий, которые должны произвестись после всех инициализаций. 
        // До этого момента данные сохраненные во ViewState не сериализуються.
        private void Page_InitComplete(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_InitComplete"));
            Response.Write(PrepareMessageTextBox());
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //}

        // Для выполнения действий после загрузки ViewState и до события Load.
        private void Page_PreLoad(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_PreLoad"));
            Response.Write(PrepareMessageTextBox());
        }

        // Используется для установки и инициализаций свойств открытия подключений к базам данных.
        private void Page_Load(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_Load"));
            Response.Write(PrepareMessageTextBox());
        }

        //protected void Page_ProcessPostData2(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ChangedEvents(object sender, EventArgs e)
        //{
        //}

        protected void TextBox1_OnTextChanged(object sender, EventArgs e)
        {
            Response.Write(PrepareEmphasizedMessage("TextBox1_OnTextChanged"));
            Response.Write(PrepareMessageTextBox());
        }

        //protected void Page_PostBackEvent(object sender, EventArgs e)
        //{
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(PrepareEmphasizedMessage("Button1_Click"));
            Response.Write(PrepareMessageTextBox());
        }

        // Для действий, которые должны происходить после выполнения обработчиков событий элементов управления.
        private void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_LoadComplete"));
            Response.Write(PrepareMessageTextBox());
        }

        // Для внесения окончательных изменений перед рендерингом страницы.
        private void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_PreRender"));
            Response.Write(PrepareMessageTextBox());
        }

        // Используется при разработке асинхронных страниц.
        private void Page_PreRenderComplete(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_PreRenderComplete"));
            Response.Write(PrepareMessageTextBox());
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //}

        // Сохранение ViewState. Последнее событие перед рендерингом.
        private void Page_SaveStateComplete(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Page_SaveStateComplete"));
            Response.Write(PrepareMessageTextBox());
        }

        //protected void Page_Render(object sender, EventArgs e)
        //{
        //}

        // Рендеринг страницы. Все элементы управления превращаются в HTML, CSS и JavaScript, который будет отправлен клиенту.

        private void Page_Unload(object sender, EventArgs e)
        {
            // Освобождение ресурсов, которые использовала страница.
        }

        private void Page_Disposed(object sender, EventArgs e)
        {
        }

        #region Help methods
        private string PrepareMessage(string message)
        {
            return string.Format("<div style='background-color:lightgreen; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }

        private string PrepareEmphasizedMessage(string message)
        {
            return string.Format("<div style='background-color:purple; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }
        #endregion
        private string PrepareMessageTextBox()
        {
            string message = string.Format("TextBox1.Text: {0}", TextBox1.Text);
            return string.Format("<div style='background-color:aquamarine; border:1px solid black; padding:12px;margin:4px; margin-left: 20px; '>{0}</div>", message);
        }
    }
}