using System;

namespace ASPWebFormsTest._21_UserControls
{
    public partial class _01_UserControl : System.Web.UI.UserControl
    {
        // Для чтения или инициализации свойств элементов управления.
        private void Page_Init(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Control_Init"));
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //}

        // Используется для установки и инициализаций свойств открытия подключений к базам данных.
        private void Page_Load(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Control_Load"));
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

        protected void TestButton_Click(object sender, EventArgs e)
        {
            TestTextBox.Text = "Hello User Control!";
            Response.Write(PrepareEmphasizedMessage("Control_TestButton_Click"));
        }

        // Для внесения окончательных изменений перед рендерингом страницы.
        private void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write(PrepareMessage("Control_PreRender"));
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //}

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
            return string.Format("<div style='background-color:yellow; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }

        private string PrepareEmphasizedMessage(string message)
        {
            return string.Format("<div style='background-color:aqua; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }
        #endregion

    }
}