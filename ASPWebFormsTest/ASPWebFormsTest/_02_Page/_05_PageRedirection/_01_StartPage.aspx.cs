using System;

namespace ASPWebFormsTest._02_Page._05_PageRedirection
{
    public partial class _01_StartPage : System.Web.UI.Page
    {
        string response = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            response += ("<p>Page_Load</p>\n");

            if (IsPostBack)
            {
                try
                {
                    Response.Redirect("_01_DestinationPage.aspx?id=1");
                }
                catch (Exception ex)
                {
                    string message = string.Format("<p>{0}</p>", ex.ToString());
                    response += (message + "\n");
                }
                finally
                {

                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            response += ("<p>Page_PreInit</p>\n");
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            response += ("<p>Page_Init</p>\n");
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            response += ("<p>Page_InitComplete</p>\n");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            response += ("<p>Button1_Click</p>\n");
        }

        private void Page_PreLoad(object sender, EventArgs e)
        {
            response += ("<p>Page_PreLoad</p>\n");
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            response += ("<p>Page_LoadComplete</p>\n");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            response += ("<p>Page_PreRender</p>\n");
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            response += ("<p>Page_PreRenderComplete</p>\n");
        }

        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            response += ("<p>Page_SaveStateComplete</p>\n");
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            response += ("<p>Page_Unload</p>\n"); 
        }

        protected void Page_Disposed(object sender, EventArgs e)
        {
            response += ("<p>Page_Disposed</p>");
        }
    }
}