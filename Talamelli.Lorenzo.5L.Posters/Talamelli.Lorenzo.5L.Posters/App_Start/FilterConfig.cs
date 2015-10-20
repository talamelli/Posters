using System.Web;
using System.Web.Mvc;

namespace Talamelli.Lorenzo._5L.Posters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
