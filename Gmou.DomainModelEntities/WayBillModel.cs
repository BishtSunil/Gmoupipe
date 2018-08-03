using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
    //@vechile_number int, 
    //@owner_name int,
    //@dipo nvarchar(50),
    //@jfrom nvarchar(50),
    //@jto nvarchar(50),
    //@waybill_no int,
    //@waybill_serial_no int,
    //@ticket_no int,
    //@ticket_serial int,
	
    //@fare money ,
    //@waybill_date date,
    //@emp_id int,
    //@reason nvarchar(500)
  public  class WayBillModel
    {
        public int BusNumber { get; set; }
        public int OwnerName { get; set; }
        public string Dipo { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }
        public int WayBillNo { get; set; }
        public int WayBillSerialNo { get; set; }
        public int TicketNo { get; set; }
        public int TickerSerialNo { get; set; }
        public decimal Fare { get; set; }
        public DateTime WayBillDate { get; set; }
        public int InsertedBy { get; set; }
        public string Reason { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfTicket { get; set; }
        public int TicketStart { get; set; }
        public int TicketEnd { get; set; }
        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public bool IsCoupon { get; set; }
        public int TotalSeats { get; set; }
        public WayBillModel() { }

    }

    public class WayBillTicketModel
    {
        public string BusNumber { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }
        public string WayBillNo { get; set; }
        public string WayBillSerialNo { get; set; }
       
        public decimal GamanPatar { get; set; }
        public decimal MiscMinus { get; set; }
        public decimal MiscPlus { get; set; }
        public int InsertedBy { get; set; }
        public int TotalAmount { get; set; }
        public List<TicketDetailsModel> ticketdetails{ get; set; }

        

    }
    public class TicketDetailsModel
    {

        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public string StartTicket { get; set; }
        public string EndTicket { get; set; }
        public string NoofTicket { get; set; }
        public string TicketFare { get; set; }
        public int WayBillID { get; set; }
      public bool IsCoupon { get; set; }
      
       

    }
    public class GamanPatraModel
    {
   

        public int GamanPatraID { get; set; }
        public int MyProperty { get; set; }
        public int StaionId { get; set; }
        public DateTime IssueDate { get; set; }
        public int BusID { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Seats { get; set; }
        public bool IsDailyService { get; set; }
        public bool PermissionType { get; set; }
        public int SubmittedBy { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
    public class GamanPatraViewModel
    {


        public int GamanPatraID { get; set; }
        
        public string StaionId { get; set; }
        public DateTime IssueDate { get; set; }
        public string BusID { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Seats { get; set; }
        public bool IsDailyService { get; set; }
        public bool PermissionType { get; set; }
        public string SubmittedBy { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
    public class WayBillViewModel
    {
        public string BusNumber { get; set; }
       
        public string Dipo { get; set; }
        public string JourneyFrom { get; set; }
        public string JourneyTo { get; set; }
        public int WayBillNo { get; set; }
        public int WayBillSerialNo { get; set; }
        public int TicketNo { get; set; }
       
        public int Fare { get; set; }
        public DateTime WayBillDate { get; set; }
        public int TotalAmount { get; set; }
        public int TicketStart { get; set; }
        public int TicketEnd { get; set; }
        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public Boolean IsCoupon { get; set; }
        public int TotalSeats { get; set; }


        public class WayBillTicketViewModel
        {
            public string BusNumber { get; set; }
            public string JourneyFrom { get; set; }
            public string JourneyTo { get; set; }
            public int WayBillNo { get; set; }
            public int WayBillSerialNo { get; set; }
            public int InsertedBy { get; set; }
            public int TotalAmount { get; set; }
            public string StationFrom { get; set; }
            public string StationTo { get; set; }
            public int StartTicket { get; set; }
            public int EndTicket { get; set; }
            public int NoofTicket { get; set; }
            public int TicketFare { get; set; }
            public int WayBillID { get; set; }

            public bool Iscoupon { get; set; }

            public int MiscPlus { get; set; }

            public int MiscMinus { get; set; }

            public int GamanPatar { get; set; }
            public WayBillTicketViewModel(int totalamount, int waybillserialno, int waybillno, int fare,
                int noofticket, int ticketstart, int ticketend, string stationfrom, string stationto, bool iscoupon,int miscplus,int miscminus, int gamanpatar)
            {


              
                this.WayBillNo = waybillno;
                this.WayBillSerialNo = waybillserialno;

                this.TicketFare = fare;

                this.StartTicket = ticketstart;
                this.EndTicket = ticketend;
                this.StationFrom = stationfrom;
                this.StationTo = stationto;
                this.NoofTicket = noofticket;
                this.TotalAmount = totalamount + fare;
                this.Iscoupon = iscoupon;
                this.MiscPlus = miscplus;
                this.MiscMinus = miscminus;
                this.GamanPatar = gamanpatar;


            }
        }

        public class RouteName
        {

            public int RouteID { get; set; }
            public string Route{ get; set; }
        }


    }
}
