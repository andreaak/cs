using System;
using System.Text;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    public partial class _05_LifeCycle : System.Web.UI.Page
    {
        private StringBuilder _output = new StringBuilder();
        private const string PreRenderCompleteS = "PreRenderComplete";
        private const string InitPreRenderCompleteS = "InitPreRenderCompleteS";
        private const string SaveStateCompleteS = "SaveStateComplete";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _output.Clear();
            _output.Append($"<p><span class=\"bold\">IsPostBack: {IsPostBack}</span></p>");
            _output.Append(PrepareMessage("Page_PreInit"));
            _output.Append(PrepareMessageTextBox());
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_Init"));
            _output.Append(PrepareMessageTextBox());
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_InitComplete"));
            // no sense. ViewState not loaded  
            _output.Append(PrepareMessageViewStates());
            _output.Append(PrepareMessageTextBox());

            // no sense. ViewState will be overrided on PostBack  
            ViewState[InitPreRenderCompleteS] = "ViewState_Page_InitComplete";
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //}

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_PreLoad"));
            _output.Append(PrepareMessageViewStates());
            _output.Append(PrepareMessageTextBox());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_Load"));
            _output.Append(PrepareMessageTextBox());
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
            _output.Append(PrepareEmphasizedMessage("Button1_Click"));
            _output.Append(PrepareMessageTextBox());
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_LoadComplete"));
            _output.Append(PrepareMessageTextBox());
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            _output.Append(PrepareMessage("Page_PreRender"));
            _output.Append(PrepareMessageTextBox());
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            ViewState[PreRenderCompleteS] = "ViewState_Page_PreRenderComplete";
            ViewState[InitPreRenderCompleteS] = "ViewState_Page_PreRenderComplete_3";
            _output.Append(PrepareMessage("Page_PreRenderComplete"));
            _output.Append(PrepareMessageTextBox());
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //}

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            ViewState[SaveStateCompleteS] = "ViewState_Page_SaveStateComplete"; // no sense. View state is saved
            _output.Append(PrepareMessage("Page_LoadComplete"));
            _output.Append(PrepareMessageTextBox());

            Output.Text = _output.ToString();
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

        #region Help methods
        private string PrepareMessage(string message)
        {
            return string.Format("<div style='background-color:lightgreen; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }

        private string PrepareEmphasizedMessage(string message)
        {
            return string.Format("<div style='background-color:purple; border:1px solid black; padding:12px;margin:4px;'>{0}</div>", message);
        }

        private string PrepareMessageTextBox()
        {
            string message = string.Format("TextBox1.Text: {0}", TextBox1.Text);
            return string.Format("<div style='background-color:aquamarine; border:1px solid black; padding:12px;margin:4px; margin-left: 20px; '>{0}</div>", message);
        }

        private string PrepareMessageViewStates()
        {
            var test1 = ViewState[PreRenderCompleteS] as string;
            var test2 = ViewState[SaveStateCompleteS] as string;
            var test3 = ViewState[InitPreRenderCompleteS] as string;


            string message =
                string.Format("Set in PreRenderComplete: {0}<br/>" +
                              "Set in SaveStateComplete: {1}<br/>" +
                              "Set in Init and PreRenderComplete: {2}",
                    test1, test2, test3);
            return string.Format("<div style='background-color:bisque; border:1px solid black; padding:12px;margin:4px;margin-left: 20px; '>{0}</div>", message);
        }
        #endregion

        /* 
        IsPostBack: False

        Page_PreInit
        TextBox1:

        Page_Init
        TextBox1:

        Page_InitComplete
        TextBox1: 
        Test1: 
        Test2: 

        Page_PreLoad
        TextBox1: 
        Test1: 
        Test2: 
        Test3: ViewState_Page_InitComplete

        Page_Load
        TextBox1:

        Page_LoadComplete
        TextBox1:

        Page_PreRender
        TextBox1:

        Page_PreRenderComplete
        TextBox1: 

        Page_SaveStateComplete
        TextBox1: 
        */

        /*
        IsPostBack: True

        Page_PreInit
        TextBox1:

        Page_Init
        TextBox1:

        Page_InitComplete
        TextBox1: 
        Test1: 
        Test2: 

        Page_PreLoad
        TextBox1: InTextBox
        Test1: ViewState_Page_PreRenderComplete
        Test2: 
        Test3: ViewState_Page_PreRenderComplete_3

        Page_Load
        TextBox1: InTextBox

        Button1_Click
        TextBox1: InTextBox

        Page_LoadComplete
        TextBox1: InTextBox

        Page_PreRender
        TextBox1: InTextBox

        Page_PreRenderComplete
        TextBox1: InTextBox

        Page_SaveStateComplete
        TextBox1: InTextBox       
        */
    }
}