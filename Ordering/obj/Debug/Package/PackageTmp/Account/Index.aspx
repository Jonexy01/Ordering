<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Ordering.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Please Login</h1>
        UserName: <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox><br />
        Password: <asp:TextBox ID="PasswordTextBox" runat="server" 
            style="margin-left: 8px"></asp:TextBox><br />
        <asp:Button ID="LoginButton" runat="server" Text="Login" 
            onclick="LoginButton_Click" />
        <asp:Label ID="ResponseLabel" runat="server" Text=""></asp:Label><br />
        <br />
        If you are not a member yet 
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Account/RegisterationPage.aspx">click to register</asp:LinkButton>
    </div>
    </form>
</body>
</html>
