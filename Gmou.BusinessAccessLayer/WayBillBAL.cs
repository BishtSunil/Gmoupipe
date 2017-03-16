using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gmou.Repository;
using Gmou.DomainModelEntities;
using Gmou.DataRepository;
using static Gmou.DomainModelEntities.WayBillViewModel;

namespace Gmou.BusinessAccessLayer
{
    public class WayBillBAL
    {
        public static vivraniDomainModel GetCashVivraiSerial(Vivirani model)
        {

            return WayBillRepository.GetVivraniSerial(model);
        }
        public static CashSheetModel GetCashSheetSerialNumber(Vivirani model)
        {
            return WayBillRepository.GetCashSheetSerial(model);
        }
        public static int BALGetGamanPatra()
        {
            return WayBillRepository.GetGamanPatra();
        }
        
        public static List<VivraniDetailsModel> AddVivraniDetails(VivraniDetails model)
        {
            model.vivranifuel.VivraniSerialID = model.vivraniSerialNumber;
            SaveVivraniFuelDeatils(model.vivranifuel);
            return WayBillRepository.SaveVivraniDetails(model).ToList();
        }

        public static int? GetPendingVivraniSerial(int empid, int department)
        {
            return WayBillRepository.GetPendingVivraniSerial(empid, department);
        }
        public static bool SaveVivraniFuelDeatils(FuelVivrani model)
        {
            return WayBillRepository.InsertVivraniFuleDetails(model);
        }
        public static List<CashSheetDetailsModel> AddCashsheetOtherExpenses(CashSheetDetails model)
        {
            return WayBillRepository.SaveCashOtherExpensesDetails(model).ToList();
        }
        public static GenerateCashSheetModel GetGeneratedCashSheet(int empId, int departmentid, DateTime date)
        {
            return WayBillRepository.GetCashSheetDetails(empId, departmentid,date);
        }
        public static BusOwnerName GetOwnerDetails(string busId)
        {
            return WayBillRepository.GetBusOwnNameDetails(busId).FirstOrDefault();
        }
        public static bool GetLastWayBill(int waybillbook, int waybillbookno, int busid)
        {
            var data = WayBillRepository.GetLastWayBillSerial(waybillbookno, busid);
            if (data == 0) { return true; }
            if (waybillbook - data == 1)
            {
                return true;
            }

            return false;

        }
        public static bool GetLastTicketSerial(int ticketserialno, int ticketno, int busid)
        {
            var data = WayBillRepository.GetLastTicketSerial(ticketno, busid);
            if (data == 0) { return true; }
            if (ticketserialno - data == 1)
            {
                return true;
            }

            return false;

        }

        public static BusDetailsModel GetAllBusses()
        {
            BusDetailsModel obj = new BusDetailsModel();
            obj.lstbusName = WayBillRepository.GetAllBuses().ToList();
            return obj;
        }
        public static bool SaveWaybillDetails(WayBillBookDetailsModel model)
        {
            var result = WayBillRepository.SaveWayBillDetails(model);
            if (result == 1)
            {
                return true;

            }
            return false;
        }

        public static List<BusJourneyHistory> GetBusHistory(int busID)
        {
            return WayBillRepository.GetBusjourneyDetails(busID).ToList();
        }

        public static bool BALSaveCouponDetails(CouponDetails model)
        {

            if (WayBillRepository.SaveCouponDetails(model) == 0)
            {
                return false;
            }
            return true;
        }

        public static CouponDetails BALGetCouponDetails(string page)
        {
            return WayBillRepository.GetCouponDetails(page).FirstOrDefault();
        }
        public static List<CouponDetails> BALGetAllCouponDetails()
        {
            return WayBillRepository.GetAllCouponDetails().ToList();
        }

        public static List<CouponDetails> BALSaveWayBillCouponDetails(CouponDetails model)
        {
            return WayBillRepository.SaveWayBillCouponDetails(model).ToList();
        }
        public static List<CouponDetails> BALGetAllWayBillCouponDetails(int waybillbookno, int waybillserialno)
        {
            return WayBillRepository.GetAllWayBillCouponDetails(waybillbookno, waybillserialno).ToList();
        }

        public static List<WayBillTicketViewModel> BALSaveWayBillEntry(WayBillTicketModel model)
        {
            //  List<WayBillViewModel> lstwaybillmodel;
            //if(model.NumberOfTicket>1 )
            //{
            //    lstwaybillmodel = new List<WayBillViewModel>();
            //    var lastticketnumber = model.TicketEnd;
            //    var tempfare = model.Fare;
            //    var startTicket= model.TicketStart;
            //    for (int a = startTicket; a <= lastticketnumber; a++)
            //    {
            //         model.TicketStart =startTicket++;
            //         model.TicketEnd = model.TicketStart;

            //         model.Fare = tempfare / model.NumberOfTicket;
            //        // model.NumberOfTicket;
            //lstwaybillmodel.Add(WayBillRepository.SaveWayBillDetailsEntry(model).FirstOrDefault());
            // }
            return WayBillRepository.SaveWayBillDetailsEntry(model).ToList();
        }

        public static bool BALGenerateCashVivrani(int empid, int busid,int vivraniid)
        {

            var _data = WayBillRepository.GenerateCashVivrani(empid, busid,vivraniid);
            if (_data == 1)
            { return true; }
            else { return false; }
        }

        public static List<CashVivraniDetails> BALShowCashVivrani(int empid, int busid)
        {

            return WayBillRepository.ShowCashVivrani(empid, busid).ToList();

        }

        public static bool BALSaveCashSheetSummary(CashSheetSummary model)
        {
            var _data = WayBillRepository.SaveCashSummary(model);
            if (_data == 1)
            { return true; }
            else { return false; }
        }

        public static bool BALCheckIfVivraniCreated(int busid, int empid)
        {

            return WayBillRepository.CheckIfVivraniCreated(empid, busid);
        
        }
        public static bool BALUpdateVivrani(int vivraniid, decimal  amount)
        {

            return WayBillRepository.UpdateVivrani(vivraniid, amount);
        }

        public static GamanPatraViewModel BALSaveGamanPatra(GamanPatraModel model)
        {

            return WayBillRepository.SaveGamanPatra(model);
        }
        public static GamanPatraViewModel BALGetGamanPatra(int id)
        {

            return WayBillRepository.GetGamanPatra(id);
        }

        public static List<RouteName> BALGetRouteName(string prefix)
        {

            return WayBillRepository.GetRouteName(prefix);
        }


}
}
