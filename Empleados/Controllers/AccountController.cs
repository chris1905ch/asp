using Empleados.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security; // Necesario para FormsAuthentication

namespace Empleados.Controllers
{
    public class AccountController : Controller
    {
        // --- BASE DE DATOS DE USUARIOS SIMULADA ---
        private static List<User> userList = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "123", Role = Enums.TipoRol.Administrador },
            new User { Id = 2, Username = "empleado", Password = "123", Role = Enums.TipoRol.Empleado }
        };
        // --- ADVERTENCIA: En una aplicación real, NUNCA guardes contraseñas en texto plano. Usa hashing.

        // GET: Account/Login
        [AllowAnonymous] // Permite el acceso a esta página sin estar logueado
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1. Verificar si el usuario y contraseña existen
            var user = userList.FirstOrDefault(u => u.Username.Equals(model.Username, System.StringComparison.OrdinalIgnoreCase) && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }

            // 2. Verificar si el rol seleccionado coincide con el rol del usuario
            if (user.Role != model.Role)
            {
                ModelState.AddModelError("", "Acceso denegado: El rol seleccionado no le corresponde a este usuario.");
                return View(model);
            }

            // 3. Si todo es correcto, crear la cookie de autenticación
            FormsAuthentication.SetAuthCookie(model.Username, false);

            // Redirigir a la página principal de empleados
            return RedirectToAction("Index", "Employees");
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}