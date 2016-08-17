using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Profile;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using DomainObjects;

namespace BusinessServices
{
    public partial class UserFacade
    {
        #region User

        public static bool SaveUser(User user, Guid userId, out Guid memberId)
        {
            bool retVal = true;
            memberId = new Guid("00000000-0000-0000-0000-000000000000");

            try
            {
                if (user.UserID.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    // Create new User
                    
                    MembershipUser membershipUser = Membership.CreateUser(user.UserName, user.Password, user.Email);
                    membershipUser.IsApproved = user.IsApproved;
                    Membership.UpdateUser(membershipUser);

                    ProfileBase pb = ProfileBase.Create(membershipUser.UserName);
                    pb["FirstName"] = user.FirstName;
                    pb["LastName"] = user.LastName;
                    pb["PhoneNumber"] = user.PhoneNumber;
                    pb["Extension"] = user.Extension;
                    pb["Address"] = user.Address;
                    pb["City"] = user.City;
                    pb["State"] = user.State;
                    pb["ZipCode"] = user.ZipCode;
                    pb.Save();

                  
                    memberId = (Guid)membershipUser.ProviderUserKey;
                }
                else
                {
                    MembershipUser membershipUser = Membership.GetUser(user.UserID);
                    memberId = user.UserID;

                    if (membershipUser != null)
                    {
                        membershipUser.UnlockUser();
                        membershipUser.Email = user.Email;
                        membershipUser.IsApproved = user.IsApproved;
                        Membership.UpdateUser(membershipUser);

                        if (!string.IsNullOrEmpty(user.Password))
                            membershipUser.ChangePassword(membershipUser.ResetPassword(), user.Password);

                        // Assign Profile
                        ProfileBase pb = ProfileBase.Create(user.UserName);
                        pb["FirstName"] = user.FirstName;
                        pb["LastName"] = user.LastName;
                        pb["PhoneNumber"] = user.PhoneNumber;
                        pb["Extension"] = user.Extension;
                        pb["Address"] = user.Address;
                        pb["City"] = user.City;
                        pb["State"] = user.State;
                        pb["ZipCode"] = user.ZipCode;
                        pb.Save();

                    }
                }
            }
            catch
            {
                retVal = false;
            }


            return retVal;
        }

        public static bool SaveUserProfile(string userName, string firstName, string lastName, string phoneNumber, string extension, string address, string city, string state, string zipCode)
        {
            bool retVal = true;

            try
            {
                ProfileBase pb = ProfileBase.Create(userName);
                pb["FirstName"] = firstName;
                pb["LastName"] = lastName;
                pb["PhoneNumber"] = phoneNumber;
                pb["Extension"] = extension;
                pb["Address"] = address;
                pb["City"] = city;
                pb["State"] = state;
                pb["ZipCode"] = zipCode;
                pb.Save();
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static bool AssignUserRole(string userName, string role)
        {
            bool retVal = true;

            try
            {
                if (!Roles.IsUserInRole(userName, role))
                    Roles.AddUserToRole(userName, role);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static User GetUserById(Guid userId)
        {
            User user = new User();

            MembershipUser membershipUser = Membership.GetUser(userId);

            if (membershipUser != null)
            {
                user.UserID = userId;
                user.UserName = membershipUser.UserName;
                user.Email = membershipUser.Email;
                user.IsApproved = membershipUser.IsApproved;

                ProfileBase pb = ProfileBase.Create(user.UserName);

                if (pb["FirstName"] != null)
                    user.FirstName = pb["FirstName"].ToString();

                if (pb["LastName"] != null)
                    user.LastName = pb["LastName"].ToString();

                if (pb["PhoneNumber"] != null)
                    user.PhoneNumber = pb["PhoneNumber"].ToString();

                if (pb["Extension"] != null)
                    user.Extension = pb["Extension"].ToString();

                if (pb["Address"] != null)
                    user.Address = pb["Address"].ToString();

                if (pb["State"] != null)
                    user.State = pb["State"].ToString();

                if (pb["City"] != null)
                    user.City = pb["City"].ToString();

                if (pb["ZipCode"] != null)
                    user.ZipCode = pb["ZipCode"].ToString();
            }


            return user;

        }

        public static User GetUserByUserName(string userName)
        {
            User user = new User();

            MembershipUser membershipUser = Membership.GetUser(userName);

            if (membershipUser != null)
            {
                user.UserID = (Guid) membershipUser.ProviderUserKey;
                user.UserName = membershipUser.UserName;
                user.Email = membershipUser.Email;
                user.IsApproved = membershipUser.IsApproved;

                ProfileBase pb = ProfileBase.Create(user.UserName);

                if (pb["FirstName"] != null)
                    user.FirstName = pb["FirstName"].ToString();

                if (pb["LastName"] != null)
                    user.LastName = pb["LastName"].ToString();

                if (pb["PhoneNumber"] != null)
                    user.PhoneNumber = pb["PhoneNumber"].ToString();

                if (pb["Extension"] != null)
                    user.Extension = pb["Extension"].ToString();

                if (pb["Address"] != null)
                    user.Address = pb["Address"].ToString();

                if (pb["State"] != null)
                    user.State = pb["State"].ToString();

                if (pb["City"] != null)
                    user.City = pb["City"].ToString();

                if (pb["ZipCode"] != null)
                    user.ZipCode = pb["ZipCode"].ToString();
            }


            return user;

        }

        public static User[] GetAllUsers()
        {
            ArrayList userList = new ArrayList();

            MembershipUserCollection membershipUserCollection = Membership.GetAllUsers();

            if (membershipUserCollection != null)
            {
                foreach (MembershipUser membershipUser in membershipUserCollection)
                {
                    User user = new User();

                    user.UserID = (Guid)membershipUser.ProviderUserKey;
                    user.UserName = membershipUser.UserName;
                    user.Email = membershipUser.Email;
                    user.IsApproved = membershipUser.IsApproved;

                    ProfileBase pb = ProfileBase.Create(user.UserName);

                    if (pb["FirstName"] != null)
                        user.FirstName = pb["FirstName"].ToString();

                    if (pb["LastName"] != null)
                        user.LastName = pb["LastName"].ToString();

                    userList.Add(user);
                }
            }

            return (User[])userList.ToArray(typeof(User));
        }

        #endregion
    }
}
