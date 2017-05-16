using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository.BulkInsert
{
    public class SQLBulkInsert
    {
     public static   SqlConnection con;
       static string consString = string.Empty;
         public static void SQLBulkInsertConnection()
        {
             consString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public static void BulkInsertTicketDetails(List<TicketDetailsModel> model)
        {
            DataTable dt = new DataTable();


            dt.Columns.AddRange(new DataColumn[8] { new DataColumn("waybillID", typeof(int)),
                        new DataColumn("stationfrom", typeof(string)),
                        new DataColumn("stationto",typeof(string)) ,
            new DataColumn("ticketstart", typeof(int)),
                        new DataColumn("ticketend", typeof(int)),
                          new DataColumn("noofticket", typeof(int)),
                        new DataColumn("ticketfare", typeof(int)),
                         new DataColumn("iscoupon", typeof(bool))
            });
            foreach (var row in model)
            {
               
                   
                    dt.Rows.Add(row.WayBillID, row.StationFrom, row.StationTo, row.StartTicket, row.EndTicket, row.NoofTicket, row.TicketFare,row.IsCoupon);
               
            }
            if (dt.Rows.Count > 0)
            {
                SQLBulkInsertConnection();
                using ( con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "[dbo].[tbl_waybillticketdetails]";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("waybillID", "waybillID");
                        sqlBulkCopy.ColumnMappings.Add("stationfrom", "stationfrom");
                        sqlBulkCopy.ColumnMappings.Add("stationto", "stationto");
                        sqlBulkCopy.ColumnMappings.Add("ticketstart", "ticketstart");
                        sqlBulkCopy.ColumnMappings.Add("ticketend", "ticketend");
                        sqlBulkCopy.ColumnMappings.Add("noofticket", "noofticket");
                        sqlBulkCopy.ColumnMappings.Add("ticketfare", "fare");
                        sqlBulkCopy.ColumnMappings.Add("iscoupon", "iscoupon");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
        }
    }
}
