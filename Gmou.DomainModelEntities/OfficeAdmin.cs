using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
  public  class OfficeAdvanceAdmin
    {
        public DateTime InsertDate { get; set; }
        public decimal Amount { get; set; }
        public int BusNumber { get; set; }
        public String Reason { get; set; }
        public int InsertedBy { get; set; }

    }
   public class OfficeAdvanceAdminModel
    {
        public DateTime InsertDate { get; set; }
        public decimal Amount { get; set; }
        public string BusNumber { get; set; }
        public String Reason { get; set; }


        public OfficeAdvanceAdminModel (DateTime date, decimal amount, string  busnumber, string reason )
        {
            this.InsertDate = date;
            this.Amount = amount;
                this.BusNumber=busnumber;
            this.Reason=reason;
        }
    }
}
