using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaHotel.Models;
using System;

namespace SistemaHotel.Controllers
{
    public class ReservasController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            var reservas = db.Reservas.Include(r => r.Clientes).Include(r => r.Habitaciones).ToList();
            return View(reservas);
        }

        public ActionResult Create()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres");
            ViewBag.HabitacionId = new SelectList(db.Habitaciones, "Id", "Numero");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,HabitacionId,FechaIngreso,FechaSalida,Estado,Adultos,Ninos,MontoEstimado,MontoPagado")] Reservas reserva)
        {
            if (reserva.FechaSalida <= reserva.FechaIngreso)
                ModelState.AddModelError("FechaSalida", "La fecha de salida debe ser posterior a la de ingreso.");
            if (reserva.Adultos < 1)
                ModelState.AddModelError("Adultos", "Debe haber al menos un adulto.");
            if (reserva.Ninos < 0)
                ModelState.AddModelError("Ninos", "No puede ser negativo.");
            if (reserva.MontoEstimado < 0)
                ModelState.AddModelError("MontoEstimado", "No puede ser negativo.");
            if (reserva.MontoPagado < 0)
                ModelState.AddModelError("MontoPagado", "No puede ser negativo.");
            if (ModelState.IsValid)
            {
                db.Reservas.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", reserva.ClienteId);
            ViewBag.HabitacionId = new SelectList(db.Habitaciones, "Id", "Numero", reserva.HabitacionId);
            return View(reserva);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Reservas reserva = db.Reservas.Find(id);
            if (reserva == null) return HttpNotFound();
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", reserva.ClienteId);
            ViewBag.HabitacionId = new SelectList(db.Habitaciones, "Id", "Numero", reserva.HabitacionId);
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClienteId,HabitacionId,FechaIngreso,FechaSalida,Estado,Adultos,Ninos,MontoEstimado,MontoPagado")] Reservas reserva)
        {
            if (reserva.FechaSalida <= reserva.FechaIngreso)
                ModelState.AddModelError("FechaSalida", "La fecha de salida debe ser posterior a la de ingreso.");
            if (reserva.Adultos < 1)
                ModelState.AddModelError("Adultos", "Debe haber al menos un adulto.");
            if (reserva.Ninos < 0)
                ModelState.AddModelError("Ninos", "No puede ser negativo.");
            if (reserva.MontoEstimado < 0)
                ModelState.AddModelError("MontoEstimado", "No puede ser negativo.");
            if (reserva.MontoPagado < 0)
                ModelState.AddModelError("MontoPagado", "No puede ser negativo.");
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", reserva.ClienteId);
            ViewBag.HabitacionId = new SelectList(db.Habitaciones, "Id", "Numero", reserva.HabitacionId);
            return View(reserva);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Reservas reserva = db.Reservas.Include(r => r.Clientes).Include(r => r.Habitaciones).FirstOrDefault(r => r.Id == id);
            if (reserva == null) return HttpNotFound();
            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservas reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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
