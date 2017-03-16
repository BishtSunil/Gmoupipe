﻿using Gmou.DataRepository;
using Gmou.DataRepository.EntityRepository;
using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.BusinessAccessLayer
{
    public class BALFuel
    {
        public static List<FuelTypeModel> BALGetAllFuel()
        {
            return DataRepository.FuelRepository.GetAllFuelList().ToList();
        }
        public static List<BusList> BALGetBuses()
        {
            return DataRepository.FuelRepository.GetAllBusList().ToList();
        }
        public static List<FuelStationModel> BALGetAllStation()
        {
            return DataRepository.FuelRepository.GetAllFuelStations().ToList();
        }

        public static bool BALSaveCashMemoDetails(FuelCashOtherMemo model)
        {

            try
            {
                foreach (var item in model.listFuel)
                {
                    FuelCashMemo chitmodel = new FuelCashMemo()
                    {
                        VechileNumber = model.VechileNumber,

                        SerialNumber = model.SerialNumber,
                        OtherFule = "",
                        OtherPrice = 00,

                        FuelStationID = model.FuelStationID,
                        InsertedBy = model.InsertedBy,
                        //DieselBookno = model.DieselBookno,

                        Fueltype = item.Fueltype,
                        FuelQuantity = item.FuelQuantity,
                        Price = item.Price
                    };
                    DataRepository.FuelRepository.InsertCashMemo(chitmodel);
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
            //return DataRepository.FuelRepository.InsertCashMemo(model).ToList();
        }

        public static decimal BALGetFuelPrice(int pumpid, int fueltypeid)
        {

            return DataRepository.FuelRepository.GetFuelPrice(pumpid, fueltypeid);

        }

        public static List<FuelPriceListShowModel> BALGetAllFuelRate()
        {
            return DataRepository.FuelRepository.GetAllFuelRate().ToList();

        }

        public static bool BALSaveFuelDetails(FuelPriceListModel model)
        {

            return DataRepository.FuelRepository.SaveFuelDetails(model);
        }

        public static bool BALUpdatefuelDetail(UpdateFuelModel model)
        {
            return DataRepository.FuelRepository.UpdatefuelDetail(model);

        }
        public static bool BALInsertChit(ChitFuel model)
        {

            try
            {
                foreach (var item in model.listFuel)
                {
                    if (item.FuelQuantity > 0)
                    {
                        ChitFuelModelInsert chitmodel = new ChitFuelModelInsert()
                        {
                            VechileNumber = model.VechileNumber,
                            ChitNo = model.ChitNo,
                            Comment = model.Comment,
                            OtherFule = "",
                            OtherPrice = 00,

                            FuelStationID = model.FuelStationID,
                            InsertedBy = model.InsertedBy,
                            DieselBookno = model.DieselBookno,

                            Fueltype = item.Fueltype,
                            FuelQuantity = item.FuelQuantity,
                            Price = item.Price
                        };
                        DataRepository.FuelRepository.InsertChit(chitmodel);
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        public static List<ChitFuelModel> BALGetAllChitDetailsByUser(int userID)
        {
            return DataRepository.FuelRepository.GetAllChitDetailsByUser(userID).ToList();

        }
        public static List<ChitFuelModel> BALGetAllChitDetails(DateTime date, int pumpid)
        {
            return DataRepository.FuelRepository.GetAllChitDetails(date, pumpid).ToList();

        }
        public static bool BALInsertReceivingChit(FuelRecieve model)
        {
            return DataRepository.FuelRepository.InsertReceivingChit(model);

        }
        public static List<StockModel> BALGetStockData(int pumpid)
        {
            return DataRepository.FuelRepository.GetStockByPumpName(pumpid).ToList();

        }
        public static AllSalesModel BALGetChitFuelSalesDetails(int pumpid)
        {
            var data = FuelRepository.GetChitFuelSalesDetails(pumpid).ToList();
            List<ChitFuelSale> _temp = new List<ChitFuelSale>();

            var d = data.ToList();//.ToArray()


            var obj = new AllSalesModel();
            foreach (var item in d)
            {
                if (item.Fueltype == 1)
                {
                    obj.PetrolQuantity = item.PetrolQuantity;
                    obj.PetrolAmount = item.PetrolAmount;
                    obj.DeiselQuantity = item.DeiselQuantity;
                    obj.DeiselAmount = item.DeiselAmount;
                    obj.LubricantQuantity = item.LubricantQuantity;
                    obj.LubricantAmount = item.LubricantAmount;
                }
                if (item.Fueltype == 2)
                {
                    obj.OPetrolQuantity = item.PetrolQuantity;
                    obj.OPetrolAmount = item.PetrolAmount;
                    obj.ODeiselQuantity = item.DeiselQuantity;
                    obj.ODeiselAmount = item.DeiselAmount;
                    obj.OLubricantQuantity = item.LubricantQuantity;
                    obj.OLubricantAmount = item.LubricantAmount;
                }

                if (item.Fueltype == 3)
                {
                    obj.SPetrolQuantity = item.PetrolQuantity;
                    obj.SPetrolAmount = item.PetrolAmount;
                    obj.SDeiselQuantity = item.DeiselQuantity;
                    obj.SDeiselAmount = item.DeiselAmount;
                    obj.SLubricantQuantity = item.LubricantQuantity;
                    obj.SLubricantAmount = item.LubricantAmount;
                }



            }
            return obj;

        }
        public static FuelChashModel BALGetCashFuelSalesDetails(CashFuleSale model)
        {
            return FuelRepository.GetCashFuelSalesDetails(model);
        }
        public static bool BALInsertStaffFule(staffFuel model)
        {
            return FuelRepository.InsertStaffFule(model);

        }
        public static bool BALInsertOtherFule(OtherFuelModel model)
        {
            try
            {
                foreach (var item in model.listFuel)
                {
                    FuelOtherSale chitmodel = new FuelOtherSale()
                    {
                        VechileNumber = model.VechileNumber,
                        //ChitNo = model.ChitNo,
                        Comment = model.Comment,
                        OtherFule = "",
                        OtherPrice = 00,

                        FuelStationID = model.FuelStationID,
                        InsertedBy = model.InsertedBy,
                        // DieselBookno = model.DieselBookno,

                        Fueltype = item.Fueltype,
                        FuelQuantity = item.FuelQuantity,
                        Price = item.Price
                    };
                    DataRepository.FuelRepository.InsertOtherFule(chitmodel);
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        //return FuelRepository.InsertOtherFule(model);

        public static List<RecieveFuleModel> BALGetRecievingFuelDetails(int pumpid)
        {
            return FuelRepository.GetRecievingFuelDetails(pumpid).ToList();
        }
        public static List<FuelOtherModel> BALGetOtherFuelDetails(int userid)
        {
            return FuelRepository.GetOtherFuelDetails(userid).ToList();
        }

        public static bool BALInsertDailySummary(FinalSummary model)
        {
            tbl_finalStock obj = new tbl_finalStock()
            {
                pumpid = model.pumpid,
                smeter_petrol = model.smeter_petrol,
                emeter_petrol = model.emeter_petrol,
                ownersale_petrol = model.ownersale_petrol,
                cashsale_petrol = model.cashsale_petrol,
                staffsale_petrol = model.staffsale_petrol,
                othersale_petrol = model.othersale_petrol,
                ownerquanity_petrol = model.ownerquanity_petrol,
                cashquanity_petrol = model.cashquanity_petrol,
                staffquanity_petrol = model.staffquanity_petrol,
                otherquanity_petrol = model.otherquanity_petrol,
                smeter_diesel = model.smeter_diesel,
                emeter_diesel = model.emeter_diesel,
                ownersale_diesel = model.ownersale_diesel,
                cashsale_diesel = model.cashsale_diesel,
                staffsale_diesel = model.staffsale_diesel,
                othersale_diesel = model.othersale_diesel,
                ownerquanity_diesel = model.ownerquanity_diesel,
                cashquanity_diesel = model.cashquanity_diesel,
                staffquanity_diesel = model.staffquanity_diesel,
                otherquanity_diesel = model.otherquanity_diesel,
                ownersale_lub = model.ownersale_lub,
                cashsale_lub = model.cashsale_lub,
                staffsale_lub = model.staffsale_lub,
                othersale_lub = model.othersale_lub,
                ownerquanity_lub = model.ownerquanity_lub,
                cashquanity_lub = model.cashquanity_lub,
                staffquanity_lub = model.staffquanity_lub,
                otherquanity_lub = model.otherquanity_lub,
                summary_date = model.summary_date,
                inserted_by = model.inserted_by,

            };

           return FuelRepository.DALInsertDailySummary(obj);
        }


    }
}