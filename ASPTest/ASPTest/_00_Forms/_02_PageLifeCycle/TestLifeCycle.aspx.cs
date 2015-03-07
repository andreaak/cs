using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPTest._00_Forms._02_PageLifeCycle
{
    public partial class TestLifeCycle : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Response.Write("<p>Page_PreInit</p>");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("<p>Page_Init</p>");
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            Response.Write("<p>Page_InitComplete</p>");
            //var test1 = ViewState["Test1"] as string; // no sense. ViewState is not recovered 
            //var test2 = ViewState["Test2"] as string; // no sense. ViewState is not recovered 
            //ViewState["Test1"] = "test11"; // no sense. Will be lost 
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
            Response.Write("<p>Page_PreLoad</p>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<p>Page_Load</p>");

            if (IsPostBack)
            {
                try
                {
                    Response.Redirect("~/default.aspx");
                }
                catch (Exception ex)
                {
                    string message = string.Format("<p>{0}</p>", ex.Message);
                    Response.Write(message);
                }
                finally
                {

                }
                Response.Redirect("~/default.aspx");
            }
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

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Response.Write("<p>Page_LoadComplete</p>");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("<p>Page_PreRender</p>");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            ViewState["Test1"] = "test1";
            Response.Write("<p>Page_PreRenderComplete</p>");
        }

        //protected void Page_SaveState(object sender, EventArgs e)
        //{
        //    Response.Write("<p>Page_SaveState</p>");
        //}

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            //ViewState["Test2"] = "test2"; // no sense. View state is saved
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
    
    }
}