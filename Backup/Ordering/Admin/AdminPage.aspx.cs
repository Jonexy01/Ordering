using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace Ordering.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UpdateLinkButton_Click(object sender, EventArgs e)
        {
            if (UpdatePanel.Visible == false)
            {
                UpdatePanel.Visible = true;
            }
            else
            {
                UpdatePanel.Visible = false;
            }
        }

        protected void SortLinkButton_Click(object sender, EventArgs e)
        {
            if (OrderGridView.Visible == false)
            {
                OrderGridView.Visible = true;
                SubmitButton.Visible = true;
            }
            else
            {
                OrderGridView.Visible = false;
                SubmitButton.Visible = false;
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            AddDetailsView.Visible = true;
            AddButton.Visible = false;
        }

        protected void DetailView_ItemInsert(object sender, DetailsViewInsertedEventArgs e) 
        {
            if (e.AffectedRows == 1)
            {
                AddResponseLabel.Text = "Inserted successfully";
                AddButton.Visible = true;
            }
        }

        protected void DetailView_ItemInserting(object sender, DetailsViewInsertEventArgs e) 
        {
            //if (AddDetailsView.CurrentMode == DetailsViewMode.Insert)
            //{
            //    //TextBox tb = (TextBox)AddDetailsView.Rows[0].Cells[1].Controls[0];
            //    TextBox tb = (TextBox)AddDetailsView.FindControl("ProductTextBox");
            //    if (tb.Text == "")
            //    {
            //        HttpContext.Current.Response.Redirect("~Account/Index");
            //    }
            //}
        }

        protected void DetailView_ItemCommand(object sender, DetailsViewCommandEventArgs e) 
        {
            if (e.CommandName == "Cancel")
            {
                AddResponseLabel.Text = "";
                AddButton.Visible = true;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < OrderGridView.Rows.Count; i++)
            {
                //string any = OrderGridView.Rows[i].Cells[0].Text;
                Label OrderIdLabel = (Label)OrderGridView.Rows[i].FindControl("OrderIdLabel");
                string any = OrderIdLabel.Text;
                int _orderId = int.Parse( any);
                DropDownList ddl = (DropDownList)OrderGridView.Rows[i].FindControl("SortDropDownList");
                if (ddl.Text == "Completed")
                {
                    CRUD.TransferOrderToCompletedOrder(_orderId);
                }
                else if (ddl.Text == "Failed")
                {
                    CRUD.TransferOrderToFailedOrder(_orderId);
                }
            }
            OrderGridView.DataBind();
        }
    }
}