//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gmou.DataRepository.EntityRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBusDetail
    {
        public int busdetailid { get; set; }
        public string bus_number { get; set; }
        public int bus_operating_center { get; set; }
        public System.DateTime registration_date { get; set; }
        public string permit_number { get; set; }
        public string model { get; set; }
        public int seats { get; set; }
        public string chesis_number { get; set; }
        public string engine_number { get; set; }
        public string road_tax { get; set; }
        public string fitness { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public string last_modified_by { get; set; }
        public System.DateTime last_modified_date { get; set; }
        public int bus_id { get; set; }
        public string bus_registration_type { get; set; }
        public string bus_registration_number { get; set; }
    
        public virtual tblBu tblBu { get; set; }
    }
}