using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.DL.Models;

namespace TPOWeb.Controllers.TPOProductionShiftType
{
    public class TPOProductionShiftTypeController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /TPOProductionShiftType/
        public ActionResult Index()
        {
            return View(db.ProductionShiftTypes.ToList());
        }

        // GET: /TPOProductionShiftType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShiftType productionshifttype = db.ProductionShiftTypes.Find(id);
            if (productionshifttype == null)
            {
                return HttpNotFound();
            }
            return View(productionshifttype);
        }

        // GET: /TPOProductionShiftType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TPOProductionShiftType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Code,Description,SortOrder,DateEntered,EnteredBy,LastModified,ModifiedBy")] ProductionShiftType productionshifttype)
        {
            if (ModelState.IsValid)
            {
                db.ProductionShiftTypes.Add(productionshifttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productionshifttype);
        }

        // GET: /TPOProductionShiftType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShiftType productionshifttype = db.ProductionShiftTypes.Find(id);
            if (productionshifttype == null)
            {
                return HttpNotFound();
            }
            return View(productionshifttype);
        }

        // POST: /TPOProductionShiftType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Code,Description,SortOrder,DateEntered,EnteredBy,LastModified,ModifiedBy")] ProductionShiftType productionshifttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionshifttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productionshifttype);
        }

        // GET: /TPOProductionShiftType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShiftType productionshifttype = db.ProductionShiftTypes.Find(id);
            if (productionshifttype == null)
            {
                return HttpNotFound();
            }
            return View(productionshifttype);
        }

        // POST: /TPOProductionShiftType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionShiftType productionshifttype = db.ProductionShiftTypes.Find(id);
            db.ProductionShiftTypes.Remove(productionshifttype);
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
