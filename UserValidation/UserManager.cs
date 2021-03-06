﻿using Gmou.DomainModelEntities;
using Gmou.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace UserValidation
{
   public  class UserManager
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return ((CustomPrincipal)(HttpContext.Current.User)).User;
                }
                else if (HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (User)HttpContext.Current.Items["User"];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Authenticates a user against a database, web service, etc.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User</returns>
        public static User AuthenticateUser(string username, string password)
        {
            User user = null;
        var result =     EmployeeRepository.GetLogin(username, password).ToList().FirstOrDefault();
        
            if(result!=null)
            {
                if(result.UserName==username && result.Password==password)
          {
              user = new User { LoginEmpID = result.EmployeeLoginID, Name = result.UserName, Username = result.UserName ,Role=result.Role,DepartmentID=result.DepartmentID};
              return user;

          }
        }

          return null;
           
        }

        /// <summary>
        /// Authenticates a user via the MembershipProvider and creates the associated forms authentication ticket.
        /// </summary>
        /// <param name="logon">Logon</param>
        /// <param name="response">HttpResponseBase</param>
        /// <returns>bool</returns>
        public static bool ValidateUser(LoginUser logon, HttpResponseBase response)
        {
            bool result = false;

            if (Membership.ValidateUser(logon.UserName, logon.Password))
            {
                // Create the authentication ticket with custom user data.
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(UserManager.User);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        logon.UserName,
                        DateTime.Now,
                        DateTime.Now.AddDays(30),
                        true,
                        userData,
                        FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                result = true;
            }

            return result;
        }

        /// <summary>
        /// Clears the user session, clears the forms auth ticket, expires the forms auth cookie.
        /// </summary>
        /// <param name="session">HttpSessionStateBase</param>
        /// <param name="response">HttpResponseBase</param>
        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            response.Cookies.Add(cookie);
        }
 
    }
}
