using System.Linq;
using System.Web.Mvc;
using SistemaHotel.Models;

namespace SistemaHotel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string password)
        {
            // Validaci�n de rol y contrase�a fija
            string rol = usuario == "admin" ? "Administrador" : usuario == "trabajador" ? "Trabajador" : null;
            if ((rol == "Administrador" && password == "admin1") || (rol == "Trabajador" && password == "trabajador1"))
            {
                using (var db = new HotelDBEntities1())
                {
                    var user = db.Usuarios.FirstOrDefault(u => u.Usuario == usuario && u.Activo);
                    if (user != null && user.Roles.Any(r => r.Nombre == rol))
                    {
                        // Autenticaci�n exitosa
                        Session["Usuario"] = user.Usuario;
                        Session["Rol"] = rol;
                        return RedirectToAction("Menu", "Home");
                    }
                }
                ViewBag.Error = "Usuario o rol incorrecto.";
                return View();
            }
            else
            {
                ViewBag.Error = "Contrase�a incorrecta.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                var cookie = new System.Web.HttpCookie(".ASPXAUTH");
                cookie.Expires = System.DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index");
        }
    }
}
