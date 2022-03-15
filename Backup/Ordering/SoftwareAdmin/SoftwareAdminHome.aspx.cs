using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Ordering.SoftwareAdmin
{
    public partial class SoftwareAdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                controlMembership.PopulateUsersDDL(SelectUserDropDownList);
                controlRoleManager.PopulateRolesDDL(ChooseRoleDropDownList);
                controlRoleManager.PopulateRolesDDL(DRoleDropDownList);
                controlRoleManager.PopulateRolesDDL(RChooseRoleDropDownList);
            }
        }

        protected void CreatRoleLinkButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.LinkButtonAction(CreateRolePanel);
        }

        protected void CreateRoleButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.CreateRole(CRoleNameTextBox.Text, ResponseCRLabel);
        }

        protected void DeleteRoleLinkButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.LinkButtonAction(DeleteRolePanel);
        }

        protected void DeleteRoleButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.DeleteRole(DRoleDropDownList.Text, ResponseDRLabel);
        }

        protected void AddUToRoleLinkButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.LinkButtonAction(AddUToRolePanel);
        }

        protected void AddUToRoleButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.AddUToRole(SelectUserDropDownList.Text, ChooseRoleDropDownList.Text, ResponseAULabel);
        }

        protected void RemoveUFromRoleLinkButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.LinkButtonAction(RemoveUFromRolePanel);
            RSelectUserDropDownList.Visible = false;
            RChooseRoleDropDownList.SelectedIndex = 0;
        }

        protected void RemoveUFromRoleButton_Click(object sender, EventArgs e)
        {
            BLL.controlRoleManager.RemoveUFromRole(RSelectUserDropDownList.Text, RChooseRoleDropDownList.Text, ResponseRULabel);
            RSelectUserDropDownList.Visible = false;
            RChooseRoleDropDownList.SelectedIndex = 0;
        }

        protected void RChooseRoleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RChooseRoleDropDownList.Text == "Select role")
            {
                RSelectUserDropDownList.Visible = false;
            }
            else
            {
                RSelectUserDropDownList.Visible = true;
                controlRoleManager.GetUsersInRole(RChooseRoleDropDownList.Text, RSelectUserDropDownList);
            }
        }
    }
}