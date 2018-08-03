using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
    public class Vivirani
    {
        public int UserID { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSubmitted { get; set; }

    }
    public class VivraniDetails
    {
        public int vivraniSerialNumber { get; set; }

        public string VechileNumber { get; set; }
        public string OwnerName { get; set; }
        public string ServiceName { get; set; }
        public int WayBillSerialNumber { get; set; }
        public int WayBillNumber { get; set; }
        public int TicketSerailNumber { get; set; }
        public int TicketNumber { get; set; }
        public string ServiceFrom { get; set; }
        public string ServiceTo { get; set; }
        public int Amount { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public Decimal TotalAmount { get; set; }
        public FuelVivrani vivranifuel { get; set; }
        public DateTime WayBillDate { get; set; }

    }
    public class CashSheetDetails
    {
        public int CashSheetSerailNumber { get; set; }
        public int BusID { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int InsertedBy { get; set; }
        public int DepartmentID { get; set; }
        public decimal TotalAmount { get; set; }
        public CashSheetDetails() { }
        public CashSheetDetails(decimal totalamount, int cashsheetserial, int busid, string description, decimal amount)
        {
            this.CashSheetSerailNumber = cashsheetserial;
            this.BusID = busid;
            this.Description = description;
            this.Amount = amount;
            this.TotalAmount = totalamount;

        }
    }

    public class CashSheetDetailsModel
    {
        public int CashSheetSerailNumber { get; set; }
        public string BusNumber { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }



        public CashSheetDetailsModel(decimal totalamount, int cashsheetserial, string busnumber, string description, decimal amount)
        {
            this.CashSheetSerailNumber = cashsheetserial;
            this.BusNumber = busnumber;
            this.Description = description;
            this.Amount = amount;
            this.TotalAmount = amount + totalamount;

        }
    }
    public class CashSheetModel
    {
        public int CashSheetSerialNumber { get; set; }
        public List<Bus> bus { get; set; }
    }
    public class vivraniDomainModel
    {
        public int VivraniSerialNumber { get; set; }
        public List<BusOwner> BusOwner { get; set; }
        public List<Bus> bus { get; set; }
        public List<FulePump> fuelpump { get; set; }
        public List<FuelType> fueltype { get; set; }
    }
    public class BusOwner
    {
        public int BusOwnerID { get; set; }
        public string BusOwnerName { get; set; }
    }
    public class FulePump
    {
        public int FuelPumpID { get; set; }
        public string PumpName { get; set; }
    }
    public class FuelType
    {
        public int FuelTypeID { get; set; }
        public string FuelTypeName { get; set; }
    }
    public class Bus
    {
        public int BusID { get; set; }
        public string BusNumber { get; set; }
        public Bus()
        {

        }
        public Bus(int busid, string busnumber)
        {
            this.BusID = busid;
            this.BusNumber = busnumber;
        }
    }
    public class VivraniDetailsModel
    {
        public int vivraniSerialNumber { get; set; }

        public string VechileNumber { get; set; }
        public string OwnerName { get; set; }
        public string ServiceName { get; set; }
        public int WayBillSerialNumber { get; set; }
        public int WayBillNumber { get; set; }
        public int TicketSerailNumber { get; set; }
        public int TicketNumber { get; set; }
        public string ServiceFrom { get; set; }
        public string ServiceTo { get; set; }
        public Decimal Amount { get; set; }
        public Decimal TotalAmount { get; set; }


        public VivraniDetailsModel(decimal totalamount, int vivraniserialnumber, string vechilenumber, string ownername, string servicename, int waybillserial, int waybillnumber,
            int ticketserial, int ticketnumber, string servicefrom, string serviceto, decimal amount)
        {

            this.vivraniSerialNumber = vivraniserialnumber;
            this.VechileNumber = vechilenumber;
            this.OwnerName = ownername;
            this.ServiceName = servicename;
            this.WayBillSerialNumber = waybillserial;
            this.WayBillNumber = waybillnumber;
            this.TicketSerailNumber = ticketnumber;
            this.ServiceFrom = servicefrom;
            this.ServiceTo = serviceto;
            this.Amount = amount;
            this.TotalAmount = totalamount + amount;
        }


    }

    public class FuelVivrani
    {
        public int VivraniSerialID { get; set; }
        public DateTime FuelDate { get; set; }
        public int ChitNumber { get; set; }
        public string Fueltype { get; set; }
        public int FuelStation { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }
        public int InsertedBy { get; set; }
        public decimal FuelPrice { get; set; }

    }
    public class CashSheetOtherExDetailsModel
    {

        public decimal Amount { get; set; }
        public string BusNumber { get; set; }
        public string Description { get; set; }
        public int CashSheetSerial { get; set; }
    }
    public class CashSheetVivraniDetails
    {
        public decimal Amount { get; set; }
        public int VivraniSerialNumber { get; set; }
        public string BusNumber { get; set; }

    }
    public class GenerateCashSheetModel
    {
        public string UserName { get; set; }
        public List<CashSheetOtherExDetailsModel> otherexepnseslist { get; set; }
        public List<CashSheetVivraniDetails> cashsheetvivranidetailslust { get; set; }
    }
    public class BusOwnerName
    {
        public string OwnerName { get; set; }
        public int WayBillBookNo { get; set; }
        public BusOwnerName(string ownername, int waybillbookno)
        {
            this.OwnerName = ownername;
            this.WayBillBookNo = waybillbookno;
        }
    }
    public class BusDetailsModel
    {
        public List<Bus> lstbusName { get; set; }
    }

    public class WayBillBookDetailsModel
    {
        public int VechileNumber { get; set; }
        public int WillBillBookNo { get; set; }
        public int WillBillBookSerialStart { get; set; }
        public int WillBillBookSerialEnd { get; set; }
        public int EmpoyeeID { get; set; }


    }
    public class WayBillBookDetailsModelDisplay
    {
        public string VechileNumber { get; set; }
        public int WayBillBookNo { get; set; }
        public int WayBillBookSerialStart { get; set; }
        public int WayBillBookSerialEnd { get; set; }
        public DateTime Date { get; set; }


        public WayBillBookDetailsModelDisplay(string vechilenumber, int waybillbookno, int waybillstart, int waybillend, DateTime date)
        {
            this.VechileNumber = vechilenumber;
            this.WayBillBookNo = waybillbookno;
            this.WayBillBookSerialStart = waybillstart;
            this.WayBillBookSerialEnd = waybillend;
            this.Date = date;
        }
    }
    public class BusJourneyHistory
    {
        public int WayBillSerialNo { get; set; }
        public int WayBillBookNo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string BusNumber { get; set; }
        public DateTime? WayBillDate { get; set; }


        public BusJourneyHistory(int waybillbook, int waybillserial, string from, string to, string busnumer, DateTime? waybilldate)
        {
            this.BusNumber = busnumer;
            this.WayBillBookNo = waybillbook;
            this.WayBillSerialNo = waybillserial;
            this.From = from;
            this.To = to;
            this.WayBillDate = waybilldate;
        }

        
    }
    public class CashVivraniDetails
    {
        public decimal Fare { get; set; }
        public string BusNumber { get; set; }
        public string VivraniNumber { get; set; }
        public DateTime WayBillDate { get; set; }
        public int WayBillNo { get; set; }
        public int WayBillSerialNumber { get; set; }
        public int TicketNumber { get; set; }
        public string TicketSeries { get; set; }
        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public decimal TotalAmount { get; set; }

        public CashVivraniDetails(decimal amount, string vivraniid, string busnumber, DateTime waybillinsertdate, int waybillno, int waybillserialnumber,
             int ticketfrom, int ticketto, string stationfrom, string stationto)
        {

            this.Fare = amount;
            this.BusNumber = busnumber;
            this.VivraniNumber = vivraniid;
            this.WayBillDate = waybillinsertdate;
            this.WayBillNo = waybillno;
            this.WayBillSerialNumber = waybillserialnumber;
           
            this.TicketSeries = string.Format("{0}/{1}", ticketfrom.ToString(), ticketto.ToString());
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.TotalAmount = TotalAmount + amount;
        }

    }

    public class CashSheetSummary
    {

        public decimal TotalAmount { get; set; }
        public int InsertedBy { get; set; }
        public int CashSheetSerialNo { get; set; }
    }

    public class VivraniReport
    {

       // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date FROM dbo.tmp_cashvivrani
        public int VivraniNumber { get; set; }
      
        public int WayBillSerialNumber { get; set; }
        public int WayBillNumber { get; set; }
        public int TicketFrom { get; set; }
        public int TicketTo { get; set; }
        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public Decimal Amount { get; set; }
        public DateTime VivraniDate { get; set; }
        public String BusNumber { get; set; }
       // public Decimal TotalAmount { get; set; }


        public VivraniReport(int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount, DateTime date, string busnumber)
        {
            this.VivraniNumber = vivraniid;
            this.WayBillSerialNumber = waybillnumber;
            this.WayBillNumber = wayserialnumb;
            this.TicketFrom = ticketfrom;
            this.TicketTo = ticketto;
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.Amount = amount;
            this.VivraniDate = date;
            this.BusNumber = busnumber;
        }
    }

    public class VivraniReportUpdated
    {

        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date FROM dbo.tmp_cashvivrani
        public int VivraniNumber { get; set; }

      
        public Decimal WayBillAmount { get; set; }
        public Decimal VivraniAmount { get; set; }
        public Decimal RoundOff { get; set; }
        // public Decimal TotalAmount { get; set; }


        public VivraniReportUpdated(int vivraniid, decimal WayBillAmount, decimal vivraniamount, decimal roundoff)
        {
            this.VivraniNumber = vivraniid;
            this.WayBillAmount = WayBillAmount;
            this.VivraniAmount = vivraniamount;
            this.RoundOff = roundoff;
           
        }
    }

    public class VivraniAllReportUpdated
    {

        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date FROM dbo.tmp_cashvivrani
        public int VivraniNumber { get; set; }

        public DateTime Date { get; set; }
        public string BusNumber { get; set; }
       
        public Decimal VivraniAmount { get; set; }

       

        public VivraniAllReportUpdated( int vivraninumber, DateTime date,string busnumber, decimal vivraniamount )
        {
            this.VivraniNumber = vivraninumber;
            this.BusNumber = busnumber;
            this.VivraniAmount = vivraniamount;
            this.Date = date;

        }
    }
    public class StationDetailModel
        {

            public int busID { get; set; }
            public String BusNumber { get; set; }
            public String  OwnerName { get; set; }
            public DateTime Date { get; set; }
        }
}
