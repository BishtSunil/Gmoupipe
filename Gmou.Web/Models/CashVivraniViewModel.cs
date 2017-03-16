using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Models
{
    public class CashVivraniViewModel
    {
        public int UserID { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSubmitted { get; set; }

    }
    public class VivraniViewModel
    {

        public SelectList bus { get; set; }
        public SelectList busowner { get; set; }
        public SelectList fuelpump { get; set; }
        public SelectList fueltype { get; set; }
        public int VivraniSerialNumber { get; set; }
        public int busId { get; set; }
        public int WaybillNo { get; set; }
    }
    public class GamanViewModel
    {

        public SelectList bus { get; set; }
        public SelectList dipo { get; set; }
        public int GamanPatraSerialNumber { get; set; }
       
    }

    public class MonthlySummaryViewModel
    {

        
        public MontlyBusReport monthlyreport { get; set; }

    }

}