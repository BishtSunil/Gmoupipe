using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Models
{
    public class FuelViewModel
    {
            public SelectList fuelpump { get; set; }
            public SelectList fueltype { get; set; }
            public SelectList busnumber { get; set; }
    }
    public class DipoViewModel
    {

        public SelectList DipoList { get; set; }
    }

    public class FinalSummaryData
    {

        public int pumpid { get; set; }
        public Nullable<long> smeter_petrol { get; set; }
        public Nullable<long> emeter_petrol { get; set; }
        public Nullable<decimal> ownersale_petrol { get; set; }
        public Nullable<decimal> cashsale_petrol { get; set; }
        public Nullable<decimal> staffsale_petrol { get; set; }
        public Nullable<decimal> othersale_petrol { get; set; }
        public Nullable<decimal> ownerquanity_petrol { get; set; }
        public Nullable<decimal> cashquanity_petrol { get; set; }
        public Nullable<decimal> staffquanity_petrol { get; set; }
        public Nullable<decimal> otherquanity_petrol { get; set; }
        public Nullable<long> smeter_diesel { get; set; }
        public Nullable<long> emeter_diesel { get; set; }
        public Nullable<decimal> ownersale_diesel { get; set; }
        public Nullable<decimal> cashsale_diesel { get; set; }
        public Nullable<decimal> staffsale_diesel { get; set; }
        public Nullable<decimal> othersale_diesel { get; set; }
        public Nullable<decimal> ownerquanity_diesel { get; set; }
        public Nullable<decimal> cashquanity_diesel { get; set; }
        public Nullable<decimal> staffquanity_diesel { get; set; }
        public Nullable<decimal> otherquanity_diesel { get; set; }
        public Nullable<decimal> ownersale_lub { get; set; }
        public Nullable<decimal> cashsale_lub { get; set; }
        public Nullable<decimal> staffsale_lub { get; set; }
        public Nullable<decimal> othersale_lub { get; set; }
        public Nullable<decimal> ownerquanity_lub { get; set; }
        public Nullable<decimal> cashquanity_lub { get; set; }
        public Nullable<decimal> staffquanity_lub { get; set; }
        public Nullable<decimal> otherquanity_lub { get; set; }
        public Nullable<System.DateTime> summary_date { get; set; }
        public Nullable<int> inserted_by { get; set; }
    }

    public class SummaryStockFinalReports
    {
       
        public List<FuelStockReport> stocks { get; set; }
        public SaleReportFinal srs { get; set; }

    }
}