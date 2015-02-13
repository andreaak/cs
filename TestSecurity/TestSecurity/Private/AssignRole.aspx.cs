using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestSecurity.Private
{
    public partial class AssignRole : System.Web.UI.Page
    {

        #region EVENTS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            ListBoxRoles.DataSource = Roles.GetAllRoles();
            ListBoxRoles.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            List<string> names = GetSelectedRoles();
            if(names.Count != 0)
            {
                Roles.AddUserToRoles(UserName.Text, names.ToArray());
            }
            ListBoxRoles.SelectedValue = null;
            UserName.Text = string.Empty;
        }

        protected void CustomValidatorLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string userName = args.Value;
            MembershipUser user = Membership.GetUser(userName);
            args.IsValid = user != null;
        }

        #endregion

        private List<string> GetSelectedRoles()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < ListBoxRoles.Items.Count; i++)
            {
                ListItem current = ListBoxRoles.Items[i];
                if (current.Selected)
                {
                    names.Add(current.Value);
                }
            }
            return names;
        }

    }
}