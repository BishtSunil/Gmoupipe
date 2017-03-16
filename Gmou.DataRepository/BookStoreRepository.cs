using Gmou.DomainModelEntities.StoreBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository
{
   public class BookStoreRepository
    {

       public static bool InsertChitBook(ChitBookModel model)
       {

           using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
           {
               using (var cmd = new SqlCommand("sp_InsertChitBookDetails", conn))
               {
                 
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("busid", model.BusID);
                   cmd.Parameters.AddWithValue("chitbookno", model.ChitBookNumber);
                   cmd.Parameters.AddWithValue("chitserialstart", model.ChitSerialStart);
                   cmd.Parameters.AddWithValue("chhitserialend", model.ChitSerialEnd);
                   cmd.Parameters.AddWithValue("issuedby", model.IssuedBy);
                   cmd.Parameters.AddWithValue("comment", model.Comment);
                  
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


       public static IEnumerable<ChitBookDetails> GetAllChitBookDetails()
       {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllChitBookDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        yield return new ChitBookDetails(
                             (Convert.ToInt32(rd["chitbookid"])),
                              (rd["bus_number"]).ToString(),
                              ((Convert.ToInt32(rd["chitbookno"]))),

                              (Convert.ToInt32((rd["chitserialstart"]))),
                              Convert.ToInt32(rd["chhitserialend"]),
                              Convert.ToDateTime((rd["issuedate"])),
                        (rd["username"]).ToString(),
                        (rd["Comment"]).ToString());
                    }

                    conn.Close();
                }
            }

        }
      
      // public static 
    }//end of class
}//end of namespace
