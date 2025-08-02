using Empleados.Enums;
using Empleados.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Empleados.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private static List<Employee> employeeList = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Ana", LastName = "García", Position = Posicion.Recepcionista },
            new Employee { Id = 2, FirstName = "Carlos", LastName = "Pérez", Position = Posicion.Gerente },
            new Employee { Id = 3, FirstName = "Sofía", LastName = "Martínez", Position = Posicion.Limpieza }
        };
        private static int nextId = 4;

        public ActionResult Index()
        {
            return View(employeeList.OrderBy(e => e.Id).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Position")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = nextId++;
                employeeList.Add(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Position")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee employeeInList = employeeList.FirstOrDefault(e => e.Id == employee.Id);
                if (employeeInList != null)
                {
                    employeeInList.FirstName = employee.FirstName;
                    employeeInList.LastName = employee.LastName;
                    employeeInList.Position = employee.Position;
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);
            if (employee == null) return HttpNotFound();
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employeeList.Remove(employee);
            }
            return RedirectToAction("Index");
        }
    }
}