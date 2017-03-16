using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gmou.Repository;
using Gmou.DataRepository.CustomRepository;
using Gmou.DataRepository;

namespace Gmou.BusinessAccessLayer
{
    public class BusBAL
    {

        public static bool AddBusRecords(SaveBusModel model)
        {
            return BusRepository.AddBusDetails(model);

        }

        public static List<BusListModel> GetAllDetails()
        {
            var result = BusRepository.GetAllBuses();
            foreach (var item in result)
            {

                item.busEncrpId = Encrypt.encryptMethod(item.bus_id.ToString());
                item.busEncrpNumber = Encrypt.encryptMethod(item.busEncrpNumber);
            }
            return result;
        }

        public static List<BusListModel> GetAllBusesBySearch(string condition, string type)
        {
            var result = BusRepository.GetAllBusesBySearch(condition, type);
            foreach (var item in result)
            {

                item.busEncrpId = Encrypt.encryptMethod(item.bus_id.ToString());
                item.busEncrpNumber = Encrypt.encryptMethod(item.busEncrpNumber);
            }
            return result;
        }

        public static SaveBusModel GetBusDetailsByNumber(string busNumber)
        {
            return BusRepository.BusDetailsByNumber(busNumber);
        }

        public static SaveBusModel EditBusDetails(int busId)
        {
            return BusRepository.EditBusDetails(busId);
        }
        public static bool CheckIfBusAvailable(string busnumber)
        {
            return BusRepository.CheckIfBusavailable(busnumber);
        }
        public static bool UpdateBusDeatils(SaveBusModel model)
        {

            return BusRepository.UpdateBusDetails(model);

        }
        public static List<BusInsuranceNotification> GetInsuranceDatesNear()
        {

            return BusRepository.InsuranceDatesNear();



        }
        public static List<WayBillBookDetailsModelDisplay> GetBusWayBillDetails()
        {
            return WayBillRepository.GetAllWayBillDetails().ToList();
        }
        public static bool DeleteBus(int busid)
        {
            return BusRepository.DeleteBus(busid);

        }

        public static BusDipoModel BALGetAllDipo()
        {

            return new BusDipoModel { DipoList = BusRepository.GetAllDipo() };
        }

    }
}
