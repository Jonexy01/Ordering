<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoftwareAdminHome.aspx.cs" Inherits="Ordering.SoftwareAdmin.SoftwareAdminHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Software administration</h1>

        <asp:LinkButton ID="CreatRoleLinkButton" runat="server"  
            onclick="CreatRoleLinkButton_Click">Create role</asp:LinkButton><br />
        <asp:Panel ID="CreateRolePanel" runat="server" Visible="false">
            Role name: <asp:TextBox ID="CRoleNameTextBox" runat="server"></asp:TextBox><br />
            <asp:Button ID="CreateRoleButton" runat="server" Text="Create role" 
                onclick="CreateRoleButton_Click" /><asp:Label ID="ResponseCRLabel" runat="server"
                Text=""></asp:Label>
        </asp:Panel>

        <asp:LinkButton ID="DeleteRoleLinkButton" runat="server" 
            onclick="DeleteRoleLinkButton_Click">Delete role</asp:LinkButton><br />
        <asp:Panel ID="DeleteRolePanel" runat="server" Visible="false">
            Role name: 
            <asp:DropDownList ID="DRoleDropDownList" runat="server">
            </asp:DropDownList><br />
            <asp:Button ID="DeleteRoleButton" runat="server" Text="Delete role" 
                onclick="DeleteRoleButton_Click" /><asp:Label ID="ResponseDRLabel" runat="server"
                Text=""></asp:Label>
        </asp:Panel>

        <asp:LinkButton ID="AddUToRoleLinkButton" runat="server" 
            onclick="AddUToRoleLinkButton_Click">Add users to role</asp:LinkButton><br />
        <asp:Panel ID="AddUToRolePanel" runat="server" Visible="false">
            Select user: 
            <asp:DropDownList ID="SelectUserDropDownList" runat="server">
            </asp:DropDownList><br />
            Choose role: 
            <asp:DropDownList ID="ChooseRoleDropDownList" runat="server">
            </asp:DropDownList><br />
            <asp:Button ID="AddUToRoleButton" runat="server" Text="Add" 
                onclick="AddUToRoleButton_Click" /><asp:Label ID="ResponseAULabel" runat="server"
                Text=""></asp:Label>
        </asp:Panel>
        
        <asp:LinkButton ID="RemoveUFromRoleLinkButton" runat="server" 
            onclick="RemoveUFromRoleLinkButton_Click">Remove user from role</asp:LinkButton><br />
        <asp:Panel ID="RemoveUFromRolePanel" runat="server" Visible="false">
            Choose role: 
            <asp:DropDownList ID="RChooseRoleDropDownList" runat="server" 
                AppendDataBoundItems="True" AutoPostBack="true"
                onselectedindexchanged="RChooseRoleDropDownList_SelectedIndexChanged">
            <asp:ListItem Text="Select role" Selected="True"></asp:ListItem>
            </asp:DropDownList><br />
            Select user: 
            <asp:DropDownList ID="RSelectUserDropDownList" runat="server" Visible="False">
            </asp:DropDownList><br />
            <asp:Button ID="RemoveUFromRoleButton" runat="server" Text="Remove" 
                onclick="RemoveUFromRoleButton_Click" /><asp:Label ID="ResponseRULabel" runat="server"
                Text=""></asp:Label>
        </asp:Panel>
        <br /><br />
        
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Order/Start.aspx">Order page</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Admin/AdminPage.aspx">Admin page</asp:LinkButton><br />
        
    </div>
    </form>
</body>
</html>
