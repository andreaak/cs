using System;

namespace ASPWebFormsTest._08_DataBinding
{
    public partial class _02_SingleValueBinding : System.Web.UI.Page
    {
        protected string FirstName
        {
            get { return "Jhon"; }
        }

        protected string LastName
        {
            get { return "Doe"; }
        }

        protected int size = 24;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Если не вызвать данный метод, выражения привязки не прочтут значения из свойств страницы.
                this.DataBind(); 
            }
        }
    }
}