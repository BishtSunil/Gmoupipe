using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository
{
    public class OfficeAdminRepository
    {
        public static bool InsertAdvnaceMemo(OfficeAdvanceAdmin model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertOwnerAdvance", conn))
                {
                     

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("busid", model.BusNumber);
                    cmd.Parameters.AddWithValue("reason", model.Reason);
                    cmd.Parameters.AddWithValue("date", model.InsertDate);
                    cmd.Parameters.AddWithValue("approvedby", model.InsertedBy);
                    cmd.Parameters.AddWithValue("amount", model.Amount);
                   
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

        public static IEnumerable<OfficeAdvanceAdminModel> GetAllAdvanceByDate()
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllAdvancebyDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        //select B.bus_number, A.advancedate ,A.amount, A.resaon from dbo.tbl_Owneradvnace A
                        //DateTime date, decimal amount, string  busnumber, string reason
                        yield return new OfficeAdvanceAdminModel(
                             (Convert.ToDateTime(rd["advancedate"])),
                              Convert.ToDecimal(rd["amount"]),
                            (rd["bus_number"]).ToString(),
                           
                              (rd["resaon"]).ToString());

                    }

                    conn.Close();
                }
            }
        }

    }
}
