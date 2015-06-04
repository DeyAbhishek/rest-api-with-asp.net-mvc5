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
    public class RawMaterialTestController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /RawMaterialTest/
        public ActionResult Index()
        {
            var rawmaterialtests = db.RawMaterialTests.Include(r => r.Plant).Include(r => r.TestLimitType).Include(r => r.TestLimitType1).Include(r => r.TestLimitType2).Include(r => r.TestLimitType3).Include(r => r.TestLimitType4);
            return View(rawmaterialtests.ToList());
        }

        // GET: /RawMaterialTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialTest rawmaterialtest = db.RawMaterialTests.Find(id);
            if (rawmaterialtest == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialtest);
        }

        // GET: /RawMaterialTest/Create
        public ActionResult Create()
        {
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code");
            ViewBag.ACLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code");
            ViewBag.CBLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code");
            ViewBag.ColorLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code");
            ViewBag.MFLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code");
            ViewBag.MoistLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code");
            return View();
        }

        // POST: /RawMaterialTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PlantID,RawMaterialID,UseColorTest,ColorLimitTypeID,ColorLimit1,ColorLimit2,UseMFTest,MFLimitTypeID,MFLimit1,MFLimit2,UseACTest,ACLimitTypeID,ACLimit1,ACLimit2,UseMoistTest,MoistLimitTypeID,MoistLimit1,MoistLimit2,UseCBTest,CBLimitTypeID,CBLimit1,CBLimit2,UseSpecGrav,UseVisual,TestFrequency,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialTest rawmaterialtest)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterialTests.Add(rawmaterialtest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialtest.PlantID);
            ViewBag.ACLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ACLimitTypeID);
            ViewBag.CBLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.CBLimitTypeID);
            ViewBag.ColorLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ColorLimitTypeID);
            ViewBag.MFLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MFLimitTypeID);
            ViewBag.MoistLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MoistLimitTypeID);
            return View(rawmaterialtest);
        }

        // GET: /RawMaterialTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialTest rawmaterialtest = db.RawMaterialTests.Find(id);
            if (rawmaterialtest == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialtest.PlantID);
            ViewBag.ACLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ACLimitTypeID);
            ViewBag.CBLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.CBLimitTypeID);
            ViewBag.ColorLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ColorLimitTypeID);
            ViewBag.MFLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MFLimitTypeID);
            ViewBag.MoistLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MoistLimitTypeID);
            return View(rawmaterialtest);
        }

        // POST: /RawMaterialTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PlantID,RawMaterialID,UseColorTest,ColorLimitTypeID,ColorLimit1,ColorLimit2,UseMFTest,MFLimitTypeID,MFLimit1,MFLimit2,UseACTest,ACLimitTypeID,ACLimit1,ACLimit2,UseMoistTest,MoistLimitTypeID,MoistLimit1,MoistLimit2,UseCBTest,CBLimitTypeID,CBLimit1,CBLimit2,UseSpecGrav,UseVisual,TestFrequency,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialTest rawmaterialtest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialtest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlantID = new SelectList(db.Plants, "ID", "Code", rawmaterialtest.PlantID);
            ViewBag.ACLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ACLimitTypeID);
            ViewBag.CBLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.CBLimitTypeID);
            ViewBag.ColorLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.ColorLimitTypeID);
            ViewBag.MFLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MFLimitTypeID);
            ViewBag.MoistLimitTypeID = new SelectList(db.TestLimitTypes, "ID", "Code", rawmaterialtest.MoistLimitTypeID);
            return View(rawmaterialtest);
        }

        // GET: /RawMaterialTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialTest rawmaterialtest = db.RawMaterialTests.Find(id);
            if (rawmaterialtest == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialtest);
        }

        // POST: /RawMaterialTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterialTest rawmaterialtest = db.RawMaterialTests.Find(id);
            db.RawMaterialTests.Remove(rawmaterialtest);
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
