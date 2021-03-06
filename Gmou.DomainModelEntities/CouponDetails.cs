﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
    public class CouponDetails
    {

        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public string Distance { get; set; }
        public string Page { get; set; }
        public decimal Fare { get; set; }
        public int InsertedBy { get; set; }
        public int WayBillBookNo { get; set; }
        public int WayBillSerialNo { get; set; }
        public decimal TotalAmount { get; set; }
        public int CouponBookNo { get; set; }
        public int CouponSerialNo { get; set; }
        public DateTime Validity { get; set; }
        public List<CouponList> lstCoupon { get; set; }

        public CouponDetails() { }
        public CouponDetails(string stationfrom, string stationto, string distance, string page, decimal fare, DateTime validity)
        {
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.Distance = distance;
            this.Page = page;
            this.Fare = fare;
            this.Validity = validity;
        }
        public CouponDetails(string stationfrom, string stationto, string distance, decimal fare)
        {
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.Distance = distance;
            this.Fare = fare;

        }
        public CouponDetails( decimal totalAmount,int couponbookno, int couponserialno, int distance, decimal fare,string stationfrom, string stationto)
        {
            this.CouponBookNo = couponbookno;
            this.CouponSerialNo = couponserialno;
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.Distance =  distance.ToString();
            this.Fare = fare;
            this.TotalAmount = totalAmount+fare;

        }
    }

    public class CouponList
    {

        public int StationID { get; set; }
        public string Page { get; set; }
        
        public  CouponList(int stationid, string page)
        {
            this.StationID = stationid;
            this.Page = page;
        }
        
    }

    public class Transhipment
    {

        public int BusFrom { get; set; }
        public int BusTo { get; set; }
        public int WaybillFrom { get; set; }
        public int waybillserialfrom { get; set; }
        public decimal Amountfrom { get; set; }
        public int WaybilTo { get; set; }
        public int waybillserialTo { get; set; }
        public decimal AmountTo { get; set; }

        public int InsertedBy { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
