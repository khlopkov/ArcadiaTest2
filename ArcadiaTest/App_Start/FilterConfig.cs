using ArcadiaTest.Middlewares;
using System.Web;
using System.Web.Mvc;

namespace ArcadiaTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
