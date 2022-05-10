using And.ETicaret.Core;
using And.ETicaret.UI.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.ETicaret.UI.Web.Controllers
{
    public class ProductController : AndControllerBase
    {
        // GET: Product
        [Route("urun/{title}/{id}")]
        public ActionResult Detail(string title,int id)
        {
            var db = new AndDB();
            var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(product);
        }
    }
}