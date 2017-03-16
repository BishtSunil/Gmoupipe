using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/

        public ActionResult Index()
        {
          var data=  BusinessAccessLayer.BALSupport.GetAllPlaces();
          return View(@"~/Views\Support\_addPlaces.cshtml", data);
        }

        [HttpPost]
        public ActionResult SaveServiceRoute(ServiceRoute model)
        {

            var data = BusinessAccessLayer.BALSupport.BALSaveServiceRoute(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
