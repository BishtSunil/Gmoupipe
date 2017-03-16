using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Gmou.DomainModelEntities;

namespace Gmou.DataRepository
{
  public   class SupportingRepository
    {
      public static IEnumerable<PlacesModel> GetAllPlaces()
      {

          using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
          {
              using (var cmd = new SqlCommand("sp_GetAllPlaces", conn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  conn.Open();
                  SqlDataReader rd = cmd.ExecuteReader();
                  List<FuelType> lstfueltype = new List<FuelType>();
                  while (rd.Read())
                  {
                      yield return new PlacesModel(
                           (Convert.ToInt32(rd["serviceid"])),
                           ((rd["service_place"]).ToString()));
                            
                  }
                 
                  conn.Close();
              }
          }
      }

      public static IEnumerable<CashSheetSummaryModel> GetCashSheetChart()
      {

          using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
          {
              using (var cmd = new SqlCommand("sp_GetCashSheetSummary", conn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  conn.Open();
                  SqlDataReader rd = cmd.ExecuteReader();
                  List<CashSheetSummaryModel> lstfueltype = new List<CashSheetSummaryModel>();
                  while (rd.Read())
                  {
                      yield return new CashSheetSummaryModel(
                           (Convert.ToDecimal(rd["total_amount"])),
                           ((Convert.ToDateTime(rd["date"]))));

                  }

                  conn.Close();
              }
          }
      }

      public static IEnumerable<ServiceRoute> SaveServiceRoute(ServiceRoute model)
      {
          int serviceID;

          using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
          {
              using (var cmd = new SqlCommand("sp_InsertRouteService", conn))
              {

                  
             cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("station_from", model.StationFrom);
                  cmd.Parameters.AddWithValue("station_to", model.StationFrom);
                  cmd.Parameters.AddWithValue("distance", model.StationFrom);
                  cmd.Parameters.AddWithValue("fare", model.StationFrom);
                  cmd.Parameters.AddWithValue("inserted_by", model.StationFrom);



                  conn.Open();
                  serviceID = (int)cmd.ExecuteScalar();
                 

                  conn.Close();

                  
              }
          }

          return GetUpdatedServiceRoute(serviceID);
      }

      private static IEnumerable<ServiceRoute> GetUpdatedServiceRoute(int serviceid)
      {
          using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
          {
              using (var cmd = new SqlCommand("sp_GetCashSheetSummary", conn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;

                  conn.Open();
                  SqlDataReader rd = cmd.ExecuteReader();
                  List<CashSheetSummaryModel> lstfueltype = new List<CashSheetSummaryModel>();
                  while (rd.Read())
                  {
                      yield return new ServiceRoute(

                          ((rd["station_from"]).ToString()),
                          ((rd["station_to"]).ToString()),
                           (Convert.ToDecimal(rd["fare"])),
                           ((Convert.ToInt32(rd["distance"])))
                           
                            );

                  }

                  conn.Close();
              }
          }
      }
    }
}
