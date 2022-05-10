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
    public class HomeController : AndControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var db = new AndDB();
            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList(); //son eklenen 5 ürünü tersten sırala

            return View(data);
        }
        public PartialViewResult GetMenu()
        {
            var db = new AndDB();
            var menus = db.Categories.Where(x => x.ParentId == 0).ToList();
            return PartialView(menus);
        }
        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string Email,string Password)
        {
            var db = new AndDB();
            var users = db.Users.Where(x => x.Email == Email && x.Password == Password && x.IsActive == true && x.IsAdmin==false).ToList();
            if (users.Count==1)
            {
                //giriş ok.
                Session["LoginUserId"] = users.FirstOrDefault().Id;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/"); // "/" anasayfa demek

            }
            else
            {
                ViewBag.Error = "Hatalı Kullanıcı Adı veya Şifre";
                return View();

            }
        }
        [Route("Uye-Kayit")]

        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserId = 1;
                entity.IsActive = true;
                entity.IsAdmin = false;
                var db = new AndDB();
                db.Users.Add(entity);
                db.SaveChanges();
                return Redirect("/");

            }
            catch (Exception ex)
            {
                //herhangi bir hata alınabilir eğer hata alınırsa
                //kullanıcıyı üye sayfasına yönlendirmek için try-catch kullandık
                return View();
            }
        }
    }
}