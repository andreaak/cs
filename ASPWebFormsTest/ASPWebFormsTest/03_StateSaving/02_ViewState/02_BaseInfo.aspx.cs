using System;
using System.Text;

namespace ASPWebFormsTest._03_StateSaving._02_ViewState
{
    public partial class _02_BaseInfo : System.Web.UI.Page
    {
        private StringBuilder _output = new StringBuilder();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _output.Clear();
            _output.Append($"<p><span class=\"bold\">IsPostBack: {IsPostBack}</span></p>");
            _output.Append("<p><span class=\"bold\">Page_PreInit</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _output.Append("<p><span class=\"bold\">Page_Init</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            // no sense. ViewState not loaded  
            var test1 = ViewState["Test1"] as string;
            var test2 = ViewState["Test2"] as string;
            _output.Append("<p><span class=\"bold\">Page_InitComplete</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}<br/>");
            _output.Append($"Test1: {test1}<br/>");
            _output.Append($"Test2: {test2}<br/>");
            _output.Append("</p>");

            // no sense. ViewState will be overrided on PostBack  
            ViewState["Test3"] = "ViewState_Page_InitComplete";
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //}

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            var test1 = ViewState["Test1"] as string;
            var test2 = ViewState["Test2"] as string;
            var test3 = ViewState["Test3"] as string;
            _output.Append("<p><span class=\"bold\">Page_PreLoad</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}<br/>");
            _output.Append($"Test1: {test1}<br/>");
            _output.Append($"Test2: {test2}<br/>");
            _output.Append($"Test3: {test3}<br/>");
            _output.Append("</p>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _output.Append("<p><span class=\"bold\">Page_Load<br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
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
            _output.Append("<p><span class=\"bold\">Button1_Click</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            _output.Append("<p><span class=\"bold\">Page_LoadComplete</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            _output.Append("<p><span class=\"bold\">Page_PreRender</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}");
            _output.Append("</p>");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            ViewState["Test1"] = "ViewState_Page_PreRenderComplete";
            ViewState["Test3"] = "ViewState_Page_PreRenderComplete_3";
            _output.Append("<p><span class=\"bold\">Page_PreRenderComplete</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}<br/>");
            _output.Append("</p>");
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //}

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            ViewState["Test2"] = "ViewState_Page_SaveStateComplete"; // no sense. View state is saved
            _output.Append("<p><span class=\"bold\">Page_SaveStateComplete</span><br/>");
            _output.Append($"TextBox1: {TextBox1.Text}<br/>");
            _output.Append("</p>");

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