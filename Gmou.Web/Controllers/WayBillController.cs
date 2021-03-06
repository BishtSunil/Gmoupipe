﻿using System;
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

        public ActionResult AddOtherExpences(int cashid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            Vivirani model = new Vivirani { UserID = user.LoginEmpID, DepartmentId = user.DepartmentID, IsSubmitted = false };
            var data = WayBillBAL.GetCashSheetSerialNumber(model);
            VivraniViewModel vivraniviewmodel = new VivraniViewModel
            {

                bus = new SelectList(data.bus, "BusID", "BusNumber"),
                VivraniSerialNumber = cashid
            };
            ViewBag.Cashserialnumber = cashid;
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

            var data = WayBillBAL.GetGeneratedCashSheet(user.LoginEmpID, user.DepartmentID, DateTime.Now);
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
        public ActionResult Coupon(CouponDetails model)
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
                GMOULogger.Logger.Log(String.Format("Way Bill contoller method {0} ", model.InsertedDate));
                if (!CheckWayBillDuplicacy(model.BusNumber, model.WayBillNo, model.WayBillSerialNo))
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
             // model.InsertedDate=  Helpers.GMOUHelper.ConvertTOIST(model.InsertedDate);
                var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
                model.InsertedBy = user.LoginEmpID;
                var data = WayBillBAL.BALSaveWayBillEntry(model);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               GMOULogger.Logger.Log("Error Exception");
                GMOULogger.Logger.Log(ex.ToString());
                logger.Error(ex.ToString());
            }
            Helpers.GMOUHelper.Log("Retun To actionREsult");
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        private bool CheckWayBillDuplicacy(string busnumber, string waybillbook, string waybillserialno)
        {
            if (WayBillBAL.BALCheckIfWayBillCreated(Convert.ToInt32(busnumber), Convert.ToInt32(waybillbook), Convert.ToInt32(waybillserialno)))
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        [HttpGet]


        public ActionResult CheckWayBillExistence(string busid, string waybillBookNo, string waybillSerrialNo)
        {
            return Json(CheckWayBillDuplicacy(busid, waybillBookNo, waybillSerrialNo), JsonRequestBehavior.AllowGet);

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

        public ActionResult GenerateCashVivrani(int busid, int vivraniid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
            var result = WayBillBAL.BALGenerateCashVivrani(empid, busid, vivraniid,DateTime.Now);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowCashVivrani(int busid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
            var result = WayBillBAL.BALShowCashVivrani(empid, busid);
            var amount = result.Sum(m => m.Fare);

            var _vivraniid = result.Select(k => k.VivraniNumber).FirstOrDefault();
            var busnumber = result.Select(k => k.BusNumber).FirstOrDefault().ToString();

           // var data = WayBillBAL.BALGetOwnerVivraniInfo(busnumber);
           // Helpers.SMSGateway.SendVivraniSMS(amount, data.Contact, data.OwnerName, busnumber, data.TotalAmount, _vivraniid);
            ViewBag.VivraniID = _vivraniid;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SendSMS(int vivraninumber, int busid)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
           

            var data = WayBillBAL.BALGetOwnerVivraniInfo(busid, vivraninumber);
            Helpers.SMSGateway.SendVivraniSMS(data.TotalAmount, data.Contact, data.OwnerName, data.Busnumber, data.TotalAmount, vivraninumber.ToString());
           // ViewBag.VivraniID = _vivraniid;
            return Json(true, JsonRequestBehavior.AllowGet);
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

            var data = WayBillBAL.BALCheckIfVivraniCreated(user.LoginEmpID, busid);

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
            var data = WayBillBAL.BALUpdateVivrani(vivraniid, amount);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GenerateCashSheet(int CashID)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            var empid = user.LoginEmpID;
          var data=  BusinessAccessLayer.WayBillBAL.BALInsertChashMaster(empid, CashID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GamanPatra()
        {

            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();

            GamanViewModel gamanmodel = new GamanViewModel
            {

                bus = new SelectList(bus, "BusID", "BusNumber"),
                dipo = new SelectList(dipotype.DipoList, "DipoID", "DipoName"),
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
            var data = BusinessAccessLayer.WayBillBAL.BALGetDistanceFare(from, to);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AutoComplete(string prefix)
        {
            var data = BusinessAccessLayer.WayBillBAL.BALGetRouteName(prefix);
            //Searching records from list using LINQ query  
            var CityName = (from N in data
                            where N.Route.StartsWith(prefix)
                            select new
                            {
                                label = N.Route,
                                val = N.RouteID
                            });
            return Json(CityName, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public JsonResult SaveData(EditWayBillData model)
        {
            int index = 0;
            String result = String.Empty;

            if (BusinessAccessLayer.WayBillBAL.BALUpdateWayBillAmount(model.VivraniID, model.Amount, model.roundOff,model.vivraninumber) == -1)
            {
                result = "1";

            }
            else
            {
                result = "0";
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public  ActionResult DeleteData(string vivranid)
        {
            int vivraniid = Convert.ToInt32(vivranid);
            return Json(WayBillBAL.BALDeleteData(vivraniid), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateWayBill()
        {

            return PartialView(@"~/Views\\WayBill\UpdateWayBill.cshtml");

        }
        public ActionResult UpdateWayBillDetails()
        {

            return PartialView(@"~/Views\\WayBill\_UpdateWaybillDetails.cshtml");

        }


        [HttpGet]
        public ActionResult GetVivrani(int vivid)
        {
            var result = WayBillBAL.BALGetAllVivraniEdit(vivid);
            ViewBag.Buses = BusinessAccessLayer.BALFuel.BALGetBuses();
            return PartialView(@"~/Views\\WayBill\_TempUpdte.cshtml", result);
            //return PartialView(@"~/Views\\WayBill\EditWayBill.cshtml", result);
        }
        [HttpGet]
        public ActionResult GetWayBillEditDetails(int waybill, int waybillserial)
        {
            var result = WayBillBAL.BALGetAllWaybillEdit(waybill, waybillserial);
            return PartialView(@"~/Views\\WayBill\EditWayBillDetails.cshtml", result);
        }
        [HttpPost]
        public ActionResult SaveTranshipmentDetails(Transhipment model)
        {
           
            model.InsertedBy = 18;
            var data = WayBillBAL.BALSaveTranshipment(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class Student
        {
            public int ID { get; set; }
            public String Name { get; set; }
            public String Demo { get; set; }
        }
        public class EditWayBillData
        {
            public int VivraniID { get; set; }
            public decimal Amount { get; set; }
            public decimal roundOff { get; set; }
            public int vivraninumber { get; set; }
        }
    }
}



