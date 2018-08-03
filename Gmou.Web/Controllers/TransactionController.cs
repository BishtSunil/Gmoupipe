using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BusTransaction()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };

            return PartialView(@"~/Views\Reports\_busTransationReport.cshtml", vivraniviewmodel);

        }
        [HttpGet]
        public ActionResult GetTransactionDetails(int pumpid)
        {

            var data = BusinessAccessLayer.BALReports.BALGetBusTransactionDetails(pumpid);
            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult VivraniDetails()
        {

            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };

            return PartialView(@"~/Views\Reports\_vivranidetails.cshtml", vivraniviewmodel);

        }
        [HttpGet]
        public ActionResult GetViraniList(int busid, DateTime date, DateTime enddate)
        {
            var data = BusinessAccessLayer.BALReports.BALGetViraniList(busid, date, enddate);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult BusStations()
        {
            var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            DipoViewModel dipo = new DipoViewModel();
            dipo.DipoList = new SelectList(dipotype.DipoList, "DipoID", "DipoName");
            // bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");
            return PartialView(@"~/Views\Reports\_busStation.cshtml", dipo);
        }
        [AllowAnonymous]
        public ActionResult MonthlySummary()
        {


            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            //var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            GamanViewModel model = new GamanViewModel
            {
                bus = new SelectList(bus, "BusID", "BusNumber")

            };


            return PartialView(@"~/Views\\Reports\MonthlySummary.cshtml", model);
        }

        public ActionResult FinalEntry()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return PartialView(@"~/Views\Reports\_finalentry.cshtml", vivraniviewmodel);
        }

        public ActionResult SaveFinalEntry()
        {
            return null;

        }
        [HttpGet]
   
        public ActionResult ShowMonthlySummary(int id, DateTime date)

        {
            try
            {
                MonthlySummaryViewModel monthlysummary = new MonthlySummaryViewModel();
                MontlyBusReport obj = new MontlyBusReport();
                obj = BusinessAccessLayer.BALSupport.BALGetVivraniReports(id, date);

                // monthlysummary.monthlyreport.VivraniSum = monthlysummary.monthlyreport.lstVivraniReports.Sum(m => m.Amount);
                return PartialView(@"~/Views\\Reports\_MainSummary.cshtml", obj);


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowMonthlySummaryCustomer(int id, DateTime date)

        {
            try
            {
                MonthlySummaryViewModel monthlysummary = new MonthlySummaryViewModel();
                MontlyBusReport obj = new MontlyBusReport();
                obj = BusinessAccessLayer.BALSupport.BALGetVivraniReports(id, date);

                // monthlysummary.monthlyreport.VivraniSum = monthlysummary.monthlyreport.lstVivraniReports.Sum(m => m.Amount);
                return PartialView(@"~/Views\\Reports\_FianlShowResult.cshtml", obj);


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public ActionResult ShowPumpReport()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return PartialView(@"~/Views\Reports\_FuleSaleReport.cshtml", vivraniviewmodel);

        }

        [HttpGet]
        public ActionResult getPumpReport(DateTime date, int pumpid)
        {
            var obj = BusinessAccessLayer.BALFuel.BALGetAllChitDetails(date, pumpid);

            // monthlysummary.monthlyreport.VivraniSum = monthlysummary.monthlyreport.lstVivraniReports.Sum(m => m.Amount);
            return Json(obj, JsonRequestBehavior.AllowGet);


        }
        public ActionResult ShowPumpStockReport()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };
            return PartialView(@"~/Views\Reports\_stocksReports.cshtml", vivraniviewmodel);

        }

        [HttpGet]
        public ActionResult getPumpstockReport(int pumpid, DateTime date)
        {
            var obj = BusinessAccessLayer.BALReports.BALGetStockReportsList(pumpid, date);

            // monthlysummary.monthlyreport.VivraniSum = monthlysummary.monthlyreport.lstVivraniReports.Sum(m => m.Amount);
            return Json(obj, JsonRequestBehavior.AllowGet);


        }



        public ActionResult DebitStatus()
        {

            // 

            return PartialView(@"~/Views\Transaction\DebitStatus.cshtml");
        }
        [HttpGet]
        public ActionResult GetDebitStatus(DateTime stdate, DateTime enddate)
        {

            var data = BusinessAccessLayer.BALReports.BALGetBusDebitStatus();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllVivrani()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetViraniAll(DateTime date, DateTime enddate)
        {
            var data = BusinessAccessLayer.BALReports.BALGetViraniListAll(date, enddate);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FuelReportsByDate()
        {
            return View(@"~/Views\Reports\_fuelReportsByDate.cshtml");
        }
       
        [HttpGet]
        public ActionResult GetFuelReportsByDate(DateTime  sdate, DateTime edate)
        {
          var data= BusinessAccessLayer.BALReports.BALGetFuelReportDate(sdate, edate);
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult BusPerformance()
        {
            return View(@"~/Views\Reports\_busPerformance.cshtml");
        }

        [HttpPost]
        public ActionResult GetBusPerformance(PerformanceModel performancemodel)
        {
            var data = BusinessAccessLayer.BALReports.BALDALGetBusPerformance(performancemodel.sdate, performancemodel.order,performancemodel.range);
            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult RoadWarrent()
        {
            var data = BusinessAccessLayer.BALFuel.BALGetAllFuel();
            var pumpfule = BusinessAccessLayer.BALFuel.BALGetAllStation();
            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            FuelViewModel vivraniviewmodel = new FuelViewModel
            {
                fueltype = new SelectList(data, "FuelType", "FuelName"),
                fuelpump = new SelectList(pumpfule, "StationID", "StationName"),
                busnumber = new SelectList(bus, "BusID", "BusNumber")
            };

          //  return PartialView(@"~/Views\Fuel\_fuelchitentry.cshtml", vivraniviewmodel);
            return View(@"~/Views\Transaction\RoadWarrent.cshtml", vivraniviewmodel);
        }

    [HttpPost]

    public ActionResult SaveRoadWarrent(int busid, decimal amount)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
          
            Gmou.DomainModelEntities.RoadWarrent rwarrent = new RoadWarrent();
            rwarrent.BusNumber = busid;
            rwarrent.Amount = amount;
            rwarrent.InsertedBy = user.LoginEmpID;
            rwarrent.InsertDate = DateTime.Now.Date;

        var data =     BusinessAccessLayer.BALReports.BALInsertRaodWarrent(rwarrent);

            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}
