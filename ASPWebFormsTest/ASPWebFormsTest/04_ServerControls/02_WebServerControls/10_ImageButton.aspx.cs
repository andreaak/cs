using System;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _10_ImageButton : System.Web.UI.Page
    {
        protected void ImageButton_Click(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            if (button != null)
            {
                button.ImageUrl = "Images/button_pressed.jpg";
            }
        }
    }
}