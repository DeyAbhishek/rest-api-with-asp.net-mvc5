using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.DL.Models;

namespace TPOWeb.Controllers.TPOProductionShift
{
    public class TPOProductionShiftController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /TPOProductionShift/
        public ActionResult Index()
        {
            var productionshifts = db.ProductionShifts.Include(p => p.Plant).Include(p => p.ProductionShiftType);
            return View(productionshifts.ToList());
        }

        // GET: /TPOProductionShift/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShift productionshift = db.ProductionShifts.Find(id);
            if (productionshift == null)
            {
                return HttpNotFound();
            }
            return View(productionshift);
        }

        // GET: /TPOProductionShift/Create
        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code");
            ViewBag.TypeID = new SelectList(db.ProductionShiftTypes, "ID", "Code");
            return View();
        }

        // POST: /TPOProductionShift/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PlantID,TypeID,Code,StartTime,EndTime,DateEntered,EnteredBy,LastModified,ModifiedBy")] ProductionShift productionshift)
        {
            if (ModelState.IsValid)
            {
                db.ProductionShifts.Add(productionshift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", productionshift.PlantID);
            ViewBag.TypeID = new SelectList(db.ProductionShiftTypes, "ID", "Code", productionshift.TypeID);
            return View(productionshift);
        }

        // GET: /TPOProductionShift/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShift productionshift = db.ProductionShifts.Find(id);
            if (productionshift == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", productionshift.PlantID);
            ViewBag.TypeID = new SelectList(db.ProductionShiftTypes, "ID", "Code", productionshift.TypeID);
            return View(productionshift);
        }

        // POST: /TPOProductionShift/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PlantID,TypeID,Code,StartTime,EndTime,DateEntered,EnteredBy,LastModified,ModifiedBy")] ProductionShift productionshift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionshift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", productionshift.PlantID);
            ViewBag.TypeID = new SelectList(db.ProductionShiftTypes, "ID", "Code", productionshift.TypeID);
            return View(productionshift);
        }

        // GET: /TPOProductionShift/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionShift productionshift = db.ProductionShifts.Find(id);
            if (productionshift == null)
            {
                return HttpNotFound();
            }
            return View(productionshift);
        }

        // POST: /TPOProductionShift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionShift productionshift = db.ProductionShifts.Find(id);
            db.ProductionShifts.Remove(productionshift);
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
