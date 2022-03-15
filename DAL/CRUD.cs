using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.Linq;
//using System.Collections.ObjectModel;

namespace DAL
{
    public class CRUD
    {
        public static List<string> FetchProducts() 
        {
            //Collection<string> col = new Collection<string>();
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                return (from t in db.Products select t.Products ).ToList();
                //foreach (var item in _product)
                //{
                //    col.Add(item);
                //}
                //return _product;
            }
            
        }

        public static int FetchPrice(string st) 
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                var dt = db.Products.SingleOrDefault(dat => dat.Products == st);
                var _price = dt.Prices;
                return Convert.ToInt32(_price);
            }
        }

        public static string FetchDetail(string st) 
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                var dt = db.Products.SingleOrDefault(dat => dat.Products == st);
                var _detail = dt.Details;
                return _detail;
            }
        }

        public static void InsertOrder(string _user, int _noProduct, int _totalPrice, DateTime _time) 
        {
            var _userId = FetchUserId(_user);
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                Order newOrder = new Order();
                newOrder.UserId = _userId;
                newOrder.UserName = _user;
                newOrder.No_of_products = _noProduct;
                newOrder.Total_price = _totalPrice;
                newOrder.Time = _time;
                db.Orders.InsertOnSubmit(newOrder);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static void InsertOrderedProducts(DateTime time, string[] pList, int[] qList) 
        {
            int _orderId = FetchOrderId(time);
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                for (int i = 0; i < pList.Length; i++)
                {
                    OrderedProduct _products = new OrderedProduct();
                    _products.OrderId = _orderId;
                    _products.Products = pList[i];
                    _products.Quantity = qList[i];
                    db.OrderedProducts.InsertOnSubmit(_products);
                    db.SubmitChanges();
                }
            }
        }

        public static Guid FetchUserId(string aUser)
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext())
            {
                try
                {
                    var dt = db.aspnet_Users.SingleOrDefault(dat => dat.UserName == aUser);
                    var _userId = dt.UserId;
                    return _userId;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static int FetchOrderId(DateTime time)
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext())
            {
                try
                {
                    var dt = db.Orders.SingleOrDefault(dat => dat.Time == time);
                    var _orderId = dt.OrderId;
                    return _orderId;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static void TransferOrderToFailedOrder(int orderId) 
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                var myOrder = db.Orders.SingleOrDefault(dat => dat.OrderId == orderId);
                //var dt = from item in db.Orders where item.OrderId == orderId select item;

                FailedOrder fo = new FailedOrder();
                fo.FailedOrderId = orderId;
                fo.UserId = myOrder.UserId;
                fo.UserName = myOrder.UserName;
                fo.No_of_products = myOrder.No_of_products;
                fo.Total_price = myOrder.Total_price;
                fo.Time = myOrder.Time;

                var myOrderProducts = from row in db.OrderedProducts where row.OrderId == orderId select row;
                //var myOrderProducts = db.OrderedProducts.SingleOrDefault(dat => dat.OrderId == orderId);
                foreach (var row in myOrderProducts)
                {
                    row.FailedOrderId = orderId;
                    row.OrderId = null;
                }

                db.FailedOrders.InsertOnSubmit(fo);
                db.Orders.DeleteOnSubmit(myOrder);
                db.SubmitChanges();
            }
        }

        public static void TransferOrderToCompletedOrder(int orderId) 
        {
            using (linqOrderingDataContext db = new linqOrderingDataContext()) 
            {
                var myOrder = db.Orders.SingleOrDefault(dat => dat.OrderId == orderId);

                CompletedOrder co = new CompletedOrder();
                co.CompletedOrderId = orderId;
                co.UserId = myOrder.UserId;
                co.UserName = myOrder.UserName;
                co.No_of_products = myOrder.No_of_products;
                co.Total_price = myOrder.Total_price;
                co.Time = myOrder.Time;


                var myOrderProducts = from row in db.OrderedProducts where row.OrderId == orderId select row;
                foreach (var row in myOrderProducts)
                {
                    row.CompletedOrderId = orderId;
                    row.OrderId = null;
                }

                db.CompletedOrders.InsertOnSubmit(co);
                db.Orders.DeleteOnSubmit(myOrder);
                db.SubmitChanges();
            }
        }
        
    }
}
