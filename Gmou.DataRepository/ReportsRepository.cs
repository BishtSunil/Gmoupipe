﻿using Gmou.DataRepository.EntityRepository;
using Gmou.DomainModelEntities;
using Gmou.DomainModelEntities.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MoreLinq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository
{
    public class ReportsRepository
    {
        public static IEnumerable<BusTransactionModel> GetBusTransactionDetails(int busid)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetBusTransactionDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@busid", busid);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new BusTransactionModel(
                           dr["bus_number"].ToString(),

                          Convert.ToDecimal((dr["amount"])),
                          Convert.ToDateTime((dr["insertdate"])),
                        (dr["description"]).ToString());

                    }
                    conn.Close();

                }
            }

        }

        public static IEnumerable<VivraniReportUpdated> GetViraniList(int busid, DateTime date,DateTime enddate)
        {
            GMOULogger.Logger.Log(String.Format("Reports start date{0}  and End date {1}", date,enddate));
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetVivraniByDateandBus1", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@busid", busid);
                    cmd.Parameters.AddWithValue("@vivranistartdate", date);
                    cmd.Parameters.AddWithValue("@vivranisenddate", enddate);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date
                        //int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount
                        yield return new VivraniReportUpdated(
                              Convert.ToInt32((dr["cash_vivrani_id"])),

                          Convert.ToDecimal((dr["WayBillAmount"])),
                             Convert.ToDecimal((dr["VivraniAmount"])),

                                 Convert.ToDecimal((dr["RoundOff"]))
                                );


                    }
                    conn.Close();

                }
            }

        }

        public static IEnumerable<VivraniAllReportUpdated> GetAllViraniList( DateTime date, DateTime enddate)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetVivraniByDateandBus1_all", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    cmd.Parameters.AddWithValue("@vivranistartdate", date);
                    cmd.Parameters.AddWithValue("@vivranisenddate", enddate);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date
                        //int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount
                        yield return new VivraniAllReportUpdated(
                              Convert.ToInt32((dr["cash_vivrani_id"])),

                          Convert.ToDateTime((dr["vivrani_inserted_date"])),
                             (dr["bus_number"]).ToString(),

                                 Convert.ToDecimal((dr["VivraniAmount"]))
                                );


                    }
                    conn.Close();

                }
            }

        }

        public static List<StationDetailModel> GetStatiionDetail(int stationid)
        {
            List<StationDetailModel> lstStatio = new List<StationDetailModel>();
            using (var item = new GMOUMISEntity())
            {
                lstStatio = item.tblBusDetails.Select(m => new StationDetailModel { BusNumber = m.bus_number, Date = m.registration_date, busID = m.bus_id }).ToList();
                //var data = item.tblBusDetails.Join();
            }

            return lstStatio;
        }

        public static MontlyBusReport GetVivraniReports(int busnumber, int month, int year)
        {
            MontlyBusReport montllybusreoprt = new MontlyBusReport();
            List<VivraniReports> lstreports = new List<VivraniReports>();
            List<FuleReopts> lstfule = new List<FuleReopts>();
            using (var item = new GMOUMISEntity())
            {


                lstreports = (from r in item.tmp_cashvivrani
                              where r.bus_number == busnumber && r.vivrani_inserted_date.Month == month && r.vivrani_inserted_date.Year == year
                              group r by new
                              {
                                  r.cash_vivrani_id,
                                  r.vivrani_inserted_date

                              } into g

                              select new VivraniReports
                              {
                                  VivraniNumber = g.Key.cash_vivrani_id,
                                  VivraniDate = g.Key.vivrani_inserted_date,
                                  Amount = g.Sum(m => m.amount) + ( g.Max(k=>k.roundOffAmount)==null?0:g.Max(k=>k.roundOffAmount))

                                 
                                  
                              }).ToList();            //  lstreports = item.tmp_cashvivrani.Where(m => m.bus_number == busnumber && m.vivrani_inserted_date.Month == month && m.vivrani_inserted_date.Year == year).GroupBy(m => m.cash_vivrani_id).Select(ml => new VivraniReports { Amount = ml.Sum(c => c.amount),  VivraniNumber = ml.First().cash_vivrani_id }).ToList();

                montllybusreoprt.lstVivraniReports = lstreports;
                var data = (from r in item.tbl_ChitFuel
                            where r.vechilenumber == busnumber && r.inserteddate.Value.Month == month && r.inserteddate.Value.Year == year
                            group r by new
                            {
                                r.dieselchitno


                            } into g

                            select new FuleReopts
                            {
                                ChitNumber = g.Key.dieselchitno,
                                FuleDate = g.Select(m => m.inserteddate.Value).FirstOrDefault(),
                                Amount = g.Sum(m => m.price)

                            }).ToList();

                montllybusreoprt.lstFuleReposrts = data;
            }
            try
            {
                using (var item = new GMOUMISEntity())
                {

                    var data = (from b in item.tblBus
                                join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                join bins in item.tblBusInsuranceDeatils on b.busid equals bins.bus_id
                               join ba in  item.tbl_backDepo on b.busid equals ba.busid

                                where b.busid == busnumber
                                select new MontlyBusReport
                                {
                                    BusNumber = b.bus_number,
                                    AccountName = ba.bankaccount,
                                    AccountNumber =ba.bankname,
                                    BankName = ba.bankname,
                                    OwnerName= bo.bus_owner_name,
                                    Dipo = item.tblSets.Where(m => m.setid == bd.bus_operating_center).Select(m => m.station).FirstOrDefault()
                                }).FirstOrDefault();
                    montllybusreoprt.BusNumber = data.BusNumber;
                    montllybusreoprt.OwnerName = data.OwnerName;
                    montllybusreoprt.AccountName = data.AccountName;
                    montllybusreoprt.BankName = data.BankName;
                    montllybusreoprt.AccountName = data.AccountName;
                    montllybusreoprt.AccountNumber = data.AccountNumber;
                    montllybusreoprt.Dipo = data.Dipo;
                }
            }
            catch (Exception)
            {

                throw;
            }

            //            select m.bus_number,k.bus_owner_name, (select station from dbo.tblSets where setid=l.bus_operating_center) as Station
            //from dbo.tblBus m join dbo.tblBusOwnerDetails K on ( m.busid=K.bus_id) join 

            //dbo.tblBusDetails l on ( m.busid=l.bus_id)
            //where m.busid=205

            return montllybusreoprt;
        }

        public static List<DebitStatus> GetBusDebitStatus()
        {
            List<DebitStatus> st = new List<DebitStatus>();
            DebitStatus obj = null;
            DebitStatus resu = null;
            using (var item = new GMOUMISEntity())

            {
                var data = item.sp_GetButDebitStatusByDate().ToList(); ;

                int count = 0;

                foreach (var itema in data)
                {
                    if (count == 0)
                    {
                        if (itema.description.Equals("Fuel Filling"))
                        {
                            obj = new DebitStatus() { FuelFilling = itema.description, FuelFillingAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                        }
                        if (itema.description.Equals("Vivrani Generated"))
                        {
                            obj = new DebitStatus() { vivrani = itema.description, VivraniAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                        }
                        if (itema.description.Equals("Advance"))
                        {
                            obj = new DebitStatus() { Advance = itema.description, AdvanceAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                        }
                        count++;
                    }
                    else
                    {
                        if (st.Where(m => m.Busid == itema.busid).Count() > 0)
                        {
                            resu = st.Where(m => m.Busid == itema.busid).FirstOrDefault();
                            if (itema.description.Equals("Fuel Filling"))
                            {
                                resu.FuelFilling = itema.description;
                                resu.FuelFillingAmount = itema.Amount; resu.Busid = itema.busid;
                                resu.Busnumber = itema.bus_number;
                            }
                            if (itema.description.Equals("Vivrani Generated"))
                            {
                                resu.vivrani = itema.description; resu.VivraniAmount = itema.Amount;
                                resu.Busid = itema.busid;
                                resu.Busnumber = itema.bus_number;
                            }
                            if (itema.description.Equals("Advance"))
                            {
                                resu.Advance = itema.description; resu.AdvanceAmount = itema.Amount;
                                resu.Busid = itema.busid;
                                resu.Busnumber = itema.bus_number;
                            }
                        }
                        else
                        {
                            if (itema.description.Equals("Fuel Filling"))
                            {
                                obj = new DebitStatus() { FuelFilling = itema.description, FuelFillingAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                            }
                            if (itema.description.Equals("Vivrani Generated"))
                            {
                                obj = new DebitStatus() { vivrani = itema.description, VivraniAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                            }
                            if (itema.description.Equals("Advance"))
                            {
                                obj = new DebitStatus() { Advance = itema.description, AdvanceAmount = itema.Amount, Busid = itema.busid, Busnumber = itema.bus_number };
                            }

                        }
                    }
                    if (resu != null)
                    {
                        st.Add(resu);
                        resu = null;
                    }
                    else
                    {
                        st.Add(obj);
                        resu = null;
                    }

                }
                var d = st.DistinctBy(m => m.Busid).ToList();
                return st;
            }
        }

        public static IEnumerable<StationStocksInfo> GetStockReportsList(int pumpid, DateTime date)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetStocksByPumpIDandDate", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pumpid", pumpid);
                    cmd.Parameters.AddWithValue("@date", date);

                    conn.Open();
                   
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date
                        //int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount
                        yield return new StationStocksInfo(

                         (dr["fueltype"]).ToString(),
                            Convert.ToDecimal((dr["openingstock"])),
                          Convert.ToDecimal((dr["closingstock"])));

                    }
                    conn.Close();
                }
            }
        }


        public static IEnumerable<FuelReportDateWise> DALGetFuelReportDateWide(DateTime startdate , DateTime enddate)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetFuelReportsbyDates", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@startDate", startdate);
                    cmd.Parameters.AddWithValue("@endDate", enddate);
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date
                        //int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount
                        yield return new FuelReportDateWise(
                             (dr["StationName"].ToString()),
                        
                         
                         (dr["FuelType"].ToString()),
                          (Convert.ToDecimal(dr["Total"])));

                    }
                    conn.Close();
                }
            }
        }



        public static IEnumerable<BusPerformanceReport> DALGetBusPerformance(DateTime startdate,string order, int range)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetBusPerformance", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@sdate", startdate);
                    cmd.Parameters.AddWithValue("@order", order);
                    cmd.Parameters.AddWithValue("@range", range);
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // cash_vivrani_id,waybillno,waybillserialno,ticket_from,ticket_to,station_from,station_to,amount,vivrani_inserted_date
                        //int vivraniid, int wayserialnumb, int waybillnumber, int ticketfrom, int ticketto,string stationfrom, string stationto, decimal amount
                        yield return new BusPerformanceReport(
                             (dr["BusNumber"].ToString()),
                             (Convert.ToDecimal(dr["VivraniAmount"])),
                             (Convert.ToDecimal(dr["FuelAmount"])),


                          (Convert.ToDecimal(dr["Diff"])));

                    }
                    conn.Close();
                }
            }
        }


        public static bool DALInsertRaodWarrent(RoadWarrent model)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertRoadWarrent", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bus_id", model.BusNumber);
                    cmd.Parameters.AddWithValue("@amount", model.Amount);
                    cmd.Parameters.AddWithValue("@inserttedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("@datetime", model.InsertDate);
                    conn.Open();

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return true;
        }

    }
}
