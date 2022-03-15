using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Web.UI.WebControls;
using DAL;

namespace BLL
{
    public class controlMembership
    {
        /// <summary>
        /// Attempts to create a user and log the user in. Returns -1 for failure; 2 for success but not logged in; 1 for successfull creation and login.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_password"></param>
        /// <param name="_email"></param>
        /// <param name="_question"></param>
        /// <param name="_answer"></param>
        /// <param name="_response"></param>
        /// <returns></returns>
        public static int RegisterUser(string _userName, string _password, string _email, string _question, string _answer, Label _response)
        {
            MembershipCreateStatus reg;
            MembershipUser user = Membership.CreateUser(_userName, _password, _email, _question, _answer, true, out reg);
            if (reg == MembershipCreateStatus.Success)
            {
                FormsAuthentication.SetAuthCookie(_userName, true);
                HttpContext.Current.Response.Redirect("~/Order/Start.aspx");
                return 1;
                //FormsAuthentication.RedirectFromLoginPage(NameTextBox.Text, true);
            }
            else if (reg == MembershipCreateStatus.DuplicateUserName)
            {
                _response.Text = "Username already exist";
                return -1;
            }
            else if (reg == MembershipCreateStatus.DuplicateEmail)
            {
                _response.Text = "Email already exist";
                return -1;
            }
            else if (reg == MembershipCreateStatus.InvalidPassword)
            {
                _response.Text = "Please enter a valid password";
                return -1;
            }
            else if (reg == MembershipCreateStatus.InvalidEmail)
            {
                _response.Text = "Please enter a valid email";
                return -1;
            }
            else
            {
                _response.Text = "Something unexpected happened";
                return -1;
            }
        }

        public static void LoginUser(string _userName, string _password, Label _response)
        {
            if (Membership.ValidateUser(_userName, _password))
            {
                FormsAuthentication.SetAuthCookie(_userName, true);
                if (Roles.IsUserInRole(_userName, "Admin"))
                {
                    HttpContext.Current.Response.Redirect("~/Admin/AdminPage.aspx");
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/Order/Start.aspx");
                }
            }
            else
            {
                _response.Text = "Incorrect username or password";
            }
        }

        public static void LogoutUser()
        {
            FormsAuthentication.SignOut();
        }

        public static void PopulateUsersDDL(DropDownList ddl) 
        {
            var userList = Membership.GetAllUsers();
            ddl.DataSource = userList;
            ddl.DataBind();
        }

    }
}
