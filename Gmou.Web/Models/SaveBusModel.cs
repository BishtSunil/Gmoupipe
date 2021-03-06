﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;


namespace Gmou.Web.Models
{
    public class BusOwnerViewModel
    {
        public SelectList busownername { get; set; }
    }
    public class SaveBusViewModel

    {

        public string bus_number { get; set; }
        public int setid { get; set; }
        public string bus_operating_center { get; set; }
        public System.DateTime registration_date { get; set; }
        public string permit_number { get; set; }
        public string model { get; set; }
        
        public int seats { get; set; }
        public string registration_number { get; set; }
        public string chesis_number { get; set; }
        public string engine_number { get; set; }
        public string road_tax { get; set; }
        public string fitness { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public string last_modified_by { get; set; }
        public System.DateTime last_modified_date { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public System.DateTime validity { get; set; }
        public string bus_owner_name { get; set; }
        public string owner_father_name { get; set; }
        public string owner_address { get; set; }
        public Nullable<long> owner_contact_number { get; set; }
        public int bus_id { get; set; }
        public int entry_amount { get; set; }
        public string entry_reciept_number { get; set; }
        public Nullable<System.DateTime> payment_date { get; set; }
        public int security_amount { get; set; }
        public string security_money_reciept { get; set; }
        public Nullable<int> remaining_amount { get; set; }
        public string emi { get; set; }
        public string gauranter1 { get; set; }
        public string gauranter1_bus { get; set; }
        public string gauranter2 { get; set; }
        public string gauranter2_bus { get; set; }
        public string Insorance { get; set; }
    }
}