<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="Ordering.Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        welcome 
        <asp:LoginName ID="LoginName1" runat="server" />
        <h1>Make your order</h1>
        Product: 
        <asp:DropDownList ID="ProductDropDownList" runat="server" 
            onselectedindexchanged="ProductDropDownList_SelectedIndexChanged" 
            AppendDataBoundItems="True" AutoPostBack="true">
            <asp:ListItem Selected="True" Text="Select product"></asp:ListItem>
        </asp:DropDownList><br />
        Price: 
        <asp:Label ID="PriceLabel" runat="server" Text=""></asp:Label><br />
        Detail: 
        <asp:Label ID="DetailLabel" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Button ID="AddToCartButton" runat="server" Text="Add to cart" 
            onclick="AddToCartButton_Click" /><br />
        <br />
        <asp:GridView ID="CartGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="CartGridView_RowCommand" 
            OnRowCancelingEdit="CartGridView_RowCancelingEdit" OnRowEditing="CartGridView_RowEditing" OnRowUpdating="CartGridView_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <asp:Label ID="NoLabel" runat="server" Text='<%#Eval("No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Products">
                    <ItemTemplate>
                        <asp:Label ID="ProductLabel" runat="server" Text='<%#Eval("Products") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="priceLabel" runat="server" Text='<%#Eval("Prices") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <%#Eval("Quantity") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="QuantityTextBox" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowEditButton="true" ShowCancelButton="true" HeaderText="Edit quantity"/>
                <asp:ButtonField CommandName="Del" ButtonType="Button" DataTextField="Delete" HeaderText="Delete item" CausesValidation="True" />
            </Columns>
        </asp:GridView><br />
        Total price: 
        <asp:Label ID="TotalPriceLabel" runat="server" Text=""></asp:Label><br />
        <asp:Button ID="OrderButton" runat="server" Text="Order" 
            onclick="OrderButton_Click" />
    </div>
    </form>
</body>
</html>
