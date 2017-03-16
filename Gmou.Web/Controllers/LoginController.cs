using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gmou.Web.Models;
using Gmou.Web.Helpers;
using UserValidation;
using System.Web.Configuration;
using Gmou.DomainModelEntities;

namespace Gmou.Web.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel logon)
        {
            string status = "The username or password provided is incorrect.";

            // Verify the fields.
            if (ModelState.IsValid)
            {
              var result=  Gmou.Web.Helpers.Converter.ModelConverter<LoginViewModel, LoginUser>(logon);
                // Authenticate the user.
              if (UserManager.ValidateUser(result, Response))
                {
                    // Redirect to the secure area.
                    if (string.IsNullOrWhiteSpace(logon.RedirectUrl))
                    {
                        logon.RedirectUrl = "/";
                    }

                    status = "OK";
                }
            }

          return   RedirectToActionPermanent("Index","Admin");
        }
        
        public ActionResult Logout()
        {
            // Clear the user session and forms auth ticket.
            UserManager.Logoff(Session, Response);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Admin()
        {
            return View();

        }
    }
}
