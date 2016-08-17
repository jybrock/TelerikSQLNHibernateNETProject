using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BusinessServices;

namespace WebApplication
{
    public class BasePage : System.Web.UI.Page
    {
        private Guid _UserId;

        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BasePage()
        {
            SetCurrentUser();
        }

        /// <summary>
        /// Initialize method for BasePage.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// Get the current logged on user
        /// </summary>
        /// <returns></returns>
        protected MembershipUser GetCurrentUser()
        {
            MembershipUser membershipUser = null;

            if (Page.User.Identity.IsAuthenticated)
            {
                membershipUser = Membership.GetUser(Page.User.Identity.Name);
            }

            return membershipUser;
        }

        protected void SetCurrentUser()
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                UserId = (Guid)Membership.GetUser(Page.User.Identity.Name).ProviderUserKey;
                UserName = Membership.GetUser(Page.User.Identity.Name).UserName;
            }
        }

        protected DateTime GetLastLogin()
        {
            return Membership.GetUser(Page.User.Identity.Name).LastLoginDate;
        }

        /// <summary>
        /// Check if user is in role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        protected bool UserInRole(string role)
        {
            return Roles.IsUserInRole(GetCurrentUser().UserName, role);
        }
    }
}