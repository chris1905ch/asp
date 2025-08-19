using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class PagosController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            var pagos = db.Pagos.Include(p => p.Reservas).ToList();
            return View(pagos);
        }

        public ActionResult Create()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservaId,Fecha,Monto,Metodo")] Pagos pago)
        {
            if (pago.Monto < 0)
                ModelState.AddModelError("Monto", "El monto no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(pago.Metodo))
                ModelState.AddModelError("Metodo", "El método de pago es obligatorio.");
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id", pago.ReservaId);
            return View(pago);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pagos pago = db.Pagos.Find(id);
            if (pago == null) return HttpNotFound();
            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id", pago.ReservaId);
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReservaId,Fecha,Monto,Metodo")] Pagos pago)
        {
            if (pago.Monto < 0)
                ModelState.AddModelError("Monto", "El monto no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(pago.Metodo))
                ModelState.AddModelError("Metodo", "El método de pago es obligatorio.");
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id", pago.ReservaId);
            return View(pago);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Pagos pago = db.Pagos.Include(p => p.Reservas).FirstOrDefault(p => p.Id == id);
            if (pago == null) return HttpNotFound();
            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagos pago = db.Pagos.Find(id);
            db.Pagos.Remove(pago);
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
