using And.ETicaret.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.ETicaret.UI.Web.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        AndDB db = new AndDB(); //global tanımlamalarda var kullanılmaz.

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string email,string password)
        {
          var data=  db.Users.Where(x => x.Email == email && x.Password == password && x.IsActive==true&& x.IsAdmin==true).ToList();
            if (data.Count==1)
            {
                //doğru giriş //adminin her controllerlarında bunu kontrol etmemiz gerekiyor.
                Session["AdminLoginUser"] = data.FirstOrDefault();

                return Redirect("/admin"); //doğru giriş yaptıysan adminin ana sayfasına gönder.
            }
            else
            {
                //hatalı giriş-hatalı ise aynı sayfaya dön 
                return View();
            }
           
        }
    }
}