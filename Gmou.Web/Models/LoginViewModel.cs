﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmou.Web.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RedirectUrl { get; set; }
    }
}