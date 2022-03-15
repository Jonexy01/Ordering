<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSuccess.aspx.cs" Inherits="Ordering.Order.OrderSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Your order was successful</h1>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Order/Start.aspx">Make another order</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton2" runat="server">Log out</asp:LinkButton>
    </div>
    </form>
</body>
</html>
