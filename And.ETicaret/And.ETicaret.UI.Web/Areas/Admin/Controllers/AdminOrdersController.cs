using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using And.ETicaret.Core;
using And.ETicaret.Core.Entity;

namespace And.ETicaret.UI.Web.Areas.Admin.Controllers
{
    public class AdminOrdersController : Controller
    {
        private AndDB db = new AndDB();

        // GET: Admin/AdminOrders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Status).Include(o => o.User).Include(o => o.UserAddress);
            return View(orders.ToList());
        }

        // GET: Admin/AdminOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.UserAddressId = new SelectList(db.UserAddresses, "Id", "Title");
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserAddressId,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,StatusId,CreateDate,UpdateDate,CreateUserId,UpdateUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", order.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            ViewBag.UserAddressId = new SelectList(db.UserAddresses, "Id", "Title", order.UserAddressId);
            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", order.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            ViewBag.UserAddressId = new SelectList(db.UserAddresses, "Id", "Title", order.UserAddressId);
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserAddressId,TotalProductPrice,TotalTaxPrice,TotalDiscount,TotalPrice,StatusId,CreateDate,UpdateDate,CreateUserId,UpdateUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", order.StatusId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            ViewBag.UserAddressId = new SelectList(db.UserAddresses, "Id", "Title", order.UserAddressId);
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
