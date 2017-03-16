using Gmou.BusinessAccessLayer;
using Gmou.DomainModelEntities.StoreBook;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class BookStoreController : Controller
    {
        //
        // GET: /BookStore/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChitBookView()
        {

            var result = WayBillBAL.GetAllBusses();
            BusOwnerViewModel bo = new BusOwnerViewModel();

            bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");


            return PartialView(@"~/Views\BookStore\_chitBookEntry.cshtml", bo);
        }
        [HttpPost]
        public ActionResult SaveDieselBookDetails(ChitBookModel model)
        {
            var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            model.IssuedBy = user.LoginEmpID;

           var data= BALBookStore.BALInsertChitBook(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SaveDieselBookDetails()
        {
           
            var data = BALBookStore.BALGetAllChitBookDetails();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
