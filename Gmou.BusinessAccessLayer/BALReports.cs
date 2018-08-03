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

        
             public static List<StationStocksInfo> BALGetStockReportsList(int pumpid, DateTime date)
        {
            try
            {
                return DataRepository.ReportsRepository.GetStockReportsList(pumpid, date).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public static List<VivraniReportUpdated> BALGetViraniList(int busid, DateTime date, DateTime enddate)
        {
            try
            {
                return DataRepository.ReportsRepository.GetViraniList(busid, date, enddate).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public static List<VivraniAllReportUpdated> BALGetViraniListAll( DateTime date, DateTime enddate)
        {
            try
            {
                return DataRepository.ReportsRepository.GetAllViraniList( date, enddate).ToList();
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


        public static List<FuelReportDateWise> BALGetFuelReportDate(DateTime  sdate, DateTime edate)
        {
            return DataRepository.ReportsRepository.DALGetFuelReportDateWide(sdate, edate).ToList();

        }
        public static List<BusPerformanceReport> BALDALGetBusPerformance(DateTime sdate, string order,int range)
        {
            return DataRepository.ReportsRepository.DALGetBusPerformance(sdate, order,range).ToList();

        }
        
        public static bool BALInsertRaodWarrent(RoadWarrent model)
        {
            return DataRepository.ReportsRepository.DALInsertRaodWarrent(model);
        }
    }
}
