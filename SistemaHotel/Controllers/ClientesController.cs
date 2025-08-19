using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class ClientesController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            var clientes = db.Clientes.ToList();
            return View(clientes);
        }

        public ActionResult Create()
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombres,Apellidos,Documento,Telefono,Email,Pais,Notas")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Documento,Telefono,Email,Pais,Notas")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Index", "Login");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null) return HttpNotFound();
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
