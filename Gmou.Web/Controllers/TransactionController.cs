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
        public ActionResult GetViraniList(int busid, DateTime date)
        {
            var data = BusinessAccessLayer.BALReports.BALGetViraniList(busid, date);
            
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

        public ActionResult MonthlySummary( )
        {
        

            var bus = BusinessAccessLayer.BALFuel.BALGetBuses();
            //var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            GamanViewModel model = new GamanViewModel
            {
                bus=new SelectList(bus, "BusID", "BusNumber")

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
                obj= BusinessAccessLayer.BALSupport.BALGetVivraniReports(id, date);
             
               // monthlysummary.monthlyreport.VivraniSum = monthlysummary.monthlyreport.lstVivraniReports.Sum(m => m.Amount);
                return PartialView(@"~/Views\\Reports\_MainSummary.cshtml", obj);


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
          var  obj = BusinessAccessLayer.BALFuel.BALGetAllChitDetails(date, pumpid);

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

         var data =    BusinessAccessLayer.BALReports.BALGetBusDebitStatus();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
