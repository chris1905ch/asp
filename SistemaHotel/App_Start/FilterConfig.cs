using System.Web;
using System.Web.Mvc;
using SistemaHotel.Filters;

namespace SistemaHotel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthFilter());
        }
    }
}
