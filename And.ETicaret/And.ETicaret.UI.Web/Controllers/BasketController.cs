using And.ETicaret.Core;
using And.ETicaret.UI.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.ETicaret.UI.Web.Controllers
{
    public class BasketController : AndControllerBase
    {
        // GET: Basket
        [HttpPost]
        public JsonResult AddProduct(int proId,int quantity)
        {
            //login olmayan gelemeyecek
            //arka planda dönecek kullanıcı görmeyecek 
            var db = new AndDB();
            db.Baskets.Add(new Core.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserId=LoginUserId,
                ProductId=proId,
                Quantity=quantity,
                UserId=LoginUserId
                
            });
          var rt=  db.SaveChanges();
            return Json(rt,JsonRequestBehavior.AllowGet);
        }

        [Route("Sepetim")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserId).ToList();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var db = new AndDB();
            var deleteitem = db.Baskets.Where(x => x.Id == id).FirstOrDefault();
            db.Baskets.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}