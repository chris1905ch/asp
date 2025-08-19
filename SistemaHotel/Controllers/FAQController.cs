using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class FAQController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        // GET: FAQ
        public ActionResult Index()
        {
            db.SeedFAQ(); // Poblar preguntas/respuestas si la tabla está vacía
            var faqs = db.FAQ.ToList();
            return View(faqs);
        }

        // POST: FAQ/Chat
        [HttpPost]
        public JsonResult Chat(string question)
        {
            // Búsqueda simple: pregunta que contenga la palabra clave
            var faqs = db.FAQ.ToList();
            var match = faqs.FirstOrDefault(f => f.Pregunta.ToLower().Contains(question.ToLower()));
            if (match != null)
                return Json(new { answer = match.Respuesta });
            // Si no hay coincidencia exacta, buscar por etiquetas
            var tagMatch = faqs.FirstOrDefault(f => (f.Etiquetas ?? "").ToLower().Contains(question.ToLower()));
            if (tagMatch != null)
                return Json(new { answer = tagMatch.Respuesta });
            // Respuesta por defecto
            return Json(new { answer = "No encontré una respuesta para tu pregunta. Por favor, intenta con otra consulta o contacta al administrador." });
        }
    }
}
