using System.Web.Mvc;

namespace SistemaHotel.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var action = filterContext.ActionDescriptor.ActionName.ToLower();
            // Permitir acceso solo a Login/Index y Login/Logout sin sesión
            if (controller == "login" && (action == "index" || action == "logout"))
                return;
            if (filterContext.HttpContext.Session["Usuario"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}
