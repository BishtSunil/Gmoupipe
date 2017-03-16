using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using Gmou.DomainModelEntities;
using System.Data.SqlClient;
using static Gmou.DomainModelEntities.WayBillViewModel;
using Gmou.DataRepository.EntityRepository;

namespace Gmou.DataRepository
{
    public class WayBillRepository
    {
        public static vivraniDomainModel GetVivraniSerial(Vivirani model)
        {
            vivraniDomainModel vivranidomainmodel = new vivraniDomainModel();
            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_CashVivraniMaster", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("emp_id", model.UserID);
                    cmd.Parameters.AddWithValue("emp_department", model.DepartmentId);
                    cmd.Parameters.AddWithValue("Issubmited", model.IsSubmitted);
                    conn.Open();
                    vivranidomainmodel.VivraniSerialNumber = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllBusOwnersName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<BusOwner> lstownerdata = new List<BusOwner>();
                    while (rd.Read())
                    {


                        var ownerdata = new BusOwner { BusOwnerID = Convert.ToInt32(rd["bus_owner_ID"]), BusOwnerName = rd["bus_owner_name"].ToString() };
                        lstownerdata.Add(ownerdata);
                    }
                    vivranidomainmodel.BusOwner = lstownerdata;
                    conn.Close();
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAallBusList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<Bus> lstbus = new List<Bus>();
                    while (rd.Read())
                    {

                        var bus = new Bus { BusID = Convert.ToInt32(rd["busid"]), BusNumber = rd["bus_number"].ToString() };
                        lstbus.Add(bus);
                    }
                    vivranidomainmodel.bus = lstbus;
                    conn.Close();
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllfuelPumps", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<FulePump> lstfuelpump = new List<FulePump>();
                    while (rd.Read())
                    {

                        var fuelpump = new FulePump { FuelPumpID = Convert.ToInt32(rd["fuel_pump_id"]), PumpName = rd["fuel_pump_name"].ToString() };
                        lstfuelpump.Add(fuelpump);
                    }
                    vivranidomainmodel.fuelpump = lstfuelpump;
                    conn.Close();
                }

            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("SP_getAllfuelTypes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<FuelType> lstfueltype = new List<FuelType>();
                    while (rd.Read())
                    {

                        var fueltype = new FuelType { FuelTypeID = Convert.ToInt32(rd["fueltypeID"]), FuelTypeName = rd["fueltype"].ToString() };
                        lstfueltype.Add(fueltype);
                    }
                    vivranidomainmodel.fueltype = lstfueltype;
                    conn.Close();
                }
            }
            return vivranidomainmodel;
        }
        public static IEnumerable<VivraniDetailsModel> SaveVivraniDetails(VivraniDetails model)
        {
            int vivraniID;
            decimal totalamount = model.TotalAmount;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_SaveVivraniDetails", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vivraniSerialNumber", model.vivraniSerialNumber);
                    cmd.Parameters.AddWithValue("vechilenumber", Convert.ToInt32(model.VechileNumber));
                    cmd.Parameters.AddWithValue("ownername", Convert.ToInt32(model.OwnerName));
                    cmd.Parameters.AddWithValue("servicename", Convert.ToInt32(model.ServiceName));
                    cmd.Parameters.AddWithValue("waybillseial", model.WayBillSerialNumber);
                    cmd.Parameters.AddWithValue("waybillno", model.WayBillNumber);
                    cmd.Parameters.AddWithValue("ticketno", model.TicketNumber);
                    cmd.Parameters.AddWithValue("ticketserial", model.TicketSerailNumber);
                    cmd.Parameters.AddWithValue("jfrom", model.ServiceFrom);
                    cmd.Parameters.AddWithValue("jto", model.ServiceTo);
                    cmd.Parameters.AddWithValue("amount", Convert.ToDecimal(model.Amount));
                    cmd.Parameters.AddWithValue("emp_id", Convert.ToInt32(model.EmployeeID));
                    cmd.Parameters.AddWithValue("department_id", Convert.ToInt32(model.DepartmentID));
                    cmd.Parameters.AddWithValue("waybill_date", model.WayBillDate);
                    conn.Open();
                    try
                    {
                        vivraniID = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    conn.Close();
                }
            }
            return GetUpdatedData(vivraniID, totalamount);
        }
        private static IEnumerable<VivraniDetailsModel> GetUpdatedData(int id, decimal totalamount)
        {
            int vivraniID;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetUpdatedVivrani", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("entryID", id);
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

                        yield return new VivraniDetailsModel(totalamount,
                             (Convert.ToInt32(dr["vivraniSerialNumber"])),
                           dr["bus_number"].ToString(),

                           (dr["ownername"].ToString()),
                           dr["servicename"].ToString(),
                            (Convert.ToInt32(dr["waybillseial"])),
                        (Convert.ToInt32(dr["waybillno"])),
                         (Convert.ToInt32(dr["ticketno"])),
                        (Convert.ToInt32(dr["ticketserial"])),
                           (dr["jfrom"].ToString()),
                           dr["jto"].ToString(),

                           Convert.ToDecimal((dr["amount"]))
                       );
                    }
                    conn.Close();
                }
            }


        }

        public static int? GetPendingVivraniSerial(int employeeId, int departmentId)
        {
            int? vivraniID = 0;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_CheckPendingVivrani", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("empid", employeeId);
                    cmd.Parameters.AddWithValue("departmentid", departmentId);
                    conn.Open();
                    var data = cmd.ExecuteScalar();

                    conn.Close();
                    if (data != null)
                    {
                        vivraniID = (int)data;
                    }
                    return null;
                }
            }
            return vivraniID;
        }

        public static bool InsertVivraniFuleDetails(FuelVivrani model)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_Insert_Vivrani_fueldetails", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vivrani_serial_id", model.VivraniSerialID);
                    cmd.Parameters.AddWithValue("fuel_date", model.FuelDate);
                    cmd.Parameters.AddWithValue("fuel_chit_number", Convert.ToInt32(model.ChitNumber));
                    cmd.Parameters.AddWithValue("fuel_type", model.Fueltype);
                    cmd.Parameters.AddWithValue("fuel_station", model.FuelStation);
                    cmd.Parameters.AddWithValue("amount", model.Amount);
                    cmd.Parameters.AddWithValue("comment", "some Comment");
                    cmd.Parameters.AddWithValue("quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("inserted_by", model.InsertedBy);
                    cmd.Parameters.AddWithValue("fuel_price", model.FuelPrice);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteScalar();
                        conn.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                }
            }
        }

        public static CashSheetModel GetCashSheetSerial(Vivirani model)
        {
            CashSheetModel vivranidomainmodel = new CashSheetModel();
            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetCashSheetSerial", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("emp_id", model.UserID);
                    //cmd.Parameters.AddWithValue("emp_department", model.DepartmentId);
                    //cmd.Parameters.AddWithValue("Issubmited", model.IsSubmitted);
                    conn.Open();
                    vivranidomainmodel.CashSheetSerialNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAallBusList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<Bus> lstbus = new List<Bus>();
                    while (rd.Read())
                    {

                        var bus = new Bus { BusID = Convert.ToInt32(rd["busid"]), BusNumber = rd["bus_number"].ToString() };
                        lstbus.Add(bus);
                    }
                    vivranidomainmodel.bus = lstbus;
                    conn.Close();
                }
            }
            return vivranidomainmodel;
        }

        public static IEnumerable<CashSheetDetailsModel> SaveCashOtherExpensesDetails(CashSheetDetails model)
        {
            int cashSheetId;
            decimal totalamount = model.TotalAmount;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_SaveCashSheetOtherExpensesDetails", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("cashsheet_serial_number", model.CashSheetSerailNumber);
                    cmd.Parameters.AddWithValue("bus_id", Convert.ToInt32(model.BusID));
                    cmd.Parameters.AddWithValue("description", model.Description);
                    cmd.Parameters.AddWithValue("amount", model.Amount);
                    cmd.Parameters.AddWithValue("insertedby", Convert.ToInt32(model.InsertedBy));
                    cmd.Parameters.AddWithValue("department", Convert.ToInt32(model.DepartmentID));

                    conn.Open();
                    try
                    {
                        cashSheetId = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    conn.Close();
                }
            }
            return GetUpdatedDataCashSheet(cashSheetId, totalamount);
        }
        private static IEnumerable<CashSheetDetailsModel> GetUpdatedDataCashSheet(int id, decimal totalamount)
        {
            int vivraniID;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetCashSheetOtherExpenses", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("entryID", id);
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

                        yield return new CashSheetDetailsModel(totalamount,
                             (Convert.ToInt32(dr["cashsheet_serial_number"])),
                           dr["bus_number"].ToString(),

                           (dr["description"].ToString()),

                           Convert.ToDecimal((dr["amount"]))
                       );
                    }
                    conn.Close();
                }
            }


        }
        public static GenerateCashSheetModel GetCashSheetDetails(int empId, int departmentid,DateTime date)
        {
            GenerateCashSheetModel generateCashsheetmodel = new GenerateCashSheetModel();
            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_getAllVivraniListbyDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("date", date);
                    cmd.Parameters.AddWithValue("empid", empId);
                   
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<CashSheetVivraniDetails> lstCashSheetVivraniDetails = new List<CashSheetVivraniDetails>();
                    while (rd.Read())
                    {

                        var bus = new CashSheetVivraniDetails { VivraniSerialNumber = Convert.ToInt32(rd["cash_vivrani_id"]), BusNumber = rd["bus_number"].ToString(), Amount = Convert.ToDecimal(rd["Amount"]) };
                        lstCashSheetVivraniDetails.Add(bus);
                    }
                    generateCashsheetmodel.cashsheetvivranidetailslust = lstCashSheetVivraniDetails;
                }
            }
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_getAllOtherExpensesByDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("date", DateTime.Now);
                    cmd.Parameters.AddWithValue("empId", empId);
                    cmd.Parameters.AddWithValue("department", departmentid);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<CashSheetOtherExDetailsModel> lstCashSheetOtherExDetailsModel = new List<CashSheetOtherExDetailsModel>();
                    while (rd.Read())
                    {

                        var bus = new CashSheetOtherExDetailsModel
                        {
                            Amount = Convert.ToInt32(rd["amount"]),
                            BusNumber = rd["bus_number"].ToString(),
                            Description = rd["description"].ToString(),
                            CashSheetSerial = Convert.ToInt32(rd["cashsheet_serial_number"])
                        };
                        lstCashSheetOtherExDetailsModel.Add(bus);
                    }
                    generateCashsheetmodel.otherexepnseslist = lstCashSheetOtherExDetailsModel;
                    conn.Close();
                }
            }
            return generateCashsheetmodel;
        }

        public static IEnumerable<BusOwnerName> GetBusOwnNameDetails(string busid)
        {

            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetBusOwnerName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busnumber", busid);

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        yield return new BusOwnerName(
                            rd["bus_owner_name"].ToString(),
                         (Convert.ToInt32(rd["waybill_book_no"])));

                    }
                }
            }
        }

        public static int GetLastTicketSerial(int ticketserialno, int busid)
        {

            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_getLastTicketSerial", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", busid);
                    cmd.Parameters.AddWithValue("ticketno", ticketserialno);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null)
                    {
                        return serialID = (int)result;
                    }
                    else
                        return 0;
                }
            }

        }
        public static int GetLastWayBillSerial(int waybillbook, int busid)
        {

            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_getLastWayBillSerial", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vechilenumber", busid);
                    cmd.Parameters.AddWithValue("waybillno", waybillbook);
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    conn.Close();
                    if (result != null)
                    {
                        return serialID = (int)result;
                    }
                    else
                        return 0;
                }
            }

        }
        public static IEnumerable<Bus> GetAllBuses()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAallBusList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<Bus> lstbus = new List<Bus>();
                    while (rd.Read())
                    {
                        yield return new Bus(
                         Convert.ToInt32(rd["busid"]),
                       rd["bus_number"].ToString());


                    }

                    conn.Close();
                }
            }
        }
        public static int SaveWayBillDetails(WayBillBookDetailsModel model)
        {
            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertWayBillBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("waybillbookno", model.WillBillBookNo);
                    cmd.Parameters.AddWithValue("waybillserialno_start", model.WillBillBookSerialStart);
                    cmd.Parameters.AddWithValue("waybillserialno_end", model.WillBillBookSerialEnd);
                    cmd.Parameters.AddWithValue("bus_id", model.VechileNumber);
                    cmd.Parameters.AddWithValue("inserted_by", model.EmpoyeeID);
                    conn.Open();
                    var result = cmd.ExecuteScalar();

                    conn.Close();
                    return 0;
                }
            }

        }

        public static IEnumerable<BusJourneyHistory> GetBusjourneyDetails(int busId)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetBusJourneyHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("busID", busId);

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<Bus> lstbus = new List<Bus>();
                    while (rd.Read())
                    {
                        yield return new BusJourneyHistory(
                         Convert.ToInt32(rd["waybillno"]),
                         Convert.ToInt32(rd["waybillseial"]),
                       rd["jfrom"].ToString(),
                        rd["jto"].ToString(), rd["bus_number"].ToString(),
                       Convert.ToDateTime(rd["waybill_date"]));


                    }
                    conn.Close();
                }
            }
        }
        public static IEnumerable<WayBillBookDetailsModelDisplay> GetAllWayBillDetails()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetWayBillBookDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    List<Bus> lstbus = new List<Bus>();
                    while (rd.Read())
                    {
                        yield return new WayBillBookDetailsModelDisplay(
                               rd["bus_number"].ToString(),
                         Convert.ToInt32(rd["waybill_book_no"]),
                         Convert.ToInt32(rd["waybill_book_serial_start"]),
                          Convert.ToInt32(rd["waybill_book_serial_end"]),
                       Convert.ToDateTime(rd["date"]));


                    }
                    conn.Close();
                }
            }
        }


        public static int SaveCouponDetails(CouponDetails model)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertCouponDetails", conn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("station_from", model.StationFrom);
                    cmd.Parameters.AddWithValue("station_to", model.StationTo);
                    cmd.Parameters.AddWithValue("distance", model.Distance);
                    cmd.Parameters.AddWithValue("page", model.Page);
                    cmd.Parameters.AddWithValue("coupon_fare", model.Fare);
                    cmd.Parameters.AddWithValue("inserted_by", 1);
                    cmd.Parameters.AddWithValue("coupon_validity", model.Validity);
                    conn.Open();
                    var result = cmd.ExecuteScalar();

                    conn.Close();
                    return 0;
                }
            }

        }

        public static IEnumerable<CouponDetails> GetCouponDetails(string page)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetCouponDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("page", page);

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        yield return new CouponDetails(
                        rd["station_from"].ToString(),
                         rd["station_to"].ToString(),
                       rd["distance"].ToString(),
                       Convert.ToDecimal(rd["coupon_fare"]));


                    }
                    conn.Close();
                }
            }
        }

        public static IEnumerable<CouponDetails> GetAllCouponDetails()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllCouponDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        yield return new CouponDetails(
                        rd["station_from"].ToString(),
                         rd["station_to"].ToString(),
                       rd["distance"].ToString(),
                        rd["page"].ToString(),
                       Convert.ToDecimal(rd["coupon_fare"]),
                       Convert.ToDateTime(rd["coupon_validity"]));


                    }
                    conn.Close();
                }
            }
        }


        public static IEnumerable<CouponList> GetAllPages()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllPages", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        yield return new CouponList(
                       Convert.ToInt32(rd["station_id"]),
                       rd["page"].ToString());

                    }
                    conn.Close();
                }
            }
        }

        public static IEnumerable<CouponDetails> SaveWayBillCouponDetails(CouponDetails model)
        {
            int couponID;
            decimal totalamount = model.TotalAmount;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertWayBillCouponDetails", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("couponbookno", model.CouponBookNo);
                    cmd.Parameters.AddWithValue("couponserialno", model.CouponSerialNo);
                    cmd.Parameters.AddWithValue("waybill_serial_no", model.WayBillSerialNo);
                    cmd.Parameters.AddWithValue("waybill_book_no", Convert.ToInt32(model.WayBillBookNo));
                    cmd.Parameters.AddWithValue("station_from", model.StationFrom);
                    cmd.Parameters.AddWithValue("station_to", model.StationTo);
                    cmd.Parameters.AddWithValue("fare", model.Fare);
                    cmd.Parameters.AddWithValue("distance", 45);
                    cmd.Parameters.AddWithValue("inserted_by", model.InsertedBy);

                    conn.Open();
                    try
                    {
                        couponID = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    conn.Close();
                }
            }
            return GetUpdatedCouponData(couponID, totalamount);
        }
        private static IEnumerable<CouponDetails> GetUpdatedCouponData(int couponID, decimal totalamount)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (var cmd = new SqlCommand("sp_GetUpdatedWayBillCouponDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("couponId", couponID);
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

                        yield return new CouponDetails(totalamount,
                            (Convert.ToInt32(dr["couponbookno"])),
                            (Convert.ToInt32(dr["couponserialno"])),
                             (Convert.ToInt32(dr["distance"])),
                               (Convert.ToDecimal(dr["fare"])),
                           dr["station_from"].ToString(),

                           (dr["station_to"].ToString()));
                    }
                }
            }

        }

        public static IEnumerable<CouponDetails> GetAllWayBillCouponDetails(int waybillbookno, int waybillserialno)
        {
            decimal totalamount = 0;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (var cmd = new SqlCommand("sp_GetAllWayBillCouponDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("waybillbookno", waybillbookno);
                    cmd.Parameters.AddWithValue("waybillserialno", waybillserialno);

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

                        yield return new CouponDetails(totalamount,
                            (Convert.ToInt32(dr["couponbookno"])),
                            (Convert.ToInt32(dr["couponserialno"])),
                             (Convert.ToInt32(dr["distance"])),
                               (Convert.ToDecimal(dr["fare"])),
                           dr["station_from"].ToString(),

                           (dr["station_to"].ToString()));
                    }
                }
            }

        }


        ////New Waybill Logic////

        public static IEnumerable<WayBillTicketViewModel> SaveWayBillDetailsEntry(WayBillTicketModel model)
        {
            int WayBillID;
           int totalamount = model.TotalAmount;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertwayBillMaster", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("busnumber", model.BusNumber);
                  
                   
                    cmd.Parameters.AddWithValue("journeyfrom", model.JourneyFrom);
                    cmd.Parameters.AddWithValue("journeyto", model.JourneyTo);
                    cmd.Parameters.AddWithValue("waybillno", model.WayBillNo);
                    cmd.Parameters.AddWithValue("waybillserialno", model.WayBillSerialNo);

                     
                    cmd.Parameters.AddWithValue("waybilldate", DateTime.Now);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);



                    conn.Open();
                    try
                    {
                        WayBillID = (int)cmd.ExecuteScalar();
                        InsertTicketDetails(WayBillID, model.ticketdetails);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                    conn.Close();
                }
            }
            return GetUpdatedWayBill(WayBillID, totalamount);
        }
        private static IEnumerable<WayBillTicketViewModel> GetUpdatedWayBill(int waybillID, int totalamount)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (var cmd = new SqlCommand("sp_GetUpdatedWayBillDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@waybillID", waybillID);
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
                        //select A.[waybillno],A.[waybillserialno],B.[waybillID],B.[stationfrom],B.[stationto],B.[noofticket],B.[fare],B.[ticketstart],B.[ticketend]
                        yield return new WayBillTicketViewModel(totalamount,

                                                            Convert.ToInt32(dr["waybillserialno"]),
                                                    (Convert.ToInt32(dr["waybillno"])),
                                                 
                                                   (Convert.ToInt32(dr["fare"])),

                                                   (Convert.ToInt32(dr["noofticket"])),
                                                   (Convert.ToInt32(dr["ticketstart"])),
                                                     (Convert.ToInt32(dr["ticketend"])),
                                                     dr["stationfrom"].ToString(),
                                                     (dr["stationto"].ToString()));
                           
                             
                    }
                }
            }

        }

        private static void InsertTicketDetails(int waybillID,List<TicketDetailsModel> model)
        {
           
            model.ToList().ForEach(k => k.WayBillID = waybillID);
            BulkInsert.SQLBulkInsert.BulkInsertTicketDetails(model);

        }

        public static int GenerateCashVivrani(int empid, int busid, int vivraniid)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                //sp_GenerateCashVivrani
                using (var cmd = new SqlCommand("sp_GenerateCashVivrani_temo1", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("waybilldate", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("empId", empid);
                    cmd.Parameters.AddWithValue("bus_id", busid);
                    cmd.Parameters.AddWithValue("vivraniid", vivraniid);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteScalar();
                        conn.Close();
                        return 1;
                        
                    }
                    catch (Exception ex)
                    {

                        return 0;
                    }

                }
            }   

        }



        public static IEnumerable<CashVivraniDetails> ShowCashVivrani(int empid, decimal busid)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                using (var cmd = new SqlCommand("sp_ShowCashVivrani", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("waybilldate", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("empId", empid);
                    cmd.Parameters.AddWithValue("bus_id", busid);
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
                        yield return new CashVivraniDetails (
                              (Convert.ToDecimal(dr["amount"])),
                               dr["cash_vivrani_id"].ToString(),
                                  dr["bus_number"].ToString(),
                                   Convert.ToDateTime(dr["waybill_insertdate"]),
                                    Convert.ToInt32(dr["waybillno"]),
                            (Convert.ToInt32(dr["waybillserialno"])),
                                                          (Convert.ToInt32(dr["Ticket_from"])),
                             (Convert.ToInt32(dr["ticket_to"])),
                              dr["station_from"].ToString(),
                           (dr["station_to"].ToString()));
                    }
                }
            }

        }


        public static int SaveCashSummary(CashSheetSummary model)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GenerateCashVivrani", conn))
                {
                     
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("total_amount",model.TotalAmount);
                    cmd.Parameters.AddWithValue("insertedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("cashseetserialno", model.CashSheetSerialNo);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return 1;

                    }
                    catch (Exception ex)
                    {
                        return 0;

                    }
                }
            }
        }

        public static bool CheckIfVivraniCreated(int busid, int empid)
        {

            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_CheckVivraniCreated", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("empid", busid);
                    cmd.Parameters.AddWithValue("busid", empid);
                    conn.Open();
                    var result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }

        }

        public static bool UpdateVivrani(int vivraniid, decimal amount)
        {
            
            int serialID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_updateCashVivrani", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("vivraniId", vivraniid);
                    cmd.Parameters.AddWithValue("updatedamount", amount);
                    conn.Open();
                    var result = (int)cmd.ExecuteNonQuery();
                    conn.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }

        }



        public static int GetGamanPatra()
        {

            
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetGamanPatraSerial", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                                      conn.Open();
                                      var result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result > 0)
                    {
                        return result;
                    }
                    else
                        return 0;
                }
            }

        }

        public static GamanPatraViewModel SaveGamanPatra(GamanPatraModel model)
        {
            int gamanid;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertGamanPatra", conn))
                {
           
	

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("gamanserialId", model.GamanPatraID);
                    cmd.Parameters.AddWithValue("operating_station", model.StaionId);
                    cmd.Parameters.AddWithValue("issuedate", model.IssueDate);
                    cmd.Parameters.AddWithValue("bus_id", model.BusID);
                    cmd.Parameters.AddWithValue("bus_from", model.From);
                    cmd.Parameters.AddWithValue("bus_to", model.To);
                    cmd.Parameters.AddWithValue("departure_date", model.DepartureDate);
                    cmd.Parameters.AddWithValue("returning_date", model.ReturnDate);
                    cmd.Parameters.AddWithValue("gaman_amount", 0);
                    cmd.Parameters.AddWithValue("total_seats", model.Seats);
                    cmd.Parameters.AddWithValue("IsDailyservice", model.IsDailyService);
                    cmd.Parameters.AddWithValue("Permission_type", model.PermissionType);
                    cmd.Parameters.AddWithValue("inserted_by", 13);
                    cmd.Parameters.AddWithValue("advance_amount", model.AdvanceAmount);
                    cmd.Parameters.AddWithValue("remaining_amount", model.RemainingAmount);
                    cmd.Parameters.AddWithValue("total_amount", model.TotalAmount);


                    try
                    {
                        conn.Open();
                        gamanid = (int)cmd.ExecuteScalar();
                        conn.Close();
                        return GetGamanPatra(gamanid);

                    }
                    catch (Exception ex)
                    {
                        return null;

                    }
                }
            }
        }
   
        public static GamanPatraViewModel GetGamanPatra(int gamanpatraID)
        {
            GamanPatraViewModel gamamodel = new GamanPatraViewModel(); ;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetGamanPatra", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("gamanId",gamanpatraID);

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        gamamodel = new GamanPatraViewModel{  AdvanceAmount= Convert.ToDecimal(rd["advance_amount"]), BusID=rd["bus_number"].ToString(), DepartureDate =Convert.ToDateTime(rd["departure_date"]) ,
                             From= rd["bus_from"].ToString(), GamanPatraID= Convert.ToInt32(rd["gamanserialId"]), IsDailyService=Convert.ToBoolean(rd["IsDailyservice"]), IssueDate=Convert.ToDateTime(rd["issuedate"]), PermissionType=Convert.ToBoolean(rd["Permission_type"]),
                         RemainingAmount= Convert.ToDecimal(rd["remaining_amount"]), ReturnDate=Convert.ToDateTime(rd["returning_date"]), Seats=Convert.ToInt32(rd["total_seats"]), StaionId=rd["diponame"].ToString(), To=rd["bus_to"].ToString(), TotalAmount= Convert.ToDecimal(rd["total_amount"]),
                         SubmittedBy=rd["username"].ToString()};
                      
                    }

                    conn.Close();
                    return gamamodel;
                }
            }
            return null;
        }


        public static List<RouteName> GetRouteName(string prefix)
        {
            

            using (var item = new GMOUMISEntity())
            {

                var CityName = (from N in item.tbl_ShortName
                                where N.ShrtName.StartsWith(prefix)
                                select  new RouteName()
                                {
                                    RouteID = N.shortID,
                                    Route = N.ShrtName
                                }).ToList();
                return CityName;

            }
        }
    }//end of class 
}//end of namespace
