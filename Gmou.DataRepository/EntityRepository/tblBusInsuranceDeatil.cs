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
    
    public partial class tblBusInsuranceDeatil
    {
        public int insurance_details_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public System.DateTime validity { get; set; }
        public int bus_id { get; set; }
    
        public virtual tblBu tblBu { get; set; }
    }
}