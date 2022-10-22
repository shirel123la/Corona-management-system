using HospitalSystem_Corona.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HospitalSystem_Corona.Controllers
{
    public class VaccinationsController : Controller
    {
        private PersonalInformationEntities db = new PersonalInformationEntities();

        // GET: Vaccinations
        public ActionResult Index()
        {
            var vaccination = db.Vaccination.Include(v => v.Manufacturer).Include(v => v.Patient);

            return View(vaccination.ToList());
        }

        //GET:Vaccinations/:Id
        public ActionResult GetVaccinationsById(int? Id)
        {
          
            var vaccinations = from v in db.Vaccination
                        where v.patient_id == Id
                        select v;

            return View("Index",vaccinations.ToList());

        }
        // GET: Vaccinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccination vaccination = db.Vaccination.Find(id);
            if (vaccination == null)
            {
                return HttpNotFound();
            }
            return View(vaccination);
        }

        // GET: Vaccinations/Create
        public ActionResult Create()
        {
            ViewBag.manufacturer_id = new SelectList(db.Manufacturer, "manufacturar_id", "name");
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number");
            return View();
        }

        // POST: Vaccinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vaccination_id,patient_id,manufacturer_id,vaccination_date")] Vaccination vaccination)
        {
    
                if (ModelState.IsValid)
            {

                var count = db.Vaccination.Where (v=>v.patient_id == vaccination.patient_id).Count();
                if (count < 4) { 
                    db.Vaccination.Add(vaccination);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                  
            ViewBag.manufacturer_id = new SelectList(db.Manufacturer, "manufacturar_id", "name", vaccination.manufacturer_id);
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", vaccination.patient_id);
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccination vaccination = db.Vaccination.Find(id);
            if (vaccination == null)
            {
                return HttpNotFound();
            }
            ViewBag.manufacturer_id = new SelectList(db.Manufacturer, "manufacturar_id", "name", vaccination.manufacturer_id);
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", vaccination.patient_id);
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vaccination_id,patient_id,manufacturer_id,vaccination_date")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manufacturer_id = new SelectList(db.Manufacturer, "manufacturar_id", "name", vaccination.manufacturer_id);
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_number", vaccination.patient_id);
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vaccination vaccination = db.Vaccination.Find(id);
            if (vaccination == null)
            {
                return HttpNotFound();
            }
            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaccination vaccination = db.Vaccination.Find(id);
            db.Vaccination.Remove(vaccination);
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
