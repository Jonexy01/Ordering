<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Ordering.Admin.AdminPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Welcome admin</h1>
        <asp:LinkButton ID="UpdateLinkButton" runat="server" 
            onclick="UpdateLinkButton_Click">Update products</asp:LinkButton>
            <br />
        <asp:Panel ID="UpdatePanel" runat="server" Visible="False">
            <asp:GridView ID="ProductGridView" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="ProductsId" 
                DataSourceID="SqlDataSource1" 
                EmptyDataText="There are no data records to display." 
                AllowPaging="True" Visible="True" EnableViewState="False">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:BoundField DataField="Products" HeaderText="Products" 
                    SortExpression="Products" ReadOnly="True" />
                    <asp:BoundField DataField="Prices" HeaderText="Prices" 
                    SortExpression="Prices" />
                    <asp:BoundField DataField="Details" HeaderText="Details" 
                    SortExpression="Details" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView><br />
            <asp:Button ID="AddButton" runat="server" Text="Add product" 
                onclick="AddButton_Click" />
            <asp:Label ID="AddResponseLabel" runat="server" EnableViewState="False"></asp:Label><br />
            <asp:DetailsView ID="AddDetailsView" runat="server" Height="50px" Width="125px" 
                AutoGenerateRows="False" DataSourceID="SqlDataSource1" DefaultMode="Insert" 
                DataKeyNames="ProductsId" Visible="False" 
                OnItemInserted="DetailView_ItemInsert" OnItemCommand="DetailView_ItemCommand" 
                EnableViewState="False">
                <Fields>
                    <asp:BoundField DataField="Products" HeaderText="Product" SortExpression="Products"/>
                    <asp:BoundField DataField="Prices" HeaderText="Price" SortExpression="Prices"/>
                    <asp:BoundField DataField="Details" HeaderText="Details" SortExpression="Details"/>
                    <asp:CommandField ShowInsertButton="True" />
                </Fields>
            </asp:DetailsView>
        </asp:Panel>
        <br /><br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:Cstring %>" 
                DeleteCommand="DELETE FROM [Product] WHERE [ProductsId] = @ProductsId" 
                InsertCommand="INSERT INTO [Product] ([Products], [Prices], [Details]) VALUES (@Products, @Prices, @Details)" 
                ProviderName="<%$ ConnectionStrings:Cstring.ProviderName %>" 
                SelectCommand="SELECT [ProductsId], [Products], [Prices], [Details] FROM [Product] ORDER BY [Products]" 
                UpdateCommand="UPDATE [Product] SET [Prices] = @Prices, [Details] = @Details WHERE [ProductsId] = @ProductsId">
                <DeleteParameters>
                    <asp:Parameter Name="ProductsId" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Products" Type="String" />
                    <asp:Parameter Name="Prices" Type="Int32" />
                    <asp:Parameter Name="Details" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Prices" Type="Int32" />
                    <asp:Parameter Name="Details" Type="String" />
                    <asp:Parameter Name="ProductsId" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

        <asp:LinkButton ID="SortLinkButton" runat="server" 
            onclick="SortLinkButton_Click">Sort Order</asp:LinkButton>
            <br />
        <asp:GridView ID="OrderGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderId"
            DataSourceID="SqlDataSource3" EmptyDataText="There are no data records to display." Visible="False">
            <Columns>
                <%--<asp:BoundField DataField="OrderId" HeaderText="OrderId" ReadOnly="True" SortExpression="OrderId" Visible="false" />--%>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="OrderIdLabel" runat="server" Text='<%#Eval("OrderId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />--%>
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="No_of_products" HeaderText="No of products" SortExpression="No_of_products" />
                <asp:BoundField DataField="Total_price" HeaderText="Total price" SortExpression="Total_price" />
                <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                <asp:TemplateField HeaderText="Sort order">
                    <ItemTemplate>
                        <asp:DropDownList ID="SortDropDownList" runat="server">
                            <asp:ListItem Selected="True" Text="Sort"></asp:ListItem>
                            <asp:ListItem Text="Completed"></asp:ListItem>
                            <asp:ListItem Text="Failed"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView><br />
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" Visible="False" 
            onclick="SubmitButton_Click" />
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Cstring %>"
            DeleteCommand="DELETE FROM [Order] WHERE [OrderId] = @OrderId" InsertCommand="INSERT INTO [Order] ([UserId], [UserName], [No of products], [Total price], [Time]) VALUES (@UserId, @UserName, @No_of_products, @Total_price, @Time)"
            ProviderName="<%$ ConnectionStrings:Cstring.ProviderName %>" SelectCommand="SELECT [OrderId], [UserId], [UserName], [No of products] AS No_of_products, [Total price] AS Total_price, [Time] FROM [Order]"
            UpdateCommand="UPDATE [Order] SET [UserId] = @UserId, [UserName] = @UserName, [No of products] = @No_of_products, [Total price] = @Total_price, [Time] = @Time WHERE [OrderId] = @OrderId">
            <DeleteParameters>
                <asp:Parameter Name="OrderId" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="UserId" Type="Object" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="No_of_products" Type="Int32" />
                <asp:Parameter Name="Total_price" Type="Int32" />
                <asp:Parameter Name="Time" Type="DateTime" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserId" Type="Object" />
                <asp:Parameter Name="UserName" Type="String" />
                <asp:Parameter Name="No_of_products" Type="Int32" />
                <asp:Parameter Name="Total_price" Type="Int32" />
                <asp:Parameter Name="Time" Type="DateTime" />
                <asp:Parameter Name="OrderId" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br /><br /><br /><br />
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Order/Start.aspx">Order page</asp:LinkButton>
        </div>
        </form>
    </body>
    </html>
