using Gmou.DomainModelEntities;
using Gmou.DomainModelEntities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.BusinessAccessLayer
{
    public class BALReports
    {

        public static List<BusTransactionModel> BALGetBusTransactionDetails(int pumpid)
        {

            return DataRepository.ReportsRepository.GetBusTransactionDetails(pumpid).ToList();
        }
        public static List<VivraniReport> BALGetViraniList(int busid, DateTime date)
        {
            try
            {
                return DataRepository.ReportsRepository.GetViraniList(busid, date).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        //GetViraniList


        public static List<DebitStatus> BALGetBusDebitStatus()

        {

            var result = DataRepository.ReportsRepository.GetBusDebitStatus();

            foreach (var item in result)
            {
                if (item.Advance != null && item.vivrani != null && item != null)
                {
                    var amount = Convert.ToDecimal(item.VivraniAmount) - Convert.ToDecimal(item.FuelFillingAmount) + Convert.ToDecimal(item.AdvanceAmount);
                    if (amount < 0)
                    {
                        item.IsDebit = true;
                    }
                    item.DebitAmount = amount;
                }
                if (item.Advance != null && item.vivrani != null && item.FuelFilling == null)
                {
                    var amount = Convert.ToDecimal(item.VivraniAmount) - Convert.ToDecimal(item.FuelFillingAmount) + Convert.ToDecimal(0.00);
                    if (amount < 0)
                    {
                        item.IsDebit = true;
                    }
                    item.DebitAmount = amount;

                }
                if (item.Advance != null && item.vivrani == null && item.FuelFilling == null)
                {
                    var amount = Convert.ToDecimal(0.0) - Convert.ToDecimal(0.0) + Convert.ToDecimal(item.AdvanceAmount);
                    if (amount < 0)
                    {
                        item.IsDebit = true;
                    }
                    item.DebitAmount = amount;
                }
                if (item.Advance == null && item.vivrani != null && item.FuelFilling != null)
                {
                    var amount = Convert.ToDecimal(item.VivraniAmount) - Convert.ToDecimal(item.FuelFillingAmount) + Convert.ToDecimal(0.0);
                    if (amount < 0)
                    {
                        item.IsDebit = true;
                    }
                    item.DebitAmount = amount;
                }
                if (item.Advance == null && item.vivrani == null && item.FuelFilling != null)
                {
                    var amount = Convert.ToDecimal(0.0) - Convert.ToDecimal(item.FuelFillingAmount) + Convert.ToDecimal(0.0);
                   if(amount<0)
                    {
                        item.IsDebit = true;
                    }
                    item.DebitAmount = amount;
                }
            }
            return result;
        }
    }
}
