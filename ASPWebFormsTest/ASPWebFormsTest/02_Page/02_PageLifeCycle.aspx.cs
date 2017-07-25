using System;

namespace ASPWebFormsTest._02_Page
{
    public partial class _02_PageLifeCycle : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Response.Write($"<p>IsPostBack: {IsPostBack}</p>");

            Response.Write("<p>Page_PreInit</p>");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("<p>Page_Init</p>");
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            // no sense. ViewState not loaded  
            var test1 = ViewState["Test1"] as string; 
            var test2 = ViewState["Test2"] as string;
            Response.Write("<p>Page_InitComplete<br/>");
            Response.Write($"Test1: {test1}<br/>");
            Response.Write($"Test2: {test2}<br/>");
            Response.Write("</p>");

            // no sense. ViewState will be overrided on PostBack  
            ViewState["Test3"] = "ViewState_Page_InitComplete";
        }

        //protected void Page_LoadState(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_LoadState</p>");
        //}

        //protected void Page_ProcessPostData(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_ProcessPostData</p>");
        //}

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            var test1 = ViewState["Test1"] as string;
            var test2 = ViewState["Test2"] as string;
            var test3 = ViewState["Test3"] as string;
            Response.Write("<p>Page_PreLoad<br/>");
            Response.Write($"Test1: {test1}<br/>");
            Response.Write($"Test2: {test2}<br/>");
            Response.Write($"Test3: {test3}<br/>");
            Response.Write("</p>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<p>Page_Load</p>");
        }

        //protected void Page_ProcessPostData2(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_ProcessPostData2</p>");
        //}

        //protected void Page_ChangedEvents(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_ChangedEvents</p>");
        //}

        //protected void Page_PostBackEvent(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_PostBackEvent</p>");
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<p>Button1_Click</p>");
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Write("<p>Page_LoadComplete</p>");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("<p>Page_PreRender</p>");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            ViewState["Test1"] = "ViewState_Page_PreRenderComplete";
            ViewState["Test3"] = "ViewState_Page_PreRenderComplete_3";
            Response.Write("<p>Page_PreRenderComplete</p>");
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_SaveState</p>");
        //}

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            ViewState["Test2"] = "ViewState_Page_SaveStateComplete"; // no sense. View state is saved
            Response.Write("<p>Page_SaveStateComplete</p>");
        }

        //protected void Page_Render(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_Render</p>");
        //}

        protected void Page_Unload(object sender, EventArgs e)
        {
            //Response.Write("<p>Page_Unload</p>"); // Can't write in response - exception
        }

        protected void Page_Disposed(object sender, EventArgs e)
        {
            //Response.Write("<p>Page_Disposed</p>"); //Can't write in response
        }

        /* 
        IsPostBack: False

        Page_PreInit

        Page_Init

        Page_InitComplete
        Test1: 
        Test2: 

        Page_PreLoad
        Test1: 
        Test2: 
        Test3: ViewState_Page_InitComplete

        Page_Load

        Page_LoadComplete

        Page_PreRender

        Page_PreRenderComplete

        Page_SaveStateComplete
        */

        /*
        IsPostBack: True

        Page_PreInit

        Page_Init

        Page_InitComplete
        Test1: 
        Test2: 

        Page_PreLoad
        Test1: ViewState_Page_PreRenderComplete
        Test2: 
        Test3: ViewState_Page_PreRenderComplete_3

        Page_Load

        Button1_Click

        Page_LoadComplete

        Page_PreRender

        Page_PreRenderComplete

        Page_SaveStateComplete         
        */
    }
}