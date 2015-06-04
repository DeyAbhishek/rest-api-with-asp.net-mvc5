using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.DL.Models;

//This class May be unused.
namespace TPOWeb.Controllers.RawMaterialQCTest
{
    public class RawMaterialQCFormController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /RawMaterialQCForm/
        public ActionResult Index()
        {
            var rawmaterialspecificgravities = db.RawMaterialSpecificGravities.Include(r => r.RawMaterialQC).Include(r => r.User);
            return View(rawmaterialspecificgravities.ToList());
        }

        // GET: /RawMaterialQCForm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialSpecificGravity rawmaterialspecificgravity = db.RawMaterialSpecificGravities.Find(id);
            if (rawmaterialspecificgravity == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialspecificgravity);
        }

        // GET: /RawMaterialQCForm/Create
        public ActionResult Create()
        {
            ViewBag.RawMaterialQCID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID");
            ViewBag.LabTechUserID = new SelectList(db.Users, "ID", "Username");
            return View();
        }

        // POST: /RawMaterialQCForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,RawMaterialQCID,LabTechUserID,DenIso,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialSpecificGravity rawmaterialspecificgravity)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterialSpecificGravities.Add(rawmaterialspecificgravity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RawMaterialQCID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID", rawmaterialspecificgravity.RawMaterialQCID);
            ViewBag.LabTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialspecificgravity.LabTechUserID);
            return View(rawmaterialspecificgravity);
        }

        // GET: /RawMaterialQCForm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialSpecificGravity rawmaterialspecificgravity = db.RawMaterialSpecificGravities.Find(id);
            if (rawmaterialspecificgravity == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawMaterialQCID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID", rawmaterialspecificgravity.RawMaterialQCID);
            ViewBag.LabTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialspecificgravity.LabTechUserID);
            return View(rawmaterialspecificgravity);
        }

        // POST: /RawMaterialQCForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,RawMaterialQCID,LabTechUserID,DenIso,DateEntered,EnteredBy,LastModified,ModifiedBy")] RawMaterialSpecificGravity rawmaterialspecificgravity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterialspecificgravity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RawMaterialQCID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID", rawmaterialspecificgravity.RawMaterialQCID);
            ViewBag.LabTechUserID = new SelectList(db.Users, "ID", "Username", rawmaterialspecificgravity.LabTechUserID);
            return View(rawmaterialspecificgravity);
        }

        // GET: /RawMaterialQCForm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RawMaterialSpecificGravity rawmaterialspecificgravity = db.RawMaterialSpecificGravities.Find(id);
            if (rawmaterialspecificgravity == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterialspecificgravity);
        }

        // POST: /RawMaterialQCForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterialSpecificGravity rawmaterialspecificgravity = db.RawMaterialSpecificGravities.Find(id);
            db.RawMaterialSpecificGravities.Remove(rawmaterialspecificgravity);
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


        [HttpGet]
        public ActionResult GravityDetail(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TPO.Domain.DTO.RawMaterialQCSpecificGravityDTO _rawMaterialSpecificGravityObject = TPO.BL.RawMaterials.RawMaterialQCSpecificGravity.Get((int)id);
            

            //var rawmaterialspecificgravities = db.RawMaterialSpecificGravities.Include(r => r.RawMaterialQC).Include(r => r.User).Include(r =>r.RawMaterialSpecificGravityDetails);


            //RawMaterialSpecificGravity rawmaterialspecificgravity = db.RawMaterialSpecificGravities.Find(id);
           // RawMaterialSpecificGravityDetail rawmaterialspecificgravitydetail = db.RawMaterialSpecificGravityDetails.Find(id);


            if (_rawMaterialSpecificGravityObject == null)
            {
                return HttpNotFound();
            }
            ViewBag.RawMaterialQCID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialID", _rawMaterialSpecificGravityObject.RawMaterialQCID);
            ViewBag.LabTechUserID = new SelectList(db.Users, "ID", "Username", _rawMaterialSpecificGravityObject.LabTechUserID);

            ViewBag.LotID = new SelectList(db.RawMaterialQCs, "ID", "RawMaterialLotID", _rawMaterialSpecificGravityObject.RawMaterialQCID);

            TPOWeb.Controllers.RawMaterialQCTest.RawMaterialQCSpecificGravityController SGDC = new RawMaterialQCSpecificGravityController();
            TPO.Model.RawMaterials.RawMaterialQCSpecificGravity model = SGDC.MapToModel(_rawMaterialSpecificGravityObject);
            
            return View(model);
         

        }



    }
}
