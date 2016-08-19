using System.Web;
using System.Web.Mvc;
using WebApp.Infrastructure;

namespace WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ProfileResultAttribute());
        }
    }
}
