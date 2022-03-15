using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BLL
{
    public class controlRoleManager
    {
        public static void CreateRole(string _role, Label lb)
        {
            Roles.CreateRole(_role);
            lb.Text = "'" + _role + "'" + " role successfully created.";
        }

        public static void DeleteRole(string _role, Label lb)
        {
            Roles.DeleteRole(_role);
            lb.Text = "'" + _role + "'" + " role successfully deleted.";
        }

        public static void AddUToRole(string _userName, string _roleName, Label lb)
        {
            Roles.AddUserToRole(_userName, _roleName);
            lb.Text = "User added to role successfully";
        }

        public static void RemoveUFromRole(string _userName, string _roleName, Label lb)
        {
            Roles.RemoveUserFromRole(_userName, _roleName);
            lb.Text = "User removed from role successfully";
        }

        public static void LinkButtonAction(Panel pn)
        {
            if (pn.Visible == true)
            {
                pn.Visible = false;
            }
            else
            {
                pn.Visible = true;
            }
        }

        public static void PopulateRolesDDL(DropDownList ddl) 
        {
            string[] _roles = Roles.GetAllRoles();
            ddl.DataSource = _roles;
            ddl.DataBind();
        }

        public static void GetUsersInRole(string _role, DropDownList ddl) 
        {
            string[] someUsers = Roles.GetUsersInRole(_role);
            ddl.DataSource = someUsers;
            ddl.DataBind();
        }
    }
}
