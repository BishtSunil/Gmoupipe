using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gmou.DomainModelEntities
{
    public class SupportModel
    {
    }
    public class PlacesModel
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }

        public PlacesModel(int serial, string places)
        {
            this.SerialNumber = serial;
            this.Name = places;
        }
    }

    public class CashSheetSummaryModel
    {

        public decimal Amount { get; set; }
        public DateTime Dates { get; set; }


        public CashSheetSummaryModel(decimal amount, DateTime dates)
        {

            this.Dates = dates;
            this.Amount = amount;
        }
    }

    public class ServiceRoute
    {

        public string StationFrom { get; set; }
        public string StationTo { get; set; }
        public decimal Fare { get; set; }
        public int Distance { get; set; }
        public int EmployeeID { get; set; }

        public ServiceRoute(string stationfrom, string stationto, decimal fare, int distance)
        {
            this.StationFrom = stationfrom;
            this.StationTo = stationto;
            this.Fare = fare;
            this.Distance = distance;

        }
    }

    public class VivraniReports
    {

        public DateTime VivraniDate { get; set; }
        public string VivraniNumber { get; set; }
        public decimal? Amount { get; set; }
        public Decimal TotalAmount { get; set; }
    }
    public class FuleReopts
    {

        public DateTime FuleDate { get; set; }
        public int ChitNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class MontlyBusReport
    {


        public List<VivraniReports> lstVivraniReports { get; set; }
        public List<FuleReopts> lstFuleReposrts { get; set; }
        public Decimal VivraniSum { get; set; }
        public String BusNumber { get; set; }
        public String OwnerName { get; set; }
        public String Dipo { get; set; }
        public string AccountNumber { get; set; }
        public String AccountName { get; set; }
        public String BankName { get; set; }
    }
    public class DebitStatus
    {
        public int Busid { get; set; }
        public string Busnumber { get; set; }
        public string FuelFilling { get; set; }
        public string vivrani { get; set; }
        public decimal? FuelFillingAmount { get; set; }
        public decimal? VivraniAmount { get; set; }

        public string Advance { get; set; }

        public decimal? AdvanceAmount { get; set; }
        public decimal DebitAmount { get; set; }

        public bool IsDebit { get; set; }
    }

    public class OwnerVivraniSMSInfo
    {

        public string OwnerName { get; set; }
        public long? Contact { get; set; }

        public decimal TotalAmount { get; set; }

        public string Busnumber { get; set; }
    }
    public class OwnerFuelSMSInfo
    {

        public string OwnerName { get; set; }
        public long Contact { get; set; }

        public int busid { get; set; }

        public string FuelName { get; set; }

        public string StationName { get; set; }
    }

    public class StationStocksInfo
    {

        public string FuleType { get; set; }
        public decimal OpeningStock { get; set; }

        public decimal ClosingStock { get; set; }

        public StationStocksInfo(string fueltype, decimal opening, decimal closing)
        {

            FuleType = fueltype;
            OpeningStock = opening;
            ClosingStock = closing;
        }
    }

    public class WayBillEditModel
    {
        public int ViviraniSerial { get; set; }
        public int Cash_Vivrani { get; set; }
        public decimal Amount { get; set; }
        public int WayBillNo { get; set; }
        public int WayBillSerialNo { get; set; }
        public decimal RoundOff { get; set; }

        public string BusNumber { get; set; }

         public List<Bus> lstbusnumber { get; set; }
        

        public WayBillEditModel( int vivraniserial, int cashvivrani, decimal amount, int waybillno, decimal roundoff)
        {

            ViviraniSerial = vivraniserial;
            Cash_Vivrani = cashvivrani;
            Amount = amount;
            WayBillNo = waybillno;
            RoundOff = roundoff;
           
          
        }
        public WayBillEditModel(int vivraniserial, int cashvivrani, decimal amount, int waybillno, int waybillserialno, decimal roundoff)
        {

            ViviraniSerial = vivraniserial;
            Cash_Vivrani = cashvivrani;
            Amount = amount;
            WayBillNo = waybillno;
            WayBillSerialNo = waybillserialno;
            RoundOff = roundoff;
        }
    }


    public class FuelReportDateWise
    {

        public string StationName { get; set; }
        public string FuelType { get; set; }
        public decimal TotalAmount { get; set; }



        public FuelReportDateWise(string stationname, string fueltype, decimal totalamount)
        {

            this.StationName = stationname;
            this.FuelType = fueltype;
            this.TotalAmount = totalamount;
        }
    }


    public class BusPerformanceReport
    {

        public string BusNumber { get; set; }
        public decimal VivraniAmount { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal Difference { get; set; }



        public BusPerformanceReport(string busnuber, decimal vivraniamount, decimal fuelamount, decimal diff)
        {

            this.BusNumber = busnuber;
            this.VivraniAmount = vivraniamount;
            this.FuelAmount = fuelamount;
            this.Difference = diff;
        }
    }

    public class PerformanceModel
    {

        public DateTime sdate { get; set; }
        public string order { get; set; }
        public int range { get; set; }

    }

    public class RoadWarrent
    {

        public int BusNumber { get; set; }
        public decimal Amount { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertDate { get; set; }

      
    }

    public class ModelRoadWarrent
    {

        public string VechileNumber { get; set; }
        public decimal Amount { get; set; }

        public DateTime  Dates { get; set; }

        public string insertedby { get; set; }
        public ModelRoadWarrent(string vechilenumber, decimal amount, string insertedby, DateTime dttime)
        {

            this.VechileNumber = vechilenumber;
            this.Amount = amount;
            this.insertedby = insertedby;
            this.Dates = dttime;


        }
    }
}
