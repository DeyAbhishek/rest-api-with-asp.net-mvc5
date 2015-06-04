using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPO.BL.Constants;
using TPO.BL.Repositories.Message;
using TPO.DL.Models;

namespace TPOWeb.Controllers.RawMaterialQCTest
{
    public class FailPropertyListController : Controller
    {
        private TPOMVCApplicationEntities db = new TPOMVCApplicationEntities();

        // GET: /FailPropertyList/
        public ActionResult Index()
        {
            return View(db.FailProperties.ToList());
        }

        // GET: /FailPropertyList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] =
                    MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FailProperty failproperty = db.FailProperties.Find(id);
            if (failproperty == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(failproperty);
        }

        // GET: /FailPropertyList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FailPropertyList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Code,Description,SortOrder,DateEntered,EnteredBy,LastModified,ModifiedBy")] FailProperty failproperty)
        {
            if (ModelState.IsValid)
            {
                db.FailProperties.Add(failproperty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(failproperty);
        }

        // GET: /FailPropertyList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FailProperty failproperty = db.FailProperties.Find(id);
            if (failproperty == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(failproperty);
        }

        // POST: /FailPropertyList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Code,Description,SortOrder,DateEntered,EnteredBy,LastModified,ModifiedBy")] FailProperty failproperty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(failproperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(failproperty);
        }

        // GET: /FailPropertyList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoId);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FailProperty failproperty = db.FailProperties.Find(id);
            if (failproperty == null)
            {
                TempData["ActionMessage"] = MessageRepository.GetStringValue(MessageKeys.ResponseMessageFailNoRecord);
                TempData["ActionMessageType"] = MessageRepository.GetStringValue(MessageKeys.ResponseTypeError);
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(failproperty);
        }

        // POST: /FailPropertyList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FailProperty failproperty = db.FailProperties.Find(id);
            db.FailProperties.Remove(failproperty);
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
