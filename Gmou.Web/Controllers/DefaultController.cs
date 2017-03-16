using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
       
            return PartialView(@"~/Views\_View.cshtml");
        }

        public ActionResult SaveVivraniDeatil()
        {
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
