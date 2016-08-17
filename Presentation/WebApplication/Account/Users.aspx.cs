using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessServices;
using DomainObjects;

namespace WebApplication.Account
{
    public partial class Users : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && UserInRole("Admin"))
            {
                AddUserButton.Visible = true;
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, EventArgs e)
        {
            User[] users = UserFacade.GetAllUsers();
            RadGrid1.DataSource = users;
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/UserProfile.aspx?AddNew=TRUE");
        }
    }
}