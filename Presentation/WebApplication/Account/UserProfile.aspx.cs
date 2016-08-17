using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessServices;
using DomainObjects;

namespace WebApplication.Account
{
    public partial class UserProfile : BasePage
    {
        bool addNew = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AddNew"] != null && Request.QueryString["AddNew"] == "TRUE" && UserInRole("Admin"))
            {
                addNew = true;
                UserNameTextBox.ReadOnly = false;
                UserNameRFV.Enabled = true;
                PasswordRFV.Enabled = true;
            }
            else if (UserInRole("Admin"))
            {
                ActiveTR.Visible = true;
            }

            if (!IsPostBack)
            {
                if (!addNew)
                    BindData();
            }
        }

        private void BindData()
        {
            Guid userId;
            User user = null;

            if (UserInRole("Admin") && Request.QueryString["UserID"] != null)
            {
                userId = new Guid(Request.QueryString["UserID"]);
                user = UserFacade.GetUserById(userId);
            }
            else
            {
                user = UserFacade.GetUserById(UserId);
            }

            if (UserInRole("Admin"))
                RoleTR.Visible = true;

            ActiveCheckBox.Checked = user.IsApproved;

            if (Roles.IsUserInRole(user.UserName, "Admin"))
                RoleDDL.SelectedValue = "Admin";
            else if (Roles.IsUserInRole(user.UserName, "Developer"))
                RoleDDL.SelectedValue = "Developer";
            else if (Roles.IsUserInRole(user.UserName, "Client"))
                RoleDDL.SelectedValue = "Client";

            if (user != null)
            {
                UserNameTextBox.Text = user.UserName;
                FirstNameTextBox.Text = user.FirstName;
                LastNameTextBox.Text = user.LastName;
                EmailTextBox.Text = user.Email;
                PhoneRadMaskedTextBox.Text = user.PhoneNumber;
                ExtensionRadMaskedTextBox.Text = user.Extension;
                AddressTextBox.Text = user.Address;
                CityTextBox.Text = user.City;
                StateTextBox.Text = user.State;
                ZipCodeTextbox.Text = user.ZipCode;
            }
        }

        protected void Submit_OnClick(object sender, EventArgs e)
        {
            Guid userId;
            User user = null;

            if (addNew)
            {
                user = new User();

                user.IsApproved = true;
                user.UserName = UserNameTextBox.Text.Trim();
                user.Password = PasswordTextBox.Text.Trim();
                user.FirstName = FirstNameTextBox.Text.Trim();
                user.LastName = LastNameTextBox.Text.Trim();
                user.Email = EmailTextBox.Text.Trim();
                user.PhoneNumber = PhoneRadMaskedTextBox.Text;
                user.Extension = ExtensionRadMaskedTextBox.Text;
                user.Address = AddressTextBox.Text.Trim();
                user.City = CityTextBox.Text.Trim();
                user.State = StateTextBox.Text.Trim();
                user.ZipCode = ZipCodeTextbox.Text.Trim();


                Guid membershipUser;

                if (UserFacade.SaveUser(user, UserId, out membershipUser))
                {
                    ErrorMessage.Text = "User Added";

                    if (RoleDDL.SelectedValue != "-1")
                    {
                        if (!Roles.IsUserInRole(user.UserName, RoleDDL.SelectedValue))
                            Roles.AddUserToRole(user.UserName, RoleDDL.SelectedValue);
                    }
                }
                else
                {
                    ErrorMessage.Text = "Unable to Add User";
                }

                
            }
            else
            {
                if (UserInRole("Admin") && Request.QueryString["UserID"] != null)
                {
                    userId = new Guid(Request.QueryString["UserID"]);
                    user = UserFacade.GetUserById(userId);
                }
                else
                {
                    user = UserFacade.GetUserById(UserId);
                }

                if (user != null)
                {
                    if (!UserInRole("Admin"))
                    {
                        if (Roles.IsUserInRole(user.UserName, "Admin"))
                            Roles.RemoveUserFromRole(user.UserName, "Admin");
                        if (Roles.IsUserInRole(user.UserName, "Developer"))
                            Roles.RemoveUserFromRole(user.UserName, "Developer");
                        if (Roles.IsUserInRole(user.UserName, "Client"))
                            Roles.RemoveUserFromRole(user.UserName, "Client");

                        if (RoleDDL.SelectedValue != "-1")
                        {
                            if (!Roles.IsUserInRole(user.UserName, RoleDDL.SelectedValue))
                                Roles.AddUserToRole(user.UserName, RoleDDL.SelectedValue);
                        }
                    }

                    user.IsApproved = ActiveCheckBox.Checked;
                    user.FirstName = FirstNameTextBox.Text.Trim();
                    user.LastName = LastNameTextBox.Text.Trim();
                    user.Password = PasswordTextBox.Text.Trim();
                    user.Email = EmailTextBox.Text.Trim();
                    user.PhoneNumber = PhoneRadMaskedTextBox.Text;
                    user.Extension = ExtensionRadMaskedTextBox.Text;
                    user.Address = AddressTextBox.Text.Trim();
                    user.City = CityTextBox.Text.Trim();
                    user.State = StateTextBox.Text.Trim();
                    user.ZipCode = ZipCodeTextbox.Text.Trim();


                    Guid membershipUser;

                    if (UserFacade.SaveUser(user, UserId, out membershipUser))
                    {
                        ErrorMessage.Text = "Updated Successfully";
                    }
                    else
                    {
                        ErrorMessage.Text = "Unable to Update";
                    }
                }
            }

            
        }
    }
}