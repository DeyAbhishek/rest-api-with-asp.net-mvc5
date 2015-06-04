using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.DL.Models;

namespace TPOWeb.Controllers.RawMaterialQCTest
{
    public class RawMaterialQCTestController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /RawMaterialQCTest/
        public ActionResult Index()
        {
            var rawmaterialqcs = db.RawMaterialQCs.Include(r => r.Plant).Include(r => r.User);
            return View(rawmaterialqcs.ToList());
        }

        // GET: /RawMaterialQCTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQC rawmaterialqc = db.RawMaterialQCs.Find(id);
            if (rawmaterialqc == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialqc);
        }

        // GET: /RawMaterialQCTest/Create
        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code");
            ViewBag.QCTechUserID = new SelectList(db.Users, "ID", "Username");
            return View();
        }

        // POST: /RawMaterialQCTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PlantID,QCTechUserID,RawMaterialID,RawMaterialLotID,VisualInspection,SpecGrav,ColorCoA,ColorFS,MFCoA,MFFS,ACCoA,ACFS,MoistCoA,MoistFS,CBCoA,CBFS,BoxCarTested,Comments,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQC rawmaterialqc)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterialQCs.Add(rawmaterialqc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqc.PlantID);
            ViewBag.QCTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialqc.QCTechUserID);
            return View(rawmaterialqc);
        }

        // GET: /RawMaterialQCTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQC rawmaterialqc = db.RawMaterialQCs.Find(id);
            if (rawmaterialqc == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqc.PlantID);
            ViewBag.QCTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialqc.QCTechUserID);
            //ViewBag.RawMaterialSpecificGravityId = rawmaterialqc.RawMaterialSpecificGravities.Any() ? rawmaterialqc.RawMaterialSpecificGravities.First().ID: 0;
            return View(rawmaterialqc);
        }

        // POST: /RawMaterialQCTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PlantID,QCTechUserID,RawMaterialID,RawMaterialLotID,VisualInspection,SpecGrav,ColorCoA,ColorFS,MFCoA,MFFS,ACCoA,ACFS,MoistCoA,MoistFS,CBCoA,CBFS,BoxCarTested,Comments,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialQC rawmaterialqc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialqc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialqc.PlantID);
            ViewBag.QCTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialqc.QCTechUserID);
            return View(rawmaterialqc);
        }

        // GET: /RawMaterialQCTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialQC rawmaterialqc = db.RawMaterialQCs.Find(id);
            if (rawmaterialqc == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialqc);
        }

        // POST: /RawMaterialQCTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterialQC rawmaterialqc = db.RawMaterialQCs.Find(id);
            db.RawMaterialQCs.Remove(rawmaterialqc);
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
