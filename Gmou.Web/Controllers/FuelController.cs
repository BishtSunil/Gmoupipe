﻿using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class FuelController : Controller
    {
        //
        // GET: /Fuel/
       
        public ActionResult Index()
        {
            return PartialView(@"~/Views\Fuel\_FuelDetail.cshtml");
        }
        public ActionResult Chart()
        {
            return PartialView(@"~/Views\Fuel\_sampleChart.cshtml");
        }

        
        
        [HttpGet]
            public ActionResult ShowChart()
            {
                var data = BusinessAccessLayer.BALSupport.GetCashSheetChart();
                var chartData = data
    
    .Select(a => new
    {
      Dates = a.Dates.ToShortDateString(),
      Amount = a.Amount
    });
                return Json(chartData,JsonRequestBehavior.AllowGet);
                }

        public ActionResult FuelCashMemoEntry()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
          
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
              //  busnumber=  new SelectList(bus, "BusID", "BusNumber")
            };

            return PartialView(@"~/Views\Fuel\_fuelentry.cshtml",vivraniviewmodel);
        }
        public ActionResult FuelChitEntry()
        {
         string username=   ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Username;
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
              var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
         var stationdetails=   BusinessAccessLayer.BALSupport.BALGetFuelStationNo(username);
            ViewBag.StationName = stationdetails.StationName;
            ViewBag.StationID= stationdetails.StationID;
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                 busnumber=  new SelectList(bus, "BusID", "BusNumber")
            };

            return PartialView(@"~/Views\Fuel\_fuelchitentry.cshtml", vivraniviewmodel);
        }
        [HttpPost]
        public ActionResult SaveCashMemoDetails(FuelCashOtherMemo model)
        {
            var data = BusinessAccessLayer.BALFuel.BALSaveCashMemoDetails(model);
            return Json(data, JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public ActionResult GetFuelPrice(FuelPriceModel model)
        {
            string username = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Username;

            var stationdetails = BusinessAccessLayer.BALSupport.BALGetFuelStationNo(username);
            var data = BusinessAccessLayer.BALFuel.BALGetFuelPrice(stationdetails.StationID, model.fuelid);
          
            return Json(data, JsonRequestBehavior.AllowGet);
        }
  
        public ActionResult FuelPriceEntry()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName")
            };

            return PartialView(@"~/Views\Fuel\_fuelPrice.cshtml", vivraniviewmodel);
        }
              [HttpGet]
        public ActionResult GetAllFuelRateList()
        {

            var data = BusinessAccessLayer.BALFuel.BALGetAllFuelRate();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveFuelDetails(FuelPriceListModel model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.UpdatedBy = user.LoginEmpID;
            var data = BusinessAccessLayer.BALFuel.BALSaveFuelDetails(model);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFuelDetails(UpdateFuelModel model)
        {
             
            var data = BusinessAccessLayer.BALFuel.BALUpdatefuelDetail(model);

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SaveChitFuelDetails(ChitFuel data)
        {

            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            data.InsertedBy = user.LoginEmpID;
            data.date= Helpers.GMOUHelper.ConvertTOIST(data.date);
            if (!BusinessAccessLayer.BALFuel.BALCheckDuplicate(data.DieselBookno, data.ChitNo, Convert.ToInt32(data.VechileNumber)))
            {
                var data_ = BusinessAccessLayer.BALFuel.BALInsertChit(data);
                var info = BusinessAccessLayer.BALFuel.BALGetOwnerFueliInfo(data.VechileNumber, data.FuelStationID, data.listFuel.Select(m => m.Fueltype).FirstOrDefault());
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
            // Helpers.SMSGateway.SendFuelSMS()
            //    string message =String.Format("Fuel Filled by your Vechile Book/Chit{0}-{1}. Quantity:{2} Price{3} at Kotdwar IOC on {4}",model.DieselBookno,model.ChitNo,model.FuelQuantity,model.Price,DateTime.Now.ToString());
            //    Helpers.SMSGateway.SendSMS(message);
            //}


        }
        [HttpPost]
        public ActionResult saveFuelDetail1(ChitFuelModelInsert fuelobj)
        {
            if (!BusinessAccessLayer.BALFuel.BALCheckDuplicate(fuelobj.DieselBookno, fuelobj.ChitNo, Convert.ToInt32(fuelobj.VechileNumber)))
            {
                fuelobj.date = Helpers.GMOUHelper.ConvertTOIST(fuelobj.date);
                var data_ = BusinessAccessLayer.BALFuel.BALInsertChitByVivrani(fuelobj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult GetAllChitDetailsByUser(int usertype)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
          
            var data = BusinessAccessLayer.BALFuel.BALGetAllChitDetailsByUser(user.LoginEmpID, usertype);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllChitDetailsByBus()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;

            var data = BusinessAccessLayer.BALFuel.BALGetAllChitDetailsByUser(user.LoginEmpID, 1);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FuelRecievingEnrty()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName")
            };

            return PartialView(@"~/Views\Fuel\_fuelOpeningEntry.cshtml", vivraniviewmodel);
        }
        [HttpPost]
        public ActionResult SaveFuelRecievingDetails(FuelRecieve model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;

            var data = BusinessAccessLayer.BALFuel.BALInsertReceivingChit(model);

            return Json(data, JsonRequestBehavior.AllowGet);
            

        }

        public ActionResult Stock()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName")
            };

            return PartialView(@"~/Views\Fuel\_openingclosingstock.cshtml", vivraniviewmodel);

        }
        [HttpGet]
        public ActionResult GetStock(int pumpid)
        {
            var data = BusinessAccessLayer.BALFuel.BALGetStockData(pumpid);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DailySummary()
        {
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var data = BusinessAccessLayer.BALFuel.BALGetChitFuelSalesDetails(1);
            data.fuelpump = new SelectList(pumpfule, "StationID", "StationName");
            return PartialView(@"~/Views\Fuel\_dailysummary.cshtml", data);

        }
        [HttpGet]
        public ActionResult ShowDailySummary()
        {
          
            var data = BusinessAccessLayer.BALFuel.BALGetChitFuelSalesDetails(1);
          
            return PartialView(@"~/Views\Fuel\_showsummary.cshtml", data);

        }
        [HttpPost]
        public ActionResult GetCashDetails(CashFuleSale model)
        {
            var data = BusinessAccessLayer.BALFuel.BALGetCashFuelSalesDetails(model);

            return Json(data, JsonRequestBehavior.AllowGet);

            
        }
        [HttpPost]
        public ActionResult SaveDailySummary(FinalSummaryData model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.inserted_by = user.LoginEmpID;
            BusinessAccessLayer.BALFuel.BALInsertDailySummary(Helpers.Converter.ConvertFinalSummary(model));
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDailySummaryReport(int pumpid)
        {
            var data = BusinessAccessLayer.BALFuel.BALGetFuelFinalSumamry();
            var result = BusinessAccessLayer.BALFuel.BALGetOtherStocksDetailsSummary(1);
            SummaryStockFinalReports suumaryreport = new SummaryStockFinalReports()
            {
                stocks = data, 
                srs = result
        };
            return View(@"~/Views\Fuel\FuelFinalReport.cshtml", suumaryreport);
        }

        public ActionResult GetStocksOtherDetails(int pumpid)
        {
            return null;

        }
         public ActionResult StaffFuelEnrty()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName")
            };

            return PartialView(@"~/Views\Fuel\_fuelstaffentry.cshtml", vivraniviewmodel);
        }
         public ActionResult OtherFuelEnrty()
         {
             var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
             var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
             FuelViewModel vivraniviewmodel = new FuelViewModel
             {
                 fueltype = new SelectList(data, "FuelType", "FuelName"),
                 fuelpump = new SelectList(pumpfule, "StationID", "StationName")
             };

             return PartialView(@"~/Views\Fuel\_otherfuelentry.cshtml", vivraniviewmodel);
         }
        [HttpPost]
         public ActionResult SaveOtherFuelDetails(OtherFuelModel model)
         {

             var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
             model.InsertedBy = user.LoginEmpID;
             var data = BusinessAccessLayer.BALFuel.BALInsertOtherFule(model);

             return Json(data, JsonRequestBehavior.AllowGet);
         }
         public ActionResult SaveStaffFuelDetails(staffFuel model)
         {

             var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
             model.InsertedBy = user.LoginEmpID;
             var data = BusinessAccessLayer.BALFuel.BALInsertStaffFule(model);

             return Json(data, JsonRequestBehavior.AllowGet);
         }
        [HttpGet]
         public ActionResult GetRecievingFuelDetails(int pumpid)
         {


             var data = BusinessAccessLayer.BALFuel.BALGetRecievingFuelDetails(pumpid);

             return Json(data, JsonRequestBehavior.AllowGet);
         }
        [HttpGet]
        public ActionResult GetOtherFuelDetails()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            int userid= user.LoginEmpID;

            var data = BusinessAccessLayer.BALFuel.BALGetOtherFuelDetails(userid);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

      public ActionResult EditFuelData()



        {
            string username = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Username;
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            var stationdetails = BusinessAccessLayer.BALSupport.BALGetFuelStationNo(username);
            ViewBag.StationName = stationdetails.StationName;
            ViewBag.StationID = stationdetails.StationID;
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return View(vivraniviewmodel);
        }
        public ActionResult EditFuel()
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            int userid = user.LoginEmpID;

            var data = BusinessAccessLayer.BALFuel.BALGetOtherFuelDetails(userid);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFuelEdit(EditFuelModelViewModel model)
        {
            string username = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Username;
          
            var stationdetails = BusinessAccessLayer.BALSupport.BALGetFuelStationNo(username);
          
            model.StationFuleID = stationdetails.StationID;

         var data =   BusinessAccessLayer.BALFuel.BALGetFuelEdit(model);
            return PartialView(@"~/Views\Fuel\Partial1.cshtml", data);
        }

        [HttpPost]
        public ActionResult SaveEditFuel(UpdateEditFuelModel model)
        {
            string username = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Username;

            var stationdetails = BusinessAccessLayer.BALSupport.BALGetFuelStationNo(username);

            model.StationID = stationdetails.StationID;

            BusinessAccessLayer.BALFuel.BALUpdateFuelEdit(model);
            return Json(true, JsonRequestBehavior.AllowGet);
            
                  
        }
        
    }
}
