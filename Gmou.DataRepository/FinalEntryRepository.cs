using Gmou.DomainModelEntities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository
{
    public class FinalEntryRepository
    {
        public static bool InsertFinalEntry(FinalEntry model)
        {
            int cashmemoid;

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_InsertFinalEntry", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("busId", model.BusID);
                    cmd.Parameters.AddWithValue("Transipment", model.Transipment);
                    cmd.Parameters.AddWithValue("wayCorrection", model.wayCorrection);
                    cmd.Parameters.AddWithValue("FareRefund", model.FareRefund);
                    cmd.Parameters.AddWithValue("Deficiency", model.Deficiency);
                    cmd.Parameters.AddWithValue("ElectionBill", model.ElectionBill);
                    cmd.Parameters.AddWithValue("FreeSeat", model.FreeSeat);
                    cmd.Parameters.AddWithValue("RoadWarrant", model.RoadWarrant);
                    cmd.Parameters.AddWithValue("Commission", model.Commission);
                    cmd.Parameters.AddWithValue("Owner", model.Owner);
                    cmd.Parameters.AddWithValue("Balance", model.Balance);
                    cmd.Parameters.AddWithValue("Payment", model.Payment);
                    cmd.Parameters.AddWithValue("Advance", model.Advance);
                    cmd.Parameters.AddWithValue("AdvanceStation", model.AdvanceStation);
                    cmd.Parameters.AddWithValue("Toll", model.Toll);
                    cmd.Parameters.AddWithValue("TRoadWarrant", model.TRoadWarrant);
                    cmd.Parameters.AddWithValue("RoadTax", model.RoadTax);
                    cmd.Parameters.AddWithValue("TTC", model.TTC);
                    cmd.Parameters.AddWithValue("PaiseDetain", model.PaiseDetain);
                    cmd.Parameters.AddWithValue("Super", model.Super);
                    cmd.Parameters.AddWithValue("Fine", model.Fine);
                    cmd.Parameters.AddWithValue("PermitFee", model.PermitFee);
                    cmd.Parameters.AddWithValue("Insurance", model.Insurance);
                    cmd.Parameters.AddWithValue("Advance2", model.Advance2);
                    cmd.Parameters.AddWithValue("BuildingFund", model.BuildingFund);
                    cmd.Parameters.AddWithValue("Loss", model.Loss);
                    cmd.Parameters.AddWithValue("Refund2", model.refund2);
                    cmd.Parameters.AddWithValue("RoadWarrantExp", model.RoadWarrantExp);
                    cmd.Parameters.AddWithValue("SupplyMail", model.SupplyMail);
                    cmd.Parameters.AddWithValue("OwnerSecurity", model.OwnerSecurity);
                    cmd.Parameters.AddWithValue("EntreeFees", model.EntreeFees);
                    cmd.Parameters.AddWithValue("VechileTranfer", model.VechileTranfer);
                    cmd.Parameters.AddWithValue("DistBoard", model.DistBoard);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    
                }
            }
            return true;
        }
    }
}
