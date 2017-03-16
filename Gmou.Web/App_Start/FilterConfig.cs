using System.Web;
using System.Web.Mvc;
using UserValidation.Attributes;

namespace Gmou.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new LogonAuthorize());
        }

    }
}