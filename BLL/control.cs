using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Web.Security;
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace BLL
{
    public class control
    {
        public static void PopulateDDL(DropDownList ddl) 
        {
            //List<string> _products = CRUD.FetchProducts();
            ddl.DataSource = CRUD.FetchProducts();
            //ddl.DataTextField = "Products";
            //ddl.DataValueField = "Products";
            ddl.DataBind();
        }

        public static void DisplayInfo(DropDownList ddl, Label lab1, Label lab2) 
        {
            int price = CRUD.FetchPrice(ddl.Text);
            lab1.Text = Convert.ToString(price);
            string detail = CRUD.FetchDetail(ddl.Text);
            lab2.Text = detail;
        }

        public static DataTable CreateCartTable() 
        {
            DataTable myCartTable = new DataTable();

            DataColumn No = new DataColumn("No", Type.GetType("System.Int32"));
            myCartTable.Columns.Add(No);
            DataColumn Products = new DataColumn("Products", Type.GetType("System.String"));
            myCartTable.Columns.Add(Products);
            DataColumn Price = new DataColumn("Prices", Type.GetType("System.Int32"));
            myCartTable.Columns.Add(Price);
            DataColumn Quantity = new DataColumn("Quantity", Type.GetType("System.Int32"));
            myCartTable.Columns.Add(Quantity);
            ButtonField bf = new ButtonField();
            DataColumn Delete = new DataColumn("Delete", bf.GetType());
            myCartTable.Columns.Add(Delete);
            DataColumn[] keys = new DataColumn[2];
            keys[0] = No;
            keys[1] = Products;
            myCartTable.PrimaryKey = keys;
            
            return myCartTable;
        }

        public static int SumColumnValues(DataTable table, int priceColIndex, int quantityColIndex) 
        {
            DataRow[] productList = table.Select();
            int aSum = 0;
            for (int i = 0; i < productList.Length; i++)
            {
                int something = (int)productList[i][priceColIndex] * (int)productList[i][quantityColIndex];
                aSum = aSum + something;
            }
            return aSum;
        }

        public static void SubmitOrderToDB(DataTable dt)
        {
            MembershipUser user = Membership.GetUser();
            string _userName = user.UserName;

            DataRow[] productList = dt.Select();
            int aSum = 0;
            for (int i = 0; i < productList.Length; i++)
            {
                int something = (int)productList[i][3];
                aSum = aSum + something;
            }

            int totalPrice = SumColumnValues(dt, 2, 3);

            DateTime _time = DateTime.Now;

            string[] orderedProducts = FetchColumnContents(dt, 1);

            CRUD.InsertOrder(_userName, aSum, totalPrice, _time);

            DataRow[] aRowList = dt.Select();
            int[] aList = new int[aRowList.Length];
            int j = 0;
            for (int i = 0; i < aRowList.Length; i++)
            {
                int _item = (int)aRowList[i][3];

                aList[j] = _item;
                j++;
            }

            CRUD.InsertOrderedProducts(_time, orderedProducts, aList);
        }

        public static string[] FetchColumnContents(DataTable dt, int colIndex) 
        {
            DataRow[] aRowList = dt.Select();
            string[] aList = new string[aRowList.Length];
            int j = 0;
            for (int i = 0; i < aRowList.Length; i++)
            {
                string _item = aRowList[i][colIndex].ToString();
                
                aList[j] = _item;
                j++;
            }
            return aList;
        }

        public static object GetItemByProductName(DataTable dt, string _name, int itemColIndex) 
        {
            string[] _products = FetchColumnContents(dt, 1);
            int myIndex = GetListItemIndex(_name, _products);
            var _item = Convert.ToInt32(FetchColumnContents(dt, itemColIndex)[myIndex]);
            return _item;
        }

        public static bool RowExist(DataTable dt, string _product) 
        {
            string[] theList = FetchColumnContents(dt, 1);
            bool test = IsItemInList(_product, theList);
            return test;
        }

        public static bool IsItemInList(string item, string[] list) 
        {
            bool check = false;
            for (int i = 0; i < list.Length; i++)
            {
                string listItem = list[i];
                if (item == listItem)
                {
                    check = true;
                    return check;
                }
            }
            return check;
        }

        public static int GetListItemIndex(string item, string[] list) 
        {
            int itemIndex = -1;
            for (int i = 0; i < list.Length; i++)
            {
                string listItem = list[i];
                if (item == listItem)
                {
                    itemIndex = i;
                }
            }
            return itemIndex;
        }

        public static void UpdateCartTable(DataTable dt, int _quantity, int indexNo) 
        {
            DataRow[] daRow = dt.Select("No = '"+indexNo+"'");
            daRow[0][3] = _quantity;
        }

        public static void SendEmail(DataTable dt) 
        {
            MembershipUser user = Membership.GetUser();
            string _user = user.UserName;
            DateTime time = DateTime.Now;

            string[] _quantities = FetchColumnContents(dt, 3);
            string[] _products = FetchColumnContents(dt, 1);
            string _order = "";
            for (int i = 0; i < _products.Length; i++)
            {
                _order += _quantities[i] + " " + _products[i] + "(s)" + "<br/>";
            }

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient server = new SmtpClient("smtp.yourmail.com");

                mail.From = new MailAddress("youremailaddress@yourmail.com");
                mail.To.Add("admin_address");
                mail.Subject = "Request from " + _user;
                mail.Body = _user + " made a request at " + time.ToString() + ", ordering" + "<br /><br/>" + _order + "<br />" + "Total price is: " + SumColumnValues(dt, 2, 3).ToString();

                server.Port = 25;
                server.Credentials = new System.Net.NetworkCredential("username", "password", "domain");
                server.EnableSsl = true;

                server.Send(mail);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
