using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;

namespace Ordering
{
    public partial class Start : System.Web.UI.Page
    {
        int aSum;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                control.PopulateDDL(ProductDropDownList);

                ViewState["CartTable"] = control.CreateCartTable();
                //CartGridView.DataSource = control.CreateCartTable();
                //CartGridView.DataBind();
                ViewState["IndexNo"] = 1;
            }
        }

        protected void ProductDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProductDropDownList.Items[0].Selected)
            {
                control.DisplayInfo(ProductDropDownList, PriceLabel, DetailLabel);
            }
            else
            {
                PriceLabel.Text = "";
                DetailLabel.Text = "";
            }
        }

        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            if (!ProductDropDownList.Items[0].Selected)
            {
                DataTable dt = (DataTable)ViewState["CartTable"];
                
                if (control.RowExist(dt, ProductDropDownList.Text))
                {
                    int initQuantity = (int)control.GetItemByProductName(dt, ProductDropDownList.Text, 3);
                    int quantity = initQuantity + 1;
                    DataRow[] daRow = dt.Select("Products = '" + ProductDropDownList.Text + "'");
                    daRow[0][3] = quantity;
                }
                else
                {
                    DataRow aRow = dt.NewRow();
                    int indexNo = (int)ViewState["IndexNo"];
                    aRow["No"] = indexNo++;
                    ViewState["IndexNo"] = indexNo;
                    aRow["Products"] = ProductDropDownList.Text;
                    aRow["Prices"] = PriceLabel.Text;
                    aRow["Quantity"] = 1;
                    ButtonField bf = new ButtonField();
                    bf.ButtonType = ButtonType.Button;
                    bf.HeaderText = "remove from cart";
                    bf.CommandName = "Del";
                    bf.Visible = true;
                    bf.CausesValidation = true;
                    aRow["Delete"] = bf;
                    dt.Rows.Add(aRow);
                }
                
                
                //ViewState["CartTable"] = dt;
                CartGridView.DataSource = ViewState["CartTable"];
                CartGridView.DataBind();

                aSum = control.SumColumnValues(dt, 2, 3);
                TotalPriceLabel.Text = aSum.ToString();
            }
        }

        protected void CartGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "Del")
            {
                int myIndex = Int32.Parse(e.CommandArgument.ToString());
                DataTable dtNew = (DataTable)ViewState["CartTable"];
                int rowNo = dtNew.Rows.Count;
                rowNo = rowNo - 1;
                dtNew.Rows.RemoveAt(myIndex);
                for (int i = myIndex; i < rowNo; i++)
                {
                    int j = i + 2;
                    DataRow[] daRow = dtNew.Select("No = '" + j + "'");
                    daRow[0][0] = j - 1;
                }
                int _no = (int)ViewState["IndexNo"];
                ViewState["IndexNo"] = _no - 1;
                CartGridView.DataSource = ViewState["CartTable"];
                CartGridView.DataBind();

                aSum = control.SumColumnValues(dtNew, 2, 3);
                TotalPriceLabel.Text = aSum.ToString();
            }
        }

        protected void OrderButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CartTable"];
            int d = dt.Rows.Count;
            if (d > 0)
            {
                control.SubmitOrderToDB(dt);
                //control.SendEmail(dt);
            }
            HttpContext.Current.Response.Redirect("~/Order/OrderSuccess.aspx");
        }

        protected void CartGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) 
        {
            e.Cancel = true;
            CartGridView.EditIndex = -1;
            CartGridView.DataSource = ViewState["CartTable"];
            CartGridView.DataBind();
        }

        protected void CartGridView_RowEditing(object sender, GridViewEditEventArgs e) 
        {
            CartGridView.EditIndex = e.NewEditIndex;
            CartGridView.DataSource = ViewState["CartTable"];
            CartGridView.DataBind();
        }

        protected void CartGridView_RowUpdating(object sender, GridViewUpdateEventArgs e) 
        {
            GridViewRow row = CartGridView.Rows[e.RowIndex];
            Label NoLabel = (Label)row.FindControl("NoLabel");
            TextBox QuantityTextBox = (TextBox)row.FindControl("QuantityTextBox");
            int quantity = Int32.Parse(QuantityTextBox.Text);
            DataTable dt = (DataTable)ViewState["CartTable"];
            control.UpdateCartTable(dt, quantity, int.Parse(NoLabel.Text));
            CartGridView.EditIndex = -1;
            CartGridView.DataSource = ViewState["CartTable"];
            CartGridView.DataBind();

            aSum = control.SumColumnValues(dt, 2, 3);
            TotalPriceLabel.Text = aSum.ToString();
        }
    }
}