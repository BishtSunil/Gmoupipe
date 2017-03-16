using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal Amount { get; set; }
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
}
