using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class ResultController : Controller
    {
        //
        // GET: /Result/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            //var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            GamanViewModel model = new GamanViewModel
            {
                bus = new SelectList(bus, "BusID", "BusNumber")

            };

            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Demo()
        {
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            //var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            GamanViewModel model = new GamanViewModel
            {
                bus = new SelectList(bus, "BusID", "BusNumber")

            };

            return View(model);
        }
    }
}
