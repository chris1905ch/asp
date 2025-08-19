using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class HabitacionesController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            var habitaciones = db.Habitaciones.Include(h => h.TiposHabitacion).ToList();
            return View(habitaciones);
        }

        public ActionResult Create()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            ViewBag.TipoId = new SelectList(db.TiposHabitacion, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Numero,TipoId,Piso,Estado")] Habitaciones habitacion)
        {
            if (string.IsNullOrWhiteSpace(habitacion.Numero))
                ModelState.AddModelError("Numero", "El número de habitación es obligatorio.");
            if (habitacion.Numero?.Length > 10)
                ModelState.AddModelError("Numero", "El número no puede superar los 10 caracteres.");
            if (habitacion.Piso < 0)
                ModelState.AddModelError("Piso", "El piso no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(habitacion.Estado))
                ModelState.AddModelError("Estado", "El estado es obligatorio.");
            if (habitacion.Estado?.Length > 30)
                ModelState.AddModelError("Estado", "El estado no puede superar los 30 caracteres.");
            if (ModelState.IsValid)
            {
                db.Habitaciones.Add(habitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoId = new SelectList(db.TiposHabitacion, "Id", "Nombre", habitacion.TipoId);
            return View(habitacion);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Habitaciones habitacion = db.Habitaciones.Find(id);
            if (habitacion == null) return HttpNotFound();
            ViewBag.TipoId = new SelectList(db.TiposHabitacion, "Id", "Nombre", habitacion.TipoId);
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Numero,TipoId,Piso,Estado")] Habitaciones habitacion)
        {
            if (string.IsNullOrWhiteSpace(habitacion.Numero))
                ModelState.AddModelError("Numero", "El número de habitación es obligatorio.");
            if (habitacion.Numero?.Length > 10)
                ModelState.AddModelError("Numero", "El número no puede superar los 10 caracteres.");
            if (habitacion.Piso < 0)
                ModelState.AddModelError("Piso", "El piso no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(habitacion.Estado))
                ModelState.AddModelError("Estado", "El estado es obligatorio.");
            if (habitacion.Estado?.Length > 30)
                ModelState.AddModelError("Estado", "El estado no puede superar los 30 caracteres.");
            if (ModelState.IsValid)
            {
                db.Entry(habitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoId = new SelectList(db.TiposHabitacion, "Id", "Nombre", habitacion.TipoId);
            return View(habitacion);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Habitaciones habitacion = db.Habitaciones.Include(h => h.TiposHabitacion).FirstOrDefault(h => h.Id == id);
            if (habitacion == null) return HttpNotFound();
            return View(habitacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitaciones habitacion = db.Habitaciones.Find(id);
            db.Habitaciones.Remove(habitacion);
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
