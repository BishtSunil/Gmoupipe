﻿
using Gmou.BusinessAccessLayer;
using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User.Role == "Traffic")
            {
               return RedirectToActionPermanent("Index", "Waybill");
            }
            
                var input = BusinessAccessLayer.BusBAL.GetInsuranceDatesNear();
                return View(@"~/Views\Admin\Partial\_admin.cshtml", input);
           
        }
    
        [HttpGet]
        public ActionResult GetInsuranceNearDate()
        {
            var input = BusinessAccessLayer.BusBAL.GetInsuranceDatesNear();
            return Json(input, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult employee()
        {
            return PartialView(@"~/Views\Admin\Partial\_getAllEmployee.cshtml");
        }
        public ActionResult ShowEmployee()
        {
            var lstEmployee = Gmou.BusinessAccessLayer.EmployeeBAL.GetAllEmployeelist();
           // ViewBag.empList = lstEmployee;
            return Json(lstEmployee,JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult CreateEmployee()
        {
            Gmou.Web.Models.EmployeeViewModel employeemodel = new Models.EmployeeViewModel
            {
                lstDepartment = new SelectList(Gmou.BusinessAccessLayer.EmployeeBAL.GetDepartmentList(), "DepartmentID", "DepartmentName"),
                lstOffices = new SelectList(Gmou.BusinessAccessLayer.EmployeeBAL.GetOfficeList(), "OfficeAreaID", "OfficeAreaName")
            };
           
           
            return PartialView(@"~/Views\Admin\Partial\_createEmployee.cshtml",employeemodel);
        }

        public ActionResult EmployeeRegitration()
        {
            Gmou.Web.Models.EmployeeViewModel employeemodel = new Models.EmployeeViewModel
            {
                lstDepartment = new SelectList(Gmou.BusinessAccessLayer.EmployeeBAL.GetDepartmentList(), "DepartmentID", "DepartmentName"),
                 lststation  = new SelectList(Gmou.BusinessAccessLayer.BALFuel.BALGetAllStation(),"StationID","StationName")
            };


            return PartialView(@"~/Views\Admin\Partial\_employeeRegitration.cshtml", employeemodel);
        }

        public ActionResult EditEmployee(string empId)
        {
            ViewBag.empId = Encrypt.decryptMethod(empId);
            Gmou.Web.Models.EmployeeViewModel employeemodel = new Models.EmployeeViewModel
            {
                lstDepartment = new SelectList(Gmou.BusinessAccessLayer.EmployeeBAL.GetDepartmentList(), "DepartmentID", "DepartmentName"),
                lstOffices = new SelectList(Gmou.BusinessAccessLayer.EmployeeBAL.GetOfficeList(), "OfficeAreaID", "OfficeAreaName")
            };

            return PartialView(@"~/Views\Admin\Partial\_createEmployee.cshtml", employeemodel);
        }
        public ActionResult EditEmployeeDetail(string empId)
        {
            int id = Convert.ToInt32(empId);
            var employeeDetails = EmployeeBAL.EditEmployeeDetail(id);

            return Json(employeeDetails.ToList(), JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult SearchEmployee(string empId)
        {
            ViewBag.empId = empId;
            return PartialView(@"~/Views\Admin\Partial\_searchEmployee.cshtml");
        }
        [HttpGet]
        public ActionResult GetEmployeeDetails(string employeeID)
        {
             int id = Convert.ToInt32(employeeID);
            var employeeDetails = EmployeeBAL.GetEmployeeDetail(id);
            

          return Json(employeeDetails.ToList(), JsonRequestBehavior.AllowGet);
        }
        public FileContentResult DownloadAttachemnt(int EmpID)
        {
           // return File(attachment.FileContent, attachment.FileType, attachment.FileName);

            return null;
        }
        [HttpPost]
        public ActionResult SaveEmpolyee(SaveEmployeeModel saveEmployeeModel)
        {
           // int employeeID = Convert.ToInt32(id);
          
            if (saveEmployeeModel.EmployeeId == 0)
            {
                if( Session["Image"] as byte[]==null)
                {
                 
                    saveEmployeeModel.Image = ReadImageFile(   Server.MapPath(Url.Content("~/Images/No_Image.png")));
                }
                else
                {
                    saveEmployeeModel.Image = Session["Image"] as byte[];
                    saveEmployeeModel.Document = Session["Document"] as byte[];
                }
               
                var result = BusinessAccessLayer.EmployeeBAL.SaveEmployeeDetails(Helpers.Converter.ConvertToDomainModel(saveEmployeeModel), false, 0);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Session["Image"] as byte[] == null)
                {

                    saveEmployeeModel.Image = ReadImageFile(Server.MapPath(Url.Content("~/Images/No_Image.png")));
                }
                else
                {
                    saveEmployeeModel.Image = Session["Image"] as byte[];
                    saveEmployeeModel.Document = Session["Document"] as byte[];
                }
               
                var result = BusinessAccessLayer.EmployeeBAL.SaveEmployeeDetails(Helpers.Converter.ConvertToDomainModel(saveEmployeeModel), true, saveEmployeeModel.EmployeeId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        } 

        private void SaveImages(byte[] images)
        {

            System.IO.File.WriteAllBytes(@"C:\GMOUOnline\Gmou.Web\EmployeeEmages\demo.jpeg", images);
        }
        public ActionResult Upload()
        {
            
                return PartialView(@"~/Views\Admin\Partial\_uploadsingle.cshtml");
            

        }
        [HttpPost]
        public void Upload(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                byte[] FileByteArray = new byte[uploadedFile.ContentLength];
                uploadedFile.InputStream.Read(FileByteArray, 0, uploadedFile.ContentLength);
                if(Session["Image"]==null)
                {
                Session["Image"] = FileByteArray;
                }
                else{
                    Session["Document"] = FileByteArray;
                }
              }
            
              
        }

        private static byte[] ReadImageFile(string imageLocation)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }

       public FileResult Download(string ImageName)
          {
           return File( ImageName, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
        [HttpPost]
        public ActionResult SaveEmpolyeeRegistartion(EmployeeRegistrationViewModel model)
       {
           string currentuser = HttpContext.Request.RequestContext.HttpContext.User.Identity.Name;
           var data = BusinessAccessLayer.EmployeeBAL.SaveEmployeeRegistration(Helpers.Converter.ConverToDomainModel(model), currentuser);
         return Json(data, JsonRequestBehavior.AllowGet);
       }
        [HttpGet]
        public ActionResult GetAllRegisteredEmployees()
        {
            var data = BusinessAccessLayer.EmployeeBAL.GetAllReegisteredEmployees();
           return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteRegisterdEmployee(int Id)
        {
            if (BusinessAccessLayer.EmployeeBAL.DeletedRegisteredEmployee(Id))
            {
                return RedirectToAction("EmployeeRegitration");
            }
            else
            {
                return Json(new { status = "failure" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult WayBillBookEntry()
        {
         var result=   WayBillBAL.GetAllBusses();
       BusOwnerViewModel bo =  new BusOwnerViewModel();

       bo.busownername = new SelectList(result.lstbusName, "BusID", "BusNumber");
           
        
            return PartialView(@"~/Views\Admin\Partial\_addWayBook.cshtml", bo);
        }

        [HttpPost]
        public ActionResult SaveWayBillBookDetails(WayBillBookDetailsModel model)
        {  var user = ((UserValidation.CustomPrincipal)(HttpContext.Request.RequestContext.HttpContext.User)).User;
            
            model.EmpoyeeID= user.DepartmentID;
            WayBillBAL.SaveWaybillDetails(model);
            return Json("sucess",JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllWayBillBookHistory()
        {

            var result = BusBAL.GetBusWayBillDetails();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
