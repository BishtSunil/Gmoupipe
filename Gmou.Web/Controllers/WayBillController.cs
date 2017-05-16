using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gmou.BusinessAccessLayer;
using Gmou.DomainModelEntities;
using Gmou.Web.Models;

namespace Gmou.Web.Controllers
{
    public class WayBillController : Controller
    {
        //
        // GET: /WayBill/
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WayBillController));
        public ActionResult Index()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            int? data = WayBillBAL.GetPendingVivraniSerial(user.LoginEmpID, user.DepartmentID);
            if (data != null)
            {
                ViewBag.vivraniID = data;
            }
            return PartialView(@"~/Views\WayBill\_wayBill.cshtml");
        }
        public ActionResult AddVivrani()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            Vivirani model = new Vivirani { UserID = user.LoginEmpID, DepartmentId = user.DepartmentID, IsSubmitted = false };
            var data = WayBillBAL.GetCashVivraiSerial(model);
            VivraniViewModel vivraniviewmodel = new VivraniViewModel
            {
                busowner = new SelectList(data.BusOwner, "BusOwnerID", "BusOwnerName"),
                bus = new SelectList(data.bus, "BusID", "BusNumber"),
                fuelpump = new SelectList(data.fuelpump, "FuelPumpID", "PumpName"),
                fueltype = new SelectList(data.fueltype, "FuelTypeID", "FuelTypeName"),
                VivraniSerialNumber = data.VivraniSerialNumber
            };
            ViewBag.serialnumber = data.VivraniSerialNumber;
            return PartialView(@"~/Views\Admin\Partial\_vivraniEnrty.cshtml", vivraniviewmodel);

        }
        [HttpGet]
      public ActionResult GetBusOwnerName(string busID)
        {
            var data = WayBillBAL.GetOwnerDetails(busID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ValidateWayBillSerialNumber(string waybillserialno, string waybillbookno, string busID)
        {
            var data = WayBillBAL.GetLastWayBill(Convert.ToInt32(waybillserialno), Convert.ToInt32(waybillbookno), Convert.ToInt32(busID));
         return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddVivraniDeatil(VivraniDetails model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.EmployeeID = user.LoginEmpID;
            model.DepartmentID = user.DepartmentID;
            var data = BusinessAccessLayer.WayBillBAL.AddVivraniDetails(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOtherExpences()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            Vivirani model = new Vivirani { UserID = user.LoginEmpID, DepartmentId = user.DepartmentID, IsSubmitted = false };
            var data = WayBillBAL.GetCashSheetSerialNumber(model);
            VivraniViewModel vivraniviewmodel = new VivraniViewModel
            {

                bus = new SelectList(data.bus, "BusID", "BusNumber"),
                VivraniSerialNumber = data.CashSheetSerialNumber
            };
            ViewBag.Cashserialnumber = data.CashSheetSerialNumber;
            return PartialView(@"~/Views\Admin\Partial\_otherExpences.cshtml", vivraniviewmodel);

        }

        public ActionResult AddExpDetail(CashSheetDetails model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;

            model.DepartmentID = user.DepartmentID;
            model.InsertedBy = user.LoginEmpID;
            var data = WayBillBAL.AddCashsheetOtherExpenses(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult chkOutCashDetail()
        {

            return PartialView(@"~/Views\Admin\Partial\_cashCheckoutDetail.cshtml");
        }
        [HttpGet]
        public ActionResult GetCashSheet()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;

            var data = WayBillBAL.GetGeneratedCashSheet(user.LoginEmpID, user.DepartmentID,DateTime.Now);
            var cashSheetId = data.otherexepnseslist.Select(k => k.CashSheetSerial).FirstOrDefault();
            ViewBag.CashSheetID = cashSheetId;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BusHistory()
        {
            var result = WayBillBAL.GetAllBusses();
            BusOwnerViewModel bo = new BusOwnerViewModel();

            bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");


            return PartialView(@"~/Views\WayBill\_bushistory.cshtml", bo);
         
        }
        [HttpGet]
        public ActionResult GetBusJourneyHistory(string busId)
        {
            int busID = Convert.ToInt32(busId);
            var result = WayBillBAL.GetBusHistory(busID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult  Coupon(CouponDetails model)
        {
            return PartialView(@"~/Views\WayBill\_addCuppon.cshtml");
        }

        [HttpPost]
        public ActionResult SaveCouponDetails(CouponDetails model)
        {
            var data = WayBillBAL.BALSaveCouponDetails(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllCouponDetails()
        {

            var data = WayBillBAL.BALGetAllCouponDetails();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveWayBillCouponDetails(CouponDetails model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.InsertedBy = user.LoginEmpID;
            var data = WayBillBAL.BALSaveWayBillCouponDetails(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
      [HttpGet]
        public ActionResult GetAllWayBillCouponDetails(int waybillbookno, int waybillserialno)
        {
            var data = WayBillBAL.BALGetAllWayBillCouponDetails(waybillbookno, waybillserialno);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
      public ActionResult AddWaybill()
      {
          var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
          Vivirani model = new Vivirani { UserID = user.LoginEmpID, DepartmentId = user.DepartmentID, IsSubmitted = false };
          var data = WayBillBAL.GetCashVivraiSerial(model);
          VivraniViewModel vivraniviewmodel = new VivraniViewModel
          {
              busowner = new SelectList(data.BusOwner, "BusOwnerID", "BusOwnerName"),
              bus = new SelectList(data.bus, "BusID", "BusNumber"),
              fuelpump = new SelectList(data.fuelpump, "FuelPumpID", "PumpName"),
              fueltype = new SelectList(data.fueltype, "FuelTypeID", "FuelTypeName"),
              VivraniSerialNumber = data.VivraniSerialNumber
          };
          ViewBag.serialnumber = data.VivraniSerialNumber;
          return PartialView(@"~/Views\\WayBill\demoWayBill.cshtml", vivraniviewmodel);

      }
        [HttpPost]
        public ActionResult SaveWayBillEntry(WayBillTicketModel model)
      {
            try
            {
                var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
                model.InsertedBy = user.LoginEmpID;
                var data = WayBillBAL.BALSaveWayBillEntry(model);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                logger.Error(ex.ToString());
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ValidateTicketSerialNumber(string ticketstartno, string ticketno, string busID)
        {
            var data = WayBillBAL.GetLastTicketSerial(Convert.ToInt32(ticketstartno), Convert.ToInt32(ticketno), Convert.ToInt32(busID));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult showCashVivraani(int busId)
        {
             var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            Vivirani model = new Vivirani { UserID = user.LoginEmpID, DepartmentId = user.DepartmentID, IsSubmitted = false };
            var data = WayBillBAL.GetCashVivraiSerial(model);
            VivraniViewModel vivraniviewmodel = new VivraniViewModel
            {
                busowner = new SelectList(data.BusOwner, "BusOwnerID", "BusOwnerName"),
                bus = new SelectList(data.bus, "BusID", "BusNumber"),
                fuelpump = new SelectList(data.fuelpump, "FuelPumpID", "PumpName"),
                fueltype = new SelectList(data.fueltype, "FuelTypeID", "FuelTypeName"),
                busId = busId,
              
                
            };
            return PartialView(@"~/Views\\WayBill\_showCashVivrani.cshtml", vivraniviewmodel);
        }
        [HttpGet]

        public ActionResult GenerateCashVivrani(int busid,int vivraniid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
          var result=  WayBillBAL.BALGenerateCashVivrani(empid, busid,vivraniid);
          return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowCashVivrani(int busid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
            var result = WayBillBAL.BALShowCashVivrani(empid, busid);
          var amount =   result.Sum(m => m.Fare);
           
            var _vivraniid = result.Select(k => k.VivraniNumber).FirstOrDefault();
            var busnumber = result.Select(k => k.BusNumber).FirstOrDefault().ToString();

            var data = WayBillBAL.BALGetOwnerVivraniInfo(busnumber);
            Helpers.SMSGateway.SendVivraniSMS(amount, data.Contact, data.OwnerName,busnumber, data.TotalAmount, _vivraniid);
            ViewBag.VivraniID = _vivraniid;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCashSheetSummary(CashSheetSummary model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.InsertedBy = user.LoginEmpID;
            var data = WayBillBAL.BALSaveCashSheetSummary(model);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetVivraniStatus(int busid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
           
            var data = WayBillBAL.BALCheckIfVivraniCreated(user.LoginEmpID,busid);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetWayBillDetails(int busid)
        {
            return null;

        }
        [HttpGet]
        public ActionResult UpdateVivraniAmount(int vivraniid, decimal amount)
        {
          var data =  WayBillBAL.BALUpdateVivrani(vivraniid, amount);
          return Json(data, JsonRequestBehavior.AllowGet);
                
        }

        public ActionResult GamanPatra()
        {
          
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
           
            GamanViewModel gamanmodel = new GamanViewModel
            {

               bus = new SelectList(bus, "BusID", "BusNumber"),
               dipo= new SelectList(dipotype.DipoList, "DipoID", "DipoName"),
                GamanPatraSerialNumber = WayBillBAL.BALGetGamanPatra()
            };

            return PartialView(@"~/Views\\WayBill\_gamanPatra.cshtml", gamanmodel);
        }
        [HttpPost]
        public ActionResult SaveGamanPatra(GamanPatraModel model)
        {
            var data = WayBillBAL.BALSaveGamanPatra(model);
            return PartialView(@"~/Views\\WayBill\_gamanpatraReport.cshtml", data);

        }

        [HttpGet]
        public ActionResult GetGamanPatra()
        {

            var data = WayBillBAL.BALGetGamanPatra(2);
            return PartialView(@"~/Views\\WayBill\_gamanpatraReport.cshtml", data);

        }

        [HttpGet]
        public ActionResult GetDistancePrice(string from, string to)
        {
           var data =  BusinessAccessLayer.WayBillBAL.BALGetDistanceFare(from, to);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AutoComplete(string prefix)
        {
            var data = BusinessAccessLayer.WayBillBAL.BALGetRouteName(prefix);
            //Searching records from list using LINQ query  
            var CityName = (from N in data
                            where N.Route.StartsWith(prefix)
                            select new {
                                label = N.Route,
                                val = N.RouteID
                            });
            return Json(CityName, JsonRequestBehavior.AllowGet);
           
        }
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


