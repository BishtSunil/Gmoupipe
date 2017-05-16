using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gmou.DomainModelEntities
{
    public class FuelModel
    {
        public int LubricantID { get; set; }
        public string LubricantName { get; set; }

        public FuelModel(int lubricantid, string lubricantname)
        {

            this.LubricantID = lubricantid;
            this.LubricantName = lubricantname;
        }
    }
    public class FuelTypeModel
    {

        public int FuelType { get; set; }
        public string FuelName { get; set; }

        public FuelTypeModel(int fueltype, string fuelname)
        {
            this.FuelType = fueltype;
            this.FuelName = fuelname;

        }
    }

    public class BusList
    {

        public int BusID { get; set; }
        public string BusNumber { get; set; }

        public BusList(int busid, string busnumber)
        {
            this.BusID = busid;
            this.BusNumber = busnumber;
        }

    }
    public class FuelStationModel
    {

        public int StationID { get; set; }
        public string StationName { get; set; }
        public FuelStationModel(int stationid, string stationname)
        {
            this.StationID = stationid;
            this.StationName = stationname;
        }
    }

    public class FuelCashMemo
    {

//        @vechilenumber nvarchar(50),
//@fuletype int, 
//@quantity  nvarchar(50) , 
//@price money, 
//@otherfuel  nvarchar(50)=null , 
//@otherprice  nvarchar(50)=null, 
//@station_id int, 
//@insertedby int,
//@serial_no int

    public string VechileNumber{get;set;}
    public int Fueltype { get; set; }
    public decimal FuelQuantity { get; set; }
    public decimal Price { get; set; }
    public string OtherFule { get; set; }
    public decimal OtherPrice { get; set; }
    public int FuelStationID { get; set; }
    public int InsertedBy { get; set; }
    public int SerialNumber { get; set; }
     
    }

    public class FuelCashOtherMemo
    {

        //        @vechilenumber nvarchar(50),
        //@fuletype int, 
        //@quantity  nvarchar(50) , 
        //@price money, 
        //@otherfuel  nvarchar(50)=null , 
        //@otherprice  nvarchar(50)=null, 
        //@station_id int, 
        //@insertedby int,
        //@serial_no int

        public string VechileNumber { get; set; }
        //public int Fueltype { get; set; }
        //public int FuelQuantity { get; set; }
        //public decimal Price { get; set; }
        public string OtherFule { get; set; }
        public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int SerialNumber { get; set; }
        public List<OtherFuel> listFuel { get; set; }
    }


    public class FuelCashMemoModel
    {

        //        @vechilenumber nvarchar(50),
        //@fuletype int, 
        //@quantity  nvarchar(50) , 
        //@price money, 
        //@otherfuel  nvarchar(50)=null , 
        //@otherprice  nvarchar(50)=null, 
        //@station_id int, 
        //@insertedby int,
        //@serial_no int

        public string VechileNumber { get; set; }
        public string Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public string FuelStationID { get; set; }
        //public int InsertedBy { get; set; }
        public int SerialNumber { get; set; }


        public FuelCashMemoModel(string vechilenumber, string fueltype, decimal quantity, decimal price, string fuelstationid, int serialno)
        {
            this.VechileNumber = vechilenumber;
            this.Fueltype = fueltype;
            this.FuelQuantity = quantity;
            this.Price = price;
            this.FuelStationID = fuelstationid;
            this.SerialNumber = serialno;
        }
    }

    public class FuelPriceModel
    {

        public int pumpid { get; set; }
        public int fuelid { get; set; }

        public FuelPriceModel() { }
    }

    public class FuelPriceListModel
    {

    //    @pumpid int, 
    //@fuelid int, 
    //@updatedby int, 
    //@price int

        public int PumpID { get; set; }
        public int FuelID { get; set; }
        public int UpdatedBy { get; set; }
        public decimal Price { get; set; }
    }

    public class FuelPriceListShowModel
    {

        //    @pumpid int, 
        //@fuelid int, 
        //@updatedby int, 
        //@price int
        public int FuelTypeID { get; set; }
        public string PumpName { get; set; }
        public string FuelType { get; set; }
        public string UpdatedBy { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedDate { get; set; }


        public FuelPriceListShowModel(int fueltypeid, string pumpid, string fuelid, string updatedby, decimal price, DateTime updated)
        {
            this.FuelTypeID = fueltypeid;
            this.PumpName = pumpid;
            this.FuelType = fuelid;
            this.UpdatedBy = updatedby;
            this.Price = price;
            this.UpdatedDate = updated;


        }

    }
   
    public class UpdateFuelModel
    {
        public int FuelListID { get; set; }
          public int PumpID { get; set; }
        public int FuelID { get; set; }
        public int UpdatedBy { get; set; }
        public decimal Price { get; set; }
    }
    public class ChitFuelModelInsert
    {
        public string VechileNumber { get; set; }
        //public int Fueltype { get; set; }
        //public int FuelQuantity { get; set; }
        //public decimal Price { get; set; }
        public string  OtherFule        { get; set; }
        public decimal OtherPrice       { get; set; }
        public int     FuelStationID    { get; set; }
        public int      InsertedBy           { get; set; }
        public int      DieselBookno { get; set; }
        public int      ChitNo           { get; set; }
        public string   Comment         { get; set; }
        public int      Fueltype        { get; set; }
        public decimal      FuelQuantity         { get; set; }
        public decimal  Price       { get; set; }
      
    }
    public class ChitFuel
    {

        public string VechileNumber { get; set; }
        //public int Fueltype { get; set; }
        //public int FuelQuantity { get; set; }
        //public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int DieselBookno { get; set; }
        public int ChitNo { get; set; }
        public string Comment { get; set; }
        public List<OtherFuel> listFuel { get; set; }
    }
    public class OtherFuel
    {
        public int Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
      
    }
    public class OtherFuelModel
    {
         //cmd.Parameters.AddWithValue("vechilenumber", model.VechileNumber);
         //           cmd.Parameters.AddWithValue("fuletype", model.Fueltype);
         //           cmd.Parameters.AddWithValue("quantity", model.FuelQuantity);
         //           cmd.Parameters.AddWithValue("price", model.Price);
         //           cmd.Parameters.AddWithValue("otherfuel", model.OtherFule);
         //           cmd.Parameters.AddWithValue("otherprice", model.OtherPrice);
         //           cmd.Parameters.AddWithValue("station_id", model.FuelStationID);
         //           cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
         //           cmd.Parameters.AddWithValue("serial_no",model.SerialNumber);
                    
         //           cmd.Parameters.AddWithValue("comment", model.Comment);
        public string VechileNumber { get; set; }
        //public int Fueltype { get; set; }
        //public int FuelQuantity { get; set; }
        //public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        //public int DieselBookno { get; set; }
        //public int ChitNo { get; set; }
        public int serialno { get; set; }
        public string Comment { get; set; }
        public List<OtherFuel> listFuel { get; set; }
    }
    public class ChitFuelModel
    {
  
        public string VechileNumber { get; set; }
        public string Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public string FuelStationID { get; set; }
        //public int InsertedBy { get; set; }
        public int BookNo { get; set; }
        public int ChitNo { get; set; }
        public string Comment { get; set; }
        public DateTime InsertedDate { get; set; }
       

        public ChitFuelModel(string vechilenumber,string fueltype, decimal quantity, decimal price,string pumpname,int dieselbook, int chitnumber,string comment, DateTime inserteddate)
        {

            this.VechileNumber = vechilenumber;
            this.Fueltype = fueltype;
            this.FuelQuantity = quantity;
            this.Price = price;
            this.FuelStationID = pumpname;
            this.BookNo = dieselbook;
            this.ChitNo = chitnumber;
            this.Comment = comment;
            this.InsertedDate = inserteddate;
        }
    }

    public class FuelRecieve
    {
        public int Pumpid { get; set; }
        public int FuelType { get; set; }
        public decimal PriceRecieved { get; set; }
        public decimal QuantityRecieved { get; set; }
        public string VechileNumber { get; set; }
        public String Comment { get; set; }
        public int InsertedBy { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class FuelRecieveModel
    {
        public string    Pumpid { get; set; }
        public string    FuelType { get; set; }
        public decimal PriceRecieved { get; set; }
        public decimal QuantityRecieved { get; set; }
        public string VechileNumber { get; set; }
        public String Comment { get; set; }
        public DateTime RecievedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalQuantity { get; set; }
        public FuelRecieveModel(string pumpid, string fueltype, decimal price, decimal quantity, string vechilenumber, string comment, DateTime receiveddate, decimal totalprice, decimal totalquantity)
        {

            this.Pumpid = pumpid;
            this.FuelType = fueltype;
            this.PriceRecieved = price;
            this.QuantityRecieved = quantity;
            this.VechileNumber = vechilenumber;
            this.Comment = comment;
            this.RecievedDate = receiveddate;
            this.TotalPrice = totalprice;
            this.TotalQuantity = totalquantity;
        }
    }

    public class StockModel
    {

    //    A.fueltype ,B.recievingquantity,B.recievingprice,B.recievingdate,B.vechileno,B.totalprice,
    //C.openingstock,C.quantity,C.closingstocki

        public string FuelType { get; set; }
        public decimal RecievingQuantity { get; set; }
        public decimal RecievingPrice { get; set; }
        public DateTime RecievingDate { get; set; }
        public String VechileNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal OpeningStock { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal ClosingStock { get; set; }

        public StockModel(string fueltype, decimal recievingquantity, decimal receievedprice , DateTime receivedate, string vechilenumber,
            decimal totalprice, decimal openingstock, decimal currentquantity, decimal closingstock)
        {
            this.FuelType = fueltype;
            this.RecievingQuantity = recievingquantity;
            this.RecievingPrice = receievedprice;
            this.RecievingDate = receivedate;
            this.VechileNumber = vechilenumber;
            this.TotalPrice = totalprice;
            this.OpeningStock = openingstock;
            this.CurrentQuantity = currentquantity;
            this.ClosingStock = closingstock;
        }
    }

    public class AllSalesModel
    {
        public decimal PetrolQuantity { get; set; }
        public decimal PetrolAmount { get; set; }
        public decimal DeiselQuantity { get; set; }
        public decimal DeiselAmount { get; set; }
        public decimal LubricantQuantity { get; set; }
        public decimal LubricantAmount { get; set; }

        public decimal OPetrolQuantity { get; set; }
        public decimal OPetrolAmount { get; set; }
        public decimal ODeiselQuantity { get; set; }
        public decimal ODeiselAmount { get; set; }
        public decimal OLubricantQuantity { get; set; }
        public decimal OLubricantAmount { get; set; }

        public decimal SPetrolQuantity { get; set; }
        public decimal SPetrolAmount { get; set; }
        public decimal SDeiselQuantity { get; set; }
        public decimal SDeiselAmount { get; set; }
        public decimal SLubricantQuantity { get; set; }
        public decimal SLubricantAmount { get; set; }
         public SelectList fuelpump { get; set; }
    }
    public class ChitFuelSale
    {

        public decimal PetrolQuantity { get; set; }
        public decimal PetrolAmount { get; set; }
        public decimal DeiselQuantity { get; set; }
        public decimal DeiselAmount { get; set; }
        public decimal LubricantQuantity { get; set; }
        public decimal LubricantAmount { get; set; }
        public decimal Fueltype { get; set; }

        public ChitFuelSale(decimal pquantity, decimal pamount, decimal dquantity, decimal damount, decimal lquantity, decimal lamount,int fuletype)
 {
     this.PetrolQuantity = pquantity;
     this.PetrolAmount = pamount;
     this.DeiselQuantity = dquantity;
     this.DeiselAmount = damount;
     this.LubricantQuantity = lquantity;
     this.LubricantAmount = lamount;
     this.Fueltype = fuletype;

 }
    }

    public class CashFuleSale
    {

        public int PumpId { get; set; }
        public int FuelType { get; set; }
        public decimal ChitQuanity { get; set; }
        public decimal StaffQuanity { get; set; }
        public decimal OtherQuanity { get; set; }
        public int StartReading { get; set; }
        public int EndReading { get; set; }
    }
    public class FuelChashModel
    {

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public FuelChashModel() { }
    }
    public class FuelOtherSale
    {

        //        @vechilenumber nvarchar(50),
        //@fuletype int, 
        //@quantity  nvarchar(50) , 
        //@price money, 
        //@otherfuel  nvarchar(50)=null , 
        //@otherprice  nvarchar(50)=null, 
        //@station_id int, 
        //@insertedby int,
        //@serial_no int

        public string VechileNumber { get; set; }
        public int Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        public string OtherFule { get; set; }
        public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int SerialNumber { get; set; }
        public string Comment { get; set; }

    }
    public class FuelOtherModel
    {

        //        @vechilenumber nvarchar(50),
        //@fuletype int, 
        //@quantity  nvarchar(50) , 
        //@price money, 
        //@otherfuel  nvarchar(50)=null , 
        //@otherprice  nvarchar(50)=null, 
        //@station_id int, 
        //@insertedby int,
        //@serial_no int

        public string VechileNumber { get; set; }
        public string Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public string FuelStationID { get; set; }
        //public int InsertedBy { get; set; }
        public decimal SerialNumber { get; set; }
        public string Comment { get; set; }
        public DateTime InsertDate { get; set; }

        public FuelOtherModel(string vechilenumber, string fueltype, decimal quantity, decimal price, string fuelstationid, int serialno, string comment, DateTime insertdate)
        {
            this.VechileNumber = vechilenumber;
            this.Fueltype = fueltype;
            this.FuelQuantity = quantity;
            this.Price = price;
            this.FuelStationID = fuelstationid;
            this.SerialNumber = serialno;
            this.Comment = comment;
            this.InsertDate = insertdate;
        }
    }
    public class staffFuel
    {

        public string VechileNumber { get; set; }
        public int Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        public string OtherFule { get; set; }
        public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int DieselBookno { get; set; }
        public int ChitNo { get; set; }
        public string Comment { get; set; }
        public List<OtherFuel> listFuel { get; set; }
    }

    public class FuelStafflModel
    {

        public string VechileNumber { get; set; }
        public int Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public int FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int BookNo { get; set; }
        public int ChitNo { get; set; }
        public string Comment { get; set; }
        public DateTime InsertedDate { get; set; }
    }
    public class StaffFuelModel
    {
       
        public string VechileNumber { get; set; }
        public string Fueltype { get; set; }
        public decimal FuelQuantity { get; set; }
        public decimal Price { get; set; }
        //public string OtherFule { get; set; }
        //public decimal OtherPrice { get; set; }
        public string FuelStationID { get; set; }
        public int InsertedBy { get; set; }
        public int BookNo { get; set; }
        public int ChitNo { get; set; }
        public string Comment { get; set; }
        public DateTime InsertedDate { get; set; }
       

        public StaffFuelModel(string vechilenumber, string fueltype, decimal quantity, decimal price, string pumpname, int dieselbook, int chitnumber, string comment, DateTime inserteddate)
        {

            this.VechileNumber = vechilenumber;
            this.Fueltype = fueltype;
            this.FuelQuantity = quantity;
            this.Price = price;
            this.FuelStationID = pumpname;
            this.BookNo = dieselbook;
            this.ChitNo = chitnumber;
            this.Comment = comment;
            this.InsertedDate = inserteddate;
        }
    }
    public class FinalSummary
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
    public class RecieveFuleModel
    {

       // F.fueltype,A.recievingprice,A.recievingquantity,A.recievingdate,A.vechileno
        public string FuelType { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
        public string VechileNo { get; set; }
        public decimal TotalAmount { get; set; }

        public RecieveFuleModel(string fuletype, decimal amount, decimal quantity, DateTime date, string vechileno)
        {

            this.FuelType = fuletype;
            this.Amount = amount;
            this.Quantity = quantity;
            this.TotalAmount = quantity * amount;
            this.Date = date;
            this.VechileNo = vechileno;
        }
    }

    public class FuelStockReport
    {
        public decimal OpeningStock { get; set; }
        public decimal ClosingStock { get; set; }

        public string Fueltype { get; set; }
        public string StationName { get; set; }
        public double SalesPrice { get; set; }
        public string UserName { get; set; }

      
        public FuelStockReport(decimal openingstock, decimal closingstock, string fueltype, string stationname, double saleprice , string username)
        {
            this.OpeningStock = openingstock; ;
            this.ClosingStock = closingstock;
            this.Fueltype = fueltype;
            this.StationName = stationname;
            this.SalesPrice = saleprice;
            this.UserName = username;
        }


       

    }


    public class SaleReportFinal
    {
        public PetrolSaleReport petrolsale { get; set; }
        public DieselSaleReport dieselsale { get; set; }
        public LubSaleReport lubsale { get; set; }
    }
    public class PetrolSaleReport
    {
        public long ? StartMeter { get; set; }
        public long ? EndMeter { get; set; }

        public decimal ? OwnerSale { get; set; }
        public decimal? CashSale { get; set; }

        public decimal? StaffSale { get; set; }
        public decimal? OtherSale { get; set; }
        public decimal? OwnerQuanity { get; set; }
        public decimal? CashQuantity { get; set; }

        public decimal? StaffQuantity { get; set; }
        public decimal? OtherQuantity { get; set; }

    }

    public class DieselSaleReport
    {
        public long? StartMeter { get; set; }
        public long? EndMeter { get; set; }

        public decimal? OwnerSale { get; set; }
        public decimal? CashSale { get; set; }

        public decimal? StaffSale { get; set; }
        public decimal? OtherSale { get; set; }
        public decimal? OwnerQuanity { get; set; }
        public decimal? CashQuantity { get; set; }

        public decimal? StaffQuantity { get; set; }
        public decimal? OtherQuantity { get; set; }
    }
        public class LubSaleReport
        {
            public long? StartMeter { get; set; }
            public long? EndMeter { get; set; }

            public decimal? OwnerSale { get; set; }
            public decimal? CashSale { get; set; }

            public decimal? StaffSale { get; set; }
            public decimal? OtherSale { get; set; }
        public decimal? OwnerQuanity { get; set; }
        public decimal? CashQuantity { get; set; }

        public decimal? StaffQuantity { get; set; }
        public decimal? OtherQuantity { get; set; }
    }

}
