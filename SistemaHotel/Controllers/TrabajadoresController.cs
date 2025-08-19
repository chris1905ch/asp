using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class TrabajadoresController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            var trabajadores = db.Trabajadores.ToList();
            return View(trabajadores);
        }

        public ActionResult Create()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombres,Apellidos,DUI_NIT,Telefono,Email,Cargo,Salario,Activo")] Trabajadores trabajador)
        {
            if (string.IsNullOrWhiteSpace(trabajador.Nombres))
                ModelState.AddModelError("Nombres", "El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(trabajador.Apellidos))
                ModelState.AddModelError("Apellidos", "El apellido es obligatorio.");
            if (string.IsNullOrWhiteSpace(trabajador.DUI_NIT))
                ModelState.AddModelError("DUI_NIT", "El DUI/NIT es obligatorio.");
            if (trabajador.Salario < 0)
                ModelState.AddModelError("Salario", "El salario no puede ser negativo.");
            if (ModelState.IsValid)
            {
                db.Trabajadores.Add(trabajador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trabajador);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Trabajadores trabajador = db.Trabajadores.Find(id);
            if (trabajador == null) return HttpNotFound();
            return View(trabajador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,DUI_NIT,Telefono,Email,Cargo,Salario,Activo")] Trabajadores trabajador)
        {
            if (string.IsNullOrWhiteSpace(trabajador.Nombres))
                ModelState.AddModelError("Nombres", "El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(trabajador.Apellidos))
                ModelState.AddModelError("Apellidos", "El apellido es obligatorio.");
            if (string.IsNullOrWhiteSpace(trabajador.DUI_NIT))
                ModelState.AddModelError("DUI_NIT", "El DUI/NIT es obligatorio.");
            if (trabajador.Salario < 0)
                ModelState.AddModelError("Salario", "El salario no puede ser negativo.");
            if (ModelState.IsValid)
            {
                db.Entry(trabajador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trabajador);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Trabajadores trabajador = db.Trabajadores.Find(id);
            if (trabajador == null) return HttpNotFound();
            return View(trabajador);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trabajadores trabajador = db.Trabajadores.Find(id);
            db.Trabajadores.Remove(trabajador);
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
