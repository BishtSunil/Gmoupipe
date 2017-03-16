using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class OfficeAdministrationController : Controller
    {
        //
        // GET: /OfficeAdministration/

        public ActionResult OwnerAdvance()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
           
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return PartialView("_OwnerAdvance", vivraniviewmodel);
        }

        public ActionResult SaveOwnerAdvance(DomainModelEntities.OfficeAdvanceAdmin model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.InsertedBy = user.LoginEmpID;
            BusinessAccessLayer.BALOfficeAdmin.BALInsertAdvance(model);
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        public ActionResult AmountAdvace()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
           
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return PartialView("_ShowAmountAdvace", vivraniviewmodel);
        }

        [HttpGet]
        public ActionResult GetAllOwnerAdvance()
        {
        var data=    BusinessAccessLayer.BALOfficeAdmin.BALGetAllAdvanceByDate();
        return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
