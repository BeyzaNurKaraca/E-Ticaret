using And.ETicaret.Core;
using And.ETicaret.UI.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace And.ETicaret.UI.Web.Controllers
{
    public class CategoriesDetailController : AndControllerBase
    {
        // GET: CategoriesDetail
        [Route("category/{name}/{id}")]
        public ActionResult Detail(string name,int id)
        {
            var db = new AndDB();
            var cat = db.Products.Where(x => x.IsActive == true&& x.CategoryId==id).ToList();
            ViewBag.category = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(cat);
        }
    }
}