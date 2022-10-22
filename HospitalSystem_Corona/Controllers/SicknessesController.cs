using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalSystem_Corona.Models;

namespace HospitalSystem_Corona.Controllers
{
    public class SicknessesController : Controller
    {
        private PersonalInformationEntities db = new PersonalInformationEntities();

        // GET: Sicknesses
        public ActionResult Index()
        {
            var sickness = db.Sickness.Include(s => s.Patient);
            return View(sickness.ToList());
        }


        //GET:Sickness/:Id
        public ActionResult GetSicknessById(int? Id)
        {

            var sickness = from s in db.Sickness
                               where s.patient_id == Id
                               select s;

            return View("Index", sickness.ToList());

        }

        // GET: Sicknesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sickness sickness = db.Sickness.Find(id);
            if (sickness == null)
            {
                return HttpNotFound();
            }
            return View(sickness);
        }

        // GET: Sicknesses/Create
        public ActionResult Create()
        {
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number");
            return View();
        }

        // POST: Sicknesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sickness_id,positive_result,date_of_recovery,patient_id")] Sickness sickness)
        {

            //Limited to one illness per patient
            if (ModelState.IsValid)
            {
                var existsSikcness = db.Sickness.FirstOrDefault(s => s.patient_id == sickness.patient_id);
                if (existsSikcness == null)
                {
                    db.Sickness.Add(sickness);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", sickness.patient_id);
            return View(sickness);
        }

        // GET: Sicknesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sickness sickness = db.Sickness.Find(id);
            if (sickness == null)
            {
                return HttpNotFound();
            }
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", sickness.patient_id);
            return View(sickness);
        }

        // POST: Sicknesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sickness_id,positive_result,date_of_recovery,patient_id")] Sickness sickness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sickness).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", sickness.patient_id);
            return View(sickness);
        }

        // GET: Sicknesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sickness sickness = db.Sickness.Find(id);
            if (sickness == null)
            {
                return HttpNotFound();
            }
            return View(sickness);
        }

        // POST: Sicknesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sickness sickness = db.Sickness.Find(id);
            db.Sickness.Remove(sickness);
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
