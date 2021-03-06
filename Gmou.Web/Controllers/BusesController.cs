﻿using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class BusesController : Controller
    {
        //
        // GET: /Buses/

        public ActionResult Index()
        {
            return PartialView(@"~/Views\Admin\Partial\_getAllBuses.cshtml");
        }
        public ActionResult BusInformation()
        {
           var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
           DipoViewModel dipo = new DipoViewModel();
           dipo.DipoList = new SelectList(dipotype.DipoList, "DipoID", "DipoName");
          // bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");
            return PartialView(@"~/Views\Admin\Partial\_busInformation.cshtml",dipo);
        }
        [HttpPost]
        public ActionResult getAllBuses()
        {
            var lstBuses = Gmou.BusinessAccessLayer.BusBAL.GetAllDetails();
            return Json(lstBuses, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetAllBusesBySearch(string condition, string type)
        {
            var lstBuses = Gmou.BusinessAccessLayer.BusBAL.GetAllBusesBySearch(condition,type);
            return Json(lstBuses, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SaveBusDetail(SaveBusModel obj)
        {
            if (obj.busId > 0)
            {
                Gmou.BusinessAccessLayer.BusBAL.UpdateBusDeatils(obj);
                return Json(new
                {
                    statusCode = 200,
                    status = "Save Sucessfully"
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                if (!BusinessAccessLayer.BusBAL.CheckIfBusAvailable(obj.bus_number))
                {
                    if (BusinessAccessLayer.BusBAL.AddBusRecords(obj))
                    {
                        return Json(new
                        {
                            statusCode = 200,
                            status = "Save Sucessfully"
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new
                          {
                              statusCode = 500,
                              status = "Some Error",

                          }, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {
                    return Json(new
                    {
                        statusCode = 404,
                        status = "Bus is already  there",

                    }, JsonRequestBehavior.AllowGet);

                }

            }
        }



        public ActionResult SearchBus(string busNumber)
        {
            ViewBag.busNumber = Encrypt.decryptMethod(busNumber);
            return PartialView(@"~/Views\Admin\Partial\_searchBus.cshtml");
        }


        public ActionResult DifferentSearchBus(string busNumber)
        {

            return PartialView(@"~/Views\Admin\Partial\_getBusesBySearch.cshtml");
        }
        [HttpGet]
        public ActionResult GetBusDetails(string busnumber)
        {
            var result= BusinessAccessLayer.BusBAL.GetBusDetailsByNumber(busnumber);
            if (result == null)
            {
                return Json(new
                {
                    statusCode = 404,
                    status = "No Record Found, Please Enter Valid Entry"
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditBusDetail(string busId)
        {
            ViewBag.busId = Encrypt.decryptMethod(busId);
            var dipotype = BusinessAccessLayer.BusBAL.BALGetAllDipo();
            DipoViewModel dipo = new DipoViewModel();
            dipo.DipoList = new SelectList(dipotype.DipoList, "DipoID", "DipoName");
            // bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");
            return PartialView(@"~/Views\Admin\Partial\_busInformation.cshtml", dipo);
        }


        public ActionResult EditBusDetails(string busID)
        {
            var busId = Convert.ToInt32(busID);
            var result = BusinessAccessLayer.BusBAL.EditBusDetails(busId);
            if (result == null)
            {
                return Json(new
                {
                    statusCode = 404,
                    status = "No Record Found, Please Enter Valid Entry"
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
             
        }

        public ActionResult UplodMultiple()
        {
            return PartialView(@"~/Views\Admin\Partial\_UplodMultiple.cshtml");
        }
        [HttpGet]
        public ActionResult Deletebus(string busId)
        {
           var data = Encrypt.decryptMethod(busId);
           var busID = Convert.ToInt32(data);
            BusinessAccessLayer.BusBAL.DeleteBus(busID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BusDepoStatus()
        {
            return null;
         
        }
       
    }
}
