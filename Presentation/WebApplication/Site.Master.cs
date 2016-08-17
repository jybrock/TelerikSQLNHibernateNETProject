using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebApplication
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Page.User.Identity.IsAuthenticated && !Roles.IsUserInRole(GetCurrentUser().UserName, "Admin")) || !Page.User.Identity.IsAuthenticated)
            {
                var mi = NavigationMenu.FindItem("Users");
                NavigationMenu.Items.Remove(mi);

                if (!Page.User.Identity.IsAuthenticated)
                {
                    var mi1 = NavigationMenu.FindItem("Profile");
                    NavigationMenu.Items.Remove(mi1);
                }
            }
            
            
        }

        protected MembershipUser GetCurrentUser()
        {
            MembershipUser membershipUser = null;

            if (Page.User.Identity.IsAuthenticated)
            {
                membershipUser = Membership.GetUser(Page.User.Identity.Name);
            }

            return membershipUser;
        }
    }
}
