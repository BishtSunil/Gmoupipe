using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Gmou.DomainModelEntities;
using Gmou.DataRepository.EntityRepository;

namespace Gmou.DataRepository
{
    public class FuelRepository
    {

        public static bool CheckDuplicateEntry(int diselbook, int chitno, int busnumber)
        {
            int count = 0;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_checkDuplicateEntry", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("dieselbook", diselbook);
                    cmd.Parameters.AddWithValue("chitcho", chitno);
                    cmd.Parameters.AddWithValue("busnumber", busnumber);

                    conn.Open();
                    try
                    {
                        count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }


                    
        }
        public static IEnumerable<FuelModel> GetAllLubricants()
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetLubricants", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        yield return new FuelModel(
                             (Convert.ToInt32(rd["lubricant_id"])),
                              ((rd["lubricant_type"]).ToString()));

                    }

                    conn.Close();
                }
            }
        }

        public static IEnumerable<FuelStationModel> GetAllFuelStations()
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetFuelStation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        yield return new FuelStationModel(
                             (Convert.ToInt32(rd["fuel_pump_id"])),
                              ((rd["fuel_pump_name"]).ToString()));

                    }

                    conn.Close();
                }
            }
        }

        public static IEnumerable<BusList> GetAllBusList()
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAallBusList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        yield return new BusList(
                      Convert.ToInt32(rd["busid"]), rd["bus_number"].ToString());

                    }

                }
            }
        }
        public static IEnumerable<FuelTypeModel> GetAllFuelList()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("SP_getAllfuelTypes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        yield return new FuelTypeModel(
                             (Convert.ToInt32(rd["fueltypeid"])),
                              ((rd["fueltype"]).ToString()));

                    }

                    conn.Close();
                }
            }

        }


        public static bool InsertCashMemo(FuelCashMemo model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertCashMemo", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", model.VechileNumber);
                    cmd.Parameters.AddWithValue("fuletype", model.Fueltype);
                    cmd.Parameters.AddWithValue("quantity", model.FuelQuantity);
                    cmd.Parameters.AddWithValue("price", model.Price);
                    cmd.Parameters.AddWithValue("otherfuel", model.OtherFule);
                    cmd.Parameters.AddWithValue("otherprice", model.OtherPrice);
                    cmd.Parameters.AddWithValue("station_id", model.FuelStationID);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("serial_no", model.SerialNumber);
                    conn.Open();
                    try
                    {
                        cashmemoid = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    conn.Close();


                }
            }
            return true;// GetUpdatedCashMemoData(cashmemoid);
        }

        private static IEnumerable<FuelCashMemoModel> GetUpdatedCashMemoData(int id)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetUpdatedCashFuel", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("cashfuelID", id);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    while (dr.Read())
                    {
                        yield return new FuelCashMemoModel(

                           dr["vechilenumber"].ToString(),

                           (dr["fueltype"].ToString()),
                           (Convert.ToDecimal(dr["quantity"])),

                            (Convert.ToDecimal(dr["price"])),
                         (dr["fuel_pump_name"].ToString()),
                        (Convert.ToInt32(dr["serial_no"]))

                       );
                    }
                    conn.Close();
                }
            }


        }


        public static decimal GetFuelPrice(int pumpid, int fueltypeid)
        {
            decimal fuelprice;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetFuelPrice", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                    cmd.Parameters.AddWithValue("fuelid", fueltypeid);

                    conn.Open();
                    fuelprice = (decimal)cmd.ExecuteScalar();
                    conn.Close();
                }
            }

            return fuelprice;
        }

        public static IEnumerable<FuelPriceListShowModel> GetAllFuelRate()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("SP_GetAllFuelPriceList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        yield return new FuelPriceListShowModel(
                            (Convert.ToInt32(rd["rate_list_id"])),
                             (rd["fuel_pump_name"]).ToString(),
                             (rd["fueltype"]).ToString(),

                             ((rd["username"]).ToString()),
                             Convert.ToDecimal(rd["price"]),
                             Convert.ToDateTime((rd["inserted_date"])));

                    }

                    conn.Close();
                }
            }

        }


        public static bool SaveFuelDetails(FuelPriceListModel model)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertFuelDetails", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("station_id", model.PumpID);
                    cmd.Parameters.AddWithValue("fuel_type_id", model.FuelID);
                    cmd.Parameters.AddWithValue("price", model.Price);
                    cmd.Parameters.AddWithValue("updated_by", model.UpdatedBy);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    conn.Close();

                }
            }
            return true;
        }


        public static bool UpdatefuelDetail(UpdateFuelModel model)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_UpdateFuelPrice", conn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fuel_listi_id", model.FuelListID);
                    cmd.Parameters.AddWithValue("pumpid", model.PumpID);
                    cmd.Parameters.AddWithValue("fuelid", model.FuelID);
                    cmd.Parameters.AddWithValue("updatedby", model.UpdatedBy);
                    cmd.Parameters.AddWithValue("price", model.Price);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    conn.Close();

                }
            }
            return true;
        }



        public static bool InsertChit(ChitFuelModelInsert model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertChitFuel", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", model.VechileNumber);
                    cmd.Parameters.AddWithValue("fuletype", model.Fueltype);
                    cmd.Parameters.AddWithValue("quantity", model.FuelQuantity);
                    cmd.Parameters.AddWithValue("price", model.Price);
                    cmd.Parameters.AddWithValue("otherfuel", model.OtherFule);
                    cmd.Parameters.AddWithValue("otherprice", model.OtherPrice);
                    cmd.Parameters.AddWithValue("station_id", model.FuelStationID);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("comment", model.Comment);
                    cmd.Parameters.AddWithValue("dieselbookno", model.DieselBookno);
                    cmd.Parameters.AddWithValue("dieselchitno", model.ChitNo);
                    cmd.Parameters.AddWithValue("date", model.date);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }


                    conn.Close();


                }
            }
            return true;
        }


        public static IEnumerable<ChitFuelModel> GetAllChitDetailsByUser(int userid, int usertype)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllChitFuelDetailsByDateAndUser", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userid", userid);
                    cmd.Parameters.AddWithValue("usertype", usertype);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    while (dr.Read())
                    {


                        yield return new ChitFuelModel(

                           dr["vechilenumber"].ToString(),

                           (dr["fueltype"].ToString()),
                           (Convert.ToDecimal(dr["quantity"])),

                            (Convert.ToDecimal(dr["price"])),
                         (dr["fuel_pump_name"].ToString()),
                        (Convert.ToInt32(dr["dieselbookno"])),
                         (Convert.ToInt32(dr["dieselchitno"])),
                            (dr["comment"].ToString()),
                            (Convert.ToDateTime(dr["inserteddate"]))

                       );
                    }
                    conn.Close();
                }
            }


        }

        public static IEnumerable<ChitFuelModel> GetAllChitDetails(DateTime date, int pumpid)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllChitFuelDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    while (dr.Read())
                    {


                        yield return new ChitFuelModel(

                           dr["bus_number"].ToString(),

                           (dr["fueltype"].ToString()),
                           (Convert.ToDecimal(dr["quantity"])),

                            (Convert.ToDecimal(dr["price"])),
                         (dr["fuel_pump_name"].ToString()),
                        (Convert.ToInt32(dr["dieselbookno"])),
                         (Convert.ToInt32(dr["dieselchitno"])),
                            (dr["comment"].ToString()),
                            (Convert.ToDateTime(dr["inserteddate"]))

                       );
                    }
                    conn.Close();
                }
            }


        }

        public static bool InsertReceivingChit(FuelRecieve model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertFuelRecieving", conn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", model.Pumpid);
                    cmd.Parameters.AddWithValue("fueltypeid", model.FuelType);
                    cmd.Parameters.AddWithValue("recievingprice", model.PriceRecieved);
                    cmd.Parameters.AddWithValue("recievingquantity", model.QuantityRecieved);
                    cmd.Parameters.AddWithValue("vechileno", model.VechileNumber);
                    cmd.Parameters.AddWithValue("comment", model.Comment);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("totalprice", model.QuantityRecieved * model.PriceRecieved);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }


                    conn.Close();


                }
            }
            return true;
        }


        public static IEnumerable<StockModel> GetStockByPumpName(int pumpid)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetOpeningAndClosingStocks", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    while (dr.Read())
                    {


                        yield return new StockModel(

                           dr["fueltype"].ToString(),
                            (Convert.ToDecimal(dr["recievingquantity"])),
                             (Convert.ToDecimal(dr["recievingprice"])),
                              (Convert.ToDateTime(dr["recievingdate"])),
                           (dr["vechileno"].ToString()),


                            (Convert.ToDecimal(dr["totalprice"])),


                        (Convert.ToDecimal(dr["openingstock"])),
                         (Convert.ToDecimal(dr["quantity"])),

                            (Convert.ToDecimal(dr["closingstock"]))


                       );
                    }
                    conn.Close();
                }
            }


        }

        public static IEnumerable<ChitFuelSale> GetChitFuelSalesDetails(int pumpid)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetOtherFuelAmount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    while (dr.Read())
                    {

                        yield return new ChitFuelSale(



                           (Convert.ToDecimal(dr["PetrolQuantity"])),
                              (Convert.ToDecimal(dr["PetrolAmount"])),
                              (Convert.ToDecimal(dr["DeiselQuantity"])),
                              (Convert.ToDecimal(dr["DeiselAmount"])),
                               (Convert.ToDecimal(dr["LubricantQuantity"])),
                              (Convert.ToDecimal(dr["LubricantAmount"])),
                               (Convert.ToInt32(dr["fueltype"]))
                              );
                    }

                }
                conn.Close();
            }
        }

        public static FuelChashModel GetCashFuelSalesDetails(CashFuleSale model)
        {

            FuelChashModel objcash = new FuelChashModel();
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetCashFuelSale", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", model.PumpId);
                    cmd.Parameters.AddWithValue("fueltypeid", model.FuelType);
                    cmd.Parameters.AddWithValue("patrolQuantity", model.ChitQuanity);
                    cmd.Parameters.AddWithValue("staffqaunity", model.StaffQuanity);
                    cmd.Parameters.AddWithValue("otherquantity", model.OtherQuanity);
                    cmd.Parameters.AddWithValue("startreading", model.StartReading);
                    cmd.Parameters.AddWithValue("endreading", model.EndReading);

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        objcash = new FuelChashModel
                        {
                            Quantity = (Convert.ToDecimal(rd["CashQuantity"])),

                            Price = Convert.ToDecimal(rd["TotalPrice"])
                        };


                    }
                }
            }
            return objcash;
        }

        public static bool InsertStaffFule(FuelStafflModel model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertStaffFuel", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", model.VechileNumber);
                    cmd.Parameters.AddWithValue("fuletype", model.Fueltype);
                    cmd.Parameters.AddWithValue("quantity", model.FuelQuantity);
                    cmd.Parameters.AddWithValue("price", model.Price);
                    //cmd.Parameters.AddWithValue("otherfuel", model.OtherFule);
                    //cmd.Parameters.AddWithValue("otherprice", model.OtherPrice);
                    cmd.Parameters.AddWithValue("station_id", model.FuelStationID);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                   
                    cmd.Parameters.AddWithValue("deiselbookno", model.BookNo);
                    cmd.Parameters.AddWithValue("chitno", model.ChitNo);

                    cmd.Parameters.AddWithValue("comment", model.Comment);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    conn.Close();



                }
            }
            return true;
        }
        public static bool InsertOtherFule(FuelOtherSale model)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertOtherFuel", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", model.VechileNumber);
                    cmd.Parameters.AddWithValue("fuletype", model.Fueltype);
                    cmd.Parameters.AddWithValue("quantity", model.FuelQuantity);
                    cmd.Parameters.AddWithValue("price", model.Price);
                    //cmd.Parameters.AddWithValue("otherfuel", model.OtherFule);
                    //cmd.Parameters.AddWithValue("otherprice", model.OtherPrice);
                    cmd.Parameters.AddWithValue("station_id", model.FuelStationID);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("serial_no", model.SerialNumber);
                   
                    cmd.Parameters.AddWithValue("comment", model.Comment);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    conn.Close();



                }
            }
            return true;
        }

        public static IEnumerable<FuelOtherModel> GetOtherFuelDetails(int userid)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllOtherFuelDetailsByDateAndUser", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("userid", userid);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    while (dr.Read())
                    {
                        // string vechilenumber, string fueltype, int quantity, decimal price, string fuelstationid, int serialno, string comment)
                        yield return new FuelOtherModel(

                            //  F.fueltype, A., A., A., A.
                            //string fuletype, decimal amount, int quantity, DateTime date, string vechileno)
                            (dr["vechilenumber"]).ToString(),
                           (dr["fueltype"]).ToString(),
                            (Convert.ToDecimal(dr["quantity"])),
                              (Convert.ToDecimal(dr["price"])),
                              (dr["fuel_pump_name"]).ToString(),
(Convert.ToInt32(dr["serial_no"])),
                              (dr["Comment"]).ToString(),

                         (Convert.ToDateTime(dr["insertdate"]))

                              );
                    }

                }
                conn.Close();
            }
        }


        public static IEnumerable<RecieveFuleModel> GetRecievingFuelDetails(int pumpid)
        {


            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetRecievingFuelDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                    while (dr.Read())
                    {

                        yield return new RecieveFuleModel(

                           //  F.fueltype, A., A., A., A.
                           //string fuletype, decimal amount, int quantity, DateTime date, string vechileno)
                           (dr["fueltype"]).ToString(),
                              (Convert.ToDecimal(dr["recievingprice"])),
                              (Convert.ToDecimal(dr["recievingquantity"])),
                              (Convert.ToDateTime(dr["recievingdate"])),
                               (dr["vechileno"]).ToString()

                              );
                    }

                }
                conn.Close();
            }
        }

        public static bool DALInsertDailySummary(tbl_finalStock model)


        {
            decimal ?[] cashquantity = new decimal?[] { model.cashquanity_diesel, model.cashquanity_petrol };
        // UpdaeCashQuantity(cashquantity,model.pumpid);
            try
            {
                using (var item = new GMOUMISEntity())
                {

                    item.tbl_finalStock.Add(model);
                    item.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public static tbl_finalStock DALGetOtherStocksDetailsSummary(int pumpid)


        {
            tbl_finalStock tblobj = new tbl_finalStock();
            //decimal?[] cashquantity = new decimal?[] { model.cashquanity_diesel, model.cashquanity_petrol };
            // UpdaeCashQuantity(cashquantity,model.pumpid);
            try
            {
                using (var item = new GMOUMISEntity())
                { 
                    DateTime startDate = DateTime.Now.Date;

                    DateTime endDate = DateTime.Now.AddDays(1).Date;

               
                    tblobj  = item.tbl_finalStock.Where(m => m.pumpid == pumpid && m.summary_date >= startDate && m.summary_date< endDate).FirstOrDefault();

                   
                }
            }
            catch (Exception ex)
            {
                return null;

            }
            return tblobj;
        }

        private static  void UpdaeCashQuantity(decimal?[] quantity,int pumpid)
        {
            for(int k = 0; k<quantity.Length; k++)
            {
                if(quantity!=null && k==0)
                {
                    UpdateCash((int)quantity[k], pumpid, 2);
                }
                if (quantity[k] != null && k == 1)
                {
                    UpdateCash((int)quantity[k], pumpid, 1);
                }

            }

        }

        private static void UpdateCash(int quantity, int stationid, int fueltype)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_updateCashFuelQuanity", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("stationid", stationid);
                    cmd.Parameters.AddWithValue("fueltypeid", fueltype);
                    cmd.Parameters.AddWithValue("quanity", quantity);


                    SqlDataReader dr;
                    conn.Open();
                    try
                    {
                        dr = cmd.ExecuteReader();
                        conn.Close();
                    }
                   
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }
        }

        public static IEnumerable< FuelStockReport> GetStocksReport(int pumpid)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_getFinalStocksReport", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pumpid", pumpid);
                 


                    SqlDataReader dr;
                    conn.Open();
                  
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {

                            yield return new FuelStockReport(
                               
                               //  F.fueltype, A., A., A., A.
                               //string fuletype, decimal amount, int quantity, DateTime date, string vechileno)
                           
                                
                                  (Convert.ToDecimal(dr["openingStock"])),
                                     (Convert.ToDecimal(dr["closingstock"])),
                                         (dr["fueltype"]).ToString(),
                                             (dr["fuel_pump_name"]).ToString(),
                                             (Convert.ToDouble(dr["Sales"])), 
                                             (dr["username"].ToString())


                                  );
                        }
                    

                }
            }
        }

        public static FuelStationModel GetFuelAdmin(string username)
        {
            int stationid; 
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetFuelAdmin", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("username", username);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    try
                    {
                        while (rd.Read())
                        {
                            return new FuelStationModel(
                            (Convert.ToInt32(rd["fuel_pump_id"])),
                             ((rd["fuel_pump_name"]).ToString()));
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                       
                    }
                    return null;
                }
            }

         
                }
            }//end of class
}//End of namespace
