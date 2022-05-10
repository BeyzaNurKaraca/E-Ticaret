using And.ETicaret.Core;
using And.ETicaret.Core.Entity;
using And.ETicaret.UI.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.ETicaret.UI.Web.Controllers
{
    public class OrderController : AndControllerBase
    {
        // GET: Order
        [Route("Siparisver")]
        public ActionResult AddressList()
        {
            var db = new AndDB();
            var data = db.UserAddresses.Where(x => x.Id == LoginUserId).ToList(); //tolist demezsek sorgu çalışmaz

            return View(data);
        }
        public ActionResult CreateUserAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAddress(UserAddress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserId = LoginUserId;
            entity.UpdateDate = DateTime.Now;
            entity.IsActive = true;
            entity.UserId = LoginUserId;

            var db = new AndDB();
            db.UserAddresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AddressList");
        }
        public ActionResult CreateOrder(int id)
        {
            //id=> adres id
            //loginuser olduğu içn kim olduğunu biliyor
            //kim olduğunu bildiği için sepette ki ürünleri biliyor
            var db = new AndDB();
            var sepet = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserId).ToList();
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserId = LoginUserId;
            order.StatusId = 1;
            order.TotalProductPrice = sepet.Sum(x => x.Product.Price);
            order.TotalTaxPrice = sepet.Sum(x => x.Product.Tax);
            order.TotalDiscount = sepet.Sum(x => x.Product.Discount);
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice; //ana para ve vergi toplamı
            order.UserAddressId = id;
            order.UserId = LoginUserId;
            order.OrderProducts = new List<OrderProduct>();
            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserId=LoginUserId,
                    ProductId=item.ProductId,
                    Quantity=item.Quantity
                });
                db.Baskets.Remove(item); //siparişi onayladıktan sonra
                //sepeti temizlemek için
            }

            db.Orders.Add(order);
            db.SaveChanges();
            var orderid =order.Id;
            //kullanıcının siparişlerin içinden son siparişinin id si.
            return RedirectToAction("Detail",new { id=orderid});
        }

        public ActionResult Detail(int id) //order id
        {
            var db = new AndDB();
            var data = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product")
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAddress")
                .Where(x => x.Id == id).FirstOrDefault();
            return View(data);

        }

        [Route("Siparislerim")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Orders.Include("Status").Where(x => x.UserId == LoginUserId).ToList();
            return View(data);
        }
        public ActionResult Pay(int id)
        {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.Id == id).FirstOrDefault();
            order.StatusId = 6;
            db.SaveChanges();
            return RedirectToAction("Detail",new { id=order.Id});
        }
    }
}