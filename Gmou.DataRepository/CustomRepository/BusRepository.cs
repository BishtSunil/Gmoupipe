﻿using Gmou.DataRepository.EntityRepository;
using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DataRepository.CustomRepository
{
    public class BusRepository
    {
        public static List<BusDipo> GetAllDipo()
        {
            List<BusDipo> dipomodel = new List<BusDipo>();

            try
            {
                using (var item = new GMOUMISEntity())
                {
                 dipomodel=    item.tbl_masterDepo.Select(m => new BusDipo { DipoID = m.dipoid, DipoName = m.diponame }).ToList();
                }
                return dipomodel;
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                return dipomodel;
            }
        }
        public static bool AddBusDetails(SaveBusModel model)
        {
            try
            {
                using (var item = new GMOUMISEntity())
                {
                    tblBu bs = new tblBu()
                    {
                        bus_number = model.bus_number,
                        setid = model.setid
                    };

                    item.tblBus.Add(bs);

                    item.SaveChanges();
                    int id = bs.busid;
                    //int id = Convert.ToInt32(item.tblBus.Select(m => m.busid).FirstOrDefault());
                    tblBusInsuranceDeatil businsurancedetails = new tblBusInsuranceDeatil()
                    {
                        bus_id = id,
                        company_name = model.company_name,
                        company_address = model.company_address,
                        validity = model.validity

                    };

                    item.tblBusInsuranceDeatils.Add(businsurancedetails);
                    item.SaveChanges();

                    tblBusOwnerDetail busownerDetails = new tblBusOwnerDetail()
                    {

                        bus_owner_name = model.bus_owner_name,
                        owner_father_name = model.owner_father_name,
                        owner_address = model.owner_address,
                        owner_contact_number = model.owner_contact_number,
                        bus_id = id,
                        entry_amount = model.entry_amount,
                        entry_reciept_number = model.entry_reciept_number,
                        payment_date = model.payment_date,
                        security_amount = model.security_amount,
                        security_money_reciept = model.security_money_reciept,
                        remaining_amount = model.remaining_amount,
                        gauranter1 = model.gauranter1,
                        gauranter1_bus = model.gauranter1_bus,
                        gauranter2 = model.gauranter2,
                        gauranter2_bus = model.gauranter2_bus,
                        building_fund = model.building_fund,
                        old_bus_number = model.old_bus_number,
                        old_bus_owner_name = model.old_bus_owner_name,
                        previous_building_fund = model.previous_building_fund,
                        emi = model.emi


                    };
                    item.tblBusOwnerDetails.Add(busownerDetails);
                    tblBusDetail busdetails = new tblBusDetail()
                    {
                        bus_id = id,
                        bus_number = model.bus_number,
                        bus_operating_center = model.bus_operating_center,
                        chesis_number = model.chesis_number,
                        engine_number = model.engine_number,
                        fitness = model.fitness,
                        last_modified_date = DateTime.Now,
                        registration_date = model.registration_date,
                        permit_number = model.permit_number,
                        model = model.model,
                        seats = model.seats,
                        road_tax = model.road_tax,
                        isDeleted = false,
                        last_modified_by = "Admin",
                        bus_registration_type = model.bus_registration_type,
                        bus_registration_number = model.bus_registration_number

                    };
                    item.tblBusDetails.Add(busdetails);
                    item.SaveChanges();
                }
                return true;
            }

            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }

        }

        public static List<BusListModel> GetAllBuses()
        {

            List<BusListModel> employeeList = new List<BusListModel>();
            try
            {
                using (var item = new GMOUMISEntity())
                {

                    employeeList = (from b in item.tblBus
                                    join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                    join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                    select new BusListModel { busEncrpNumber = b.bus_number, bus_number = b.bus_number, bus_id = b.busid, bus_operating_center = bd.bus_operating_center, seats = bd.seats, bus_owner_name = bo.bus_owner_name, registration_date = bd.registration_date }).ToList();
                    return employeeList;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return employeeList;

            }

        }


        public static List<BusListModel> GetAllBusesBySearch(string condition, string type)
        {
            int con = 0;
            if (type == "seats" || type == "setid")
            {
                con = Convert.ToInt32(condition);
            }

            List<BusListModel> employeeList = new List<BusListModel>();
            try
            {
                using (var item = new GMOUMISEntity())
                {

                    employeeList = (from b in item.tblBus
                                    join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                    join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                    select new BusListModel { busEncrpNumber = b.bus_number, bus_number = b.bus_number, bus_id = b.busid, bus_operating_center = bd.bus_operating_center, seats = bd.seats, bus_owner_name = bo.bus_owner_name, model = bd.model }).ToList();
                    if (type == "seats")
                    {
                        employeeList = employeeList.Where(m => m.seats == con).ToList();
                    }
                    else if (type == "setid")
                    {
                        employeeList = employeeList.Where(m => m.setid == con).ToList();
                    }
                    else
                    {
                        employeeList = employeeList.Where(m => m.model == condition).ToList();
                    }
                    return employeeList;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return employeeList;

            }

        }

        public static SaveBusModel BusDetailsByNumber(string busnumber)
        {
            SaveBusModel savebusmodel;
            try
            {
                savebusmodel = new SaveBusModel();
                using (var item = new GMOUMISEntity())
                {

                    var busID = item.tblBus.Where(i => i.bus_number == busnumber).Select(i => i.busid).SingleOrDefault();
                    if (busID == 0)
                    { return null; }

                    savebusmodel = (from b in item.tblBus
                                    join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                    join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                    join bins in item.tblBusInsuranceDeatils on b.busid equals bins.bus_id
                                    where b.busid == busID
                                    select new SaveBusModel
                                    {
                                        bus_number = b.bus_number,
                                        bus_operating_center = bd.bus_operating_center,
                                        registration_date = bd.registration_date,
                                        permit_number = bd.permit_number,
                                        model = bd.model,
                                        seats = bd.seats,
                                        chesis_number = bd.chesis_number,
                                        engine_number = bd.engine_number,
                                        setid = b.setid,
                                        road_tax = bd.road_tax,
                                        fitness = bd.fitness,
                                        company_name = bins.company_name,
                                        company_address = bins.company_address,
                                        validity = bins.validity,
                                        bus_owner_name = bo.bus_owner_name,
                                        owner_father_name = bo.owner_father_name,
                                        owner_address = bo.owner_address,
                                        owner_contact_number = bo.owner_contact_number,
                                        entry_amount = bo.entry_amount,
                                        entry_reciept_number = bo.entry_reciept_number,
                                        payment_date = bo.payment_date,
                                        security_amount = bo.security_amount,
                                        security_money_reciept = bo.security_money_reciept,
                                        remaining_amount = bo.remaining_amount,
                                        gauranter1 = bo.gauranter1,
                                        gauranter2 = bo.gauranter2,
                                        gauranter1_bus = bo.gauranter1_bus,
                                        gauranter2_bus = bo.gauranter2_bus,
                                        building_fund = bo.building_fund,
                                        previous_building_fund = bo.previous_building_fund,
                                        old_bus_owner_name = bo.old_bus_owner_name,
                                        old_bus_number = bo.old_bus_number,
                                        bus_registration_type = bd.bus_registration_type,
                                        emi = bo.emi,
                                        bus_registration_number = bd.bus_registration_number

                                    }).FirstOrDefault();

                }
                return savebusmodel;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public static SaveBusModel EditBusDetails(int id)
        {
            SaveBusModel savebusmodel;
            try
            {
                savebusmodel = new SaveBusModel();
                using (var item = new GMOUMISEntity())
                {
                    //var busID = item.tblBus.Where(i => i.bus_number == busnumber).Select(i => i.busid).SingleOrDefault();

                    savebusmodel = (from b in item.tblBus
                                    join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                    join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                    join bins in item.tblBusInsuranceDeatils on b.busid equals bins.bus_id
                                    where b.busid == id
                                    select new SaveBusModel
                                    {
                                        bus_number = b.bus_number,
                                        bus_operating_center = bd.bus_operating_center,
                                        registration_date = bd.registration_date,
                                        permit_number = bd.permit_number,
                                        model = bd.model,
                                        seats = bd.seats,
                                        chesis_number = bd.chesis_number,
                                        engine_number = bd.engine_number,
                                        road_tax = bd.road_tax,
                                        fitness = bd.fitness,
                                        company_name = bins.company_name,
                                        company_address = bins.company_address,
                                        validity = bins.validity,
                                        bus_owner_name = bo.bus_owner_name,
                                        owner_father_name = bo.owner_father_name,
                                        owner_address = bo.owner_address,
                                        owner_contact_number = bo.owner_contact_number,
                                        entry_amount = bo.entry_amount,
                                        entry_reciept_number = bo.entry_reciept_number,
                                        payment_date = bo.payment_date,
                                        security_amount = bo.security_amount,
                                        security_money_reciept = bo.security_money_reciept,
                                        remaining_amount = bo.remaining_amount,
                                        gauranter1 = bo.gauranter1,
                                        gauranter2 = bo.gauranter2,
                                        gauranter1_bus = bo.gauranter1_bus,
                                        gauranter2_bus = bo.gauranter2_bus,
                                        building_fund = bo.building_fund,
                                        previous_building_fund = bo.previous_building_fund,
                                        old_bus_owner_name = bo.old_bus_owner_name,
                                        old_bus_number = bo.old_bus_number,
                                        bus_registration_type = bd.bus_registration_type,
                                        emi = bo.emi,
                                        bus_registration_number = bd.bus_registration_number

                                    }).FirstOrDefault();
                }
                return savebusmodel;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static bool UpdateBusDetails(SaveBusModel model)
        {

            try
            {
                using (var item = new GMOUMISEntity())
                {
                    var resulytblbus = item.tblBus.Single(c => c.busid == model.busId);
                    resulytblbus.setid = model.setid;
                    resulytblbus.bus_number = model.bus_number;



                    item.SaveChanges();

                    //int id = Convert.ToInt32(item.tblBus.Select(m => m.busid).FirstOrDefault());
                    // var busdetails = item.tblBusDetails.Single(c => c.bus_id == model.bus_id);
                    //tblBusInsuranceDeatil businsurancedetails = new tblBusInsuranceDeatil()
                    //{
                    var busInsurance = item.tblBusInsuranceDeatils.Single(c => c.bus_id == model.busId);
                    busInsurance.company_name = model.company_name;
                    busInsurance.company_address = model.company_address;
                    busInsurance.validity = model.validity;
                    item.SaveChanges();
                    //};

                    //item.tblBusInsuranceDeatils.Add(businsurancedetails);
                    //item.SaveChanges();
                    var busOwner = item.tblBusOwnerDetails.Single(c => c.bus_id == model.busId);
                    //tblBusOwnerDetail busownerDetails = new tblBusOwnerDetail()
                    //{

                    busOwner.bus_owner_name = model.bus_owner_name;
                    busOwner.owner_father_name = model.owner_father_name;
                    busOwner.owner_address = model.owner_address;
                    busOwner.owner_contact_number = model.owner_contact_number;
                    busOwner.entry_reciept_number = model.entry_reciept_number;
                    busOwner.entry_amount = model.entry_amount;
                    busOwner.payment_date = model.payment_date;
                    busOwner.security_amount = model.security_amount;
                    busOwner.security_money_reciept = model.security_money_reciept;
                    busOwner.remaining_amount = model.remaining_amount;
                    busOwner.gauranter1 = model.gauranter1;
                    busOwner.gauranter1_bus = model.gauranter1_bus;
                    busOwner.gauranter2 = model.gauranter2;
                    busOwner.gauranter2_bus = model.gauranter2_bus;
                    busOwner.old_bus_number = model.old_bus_number;
                    busOwner.old_bus_owner_name = model.old_bus_owner_name;
                    busOwner.building_fund = model.building_fund;
                    busOwner.previous_building_fund = model.previous_building_fund;
                    busOwner.emi = model.emi;
                    item.SaveChanges();
                    // };
                    //item.tblBusOwnerDetails.Add(busownerDetails);
                    var busDeatils = item.tblBusDetails.Single(c => c.bus_id == model.busId);

                    busDeatils.bus_number = model.bus_number;
                    busDeatils.bus_operating_center = model.bus_operating_center;
                    busDeatils.chesis_number = model.chesis_number;
                    busDeatils.engine_number = model.engine_number;
                    busDeatils.fitness = model.fitness;
                    busDeatils.last_modified_date = DateTime.Now;
                    busDeatils.registration_date = model.registration_date;
                    busDeatils.permit_number = model.permit_number;
                    busDeatils.model = model.model;
                    busDeatils.seats = model.seats;
                    busDeatils.road_tax = model.road_tax;
                    busDeatils.isDeleted = false;
                    busDeatils.last_modified_by = "Admin";
                    busDeatils.bus_registration_type = model.bus_registration_type;

                    busDeatils.bus_registration_number = model.bus_registration_number;

                    item.SaveChanges();
                }
                return true;
            }

            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }
        }

        public static bool CheckIfBusavailable(string busnumber)
        {
            using (var item = new GMOUMISEntity())
            {
                var count = item.tblBus.Where(i => i.bus_number == busnumber).Count();
                if (count > 0)
                {
                    return true;
                }
                return false;

            }

        }

        public static List<BusInsuranceNotification> InsuranceDatesNear()
        {
            List<BusInsuranceNotification> employeeList = new List<BusInsuranceNotification>();
            var expecteddate = DateTime.Now.AddDays(10);
            using (var item = new GMOUMISEntity())
            {
                employeeList = (from bI in item.tblBusInsuranceDeatils
                                join bs in item.tblBus on bI.bus_id equals bs.busid
                                join bd in item.tblBusDetails on bs.busid equals bd.bus_id
                                join bo in item.tblBusOwnerDetails on bs.busid equals bo.bus_id
                                where bI.validity <= expecteddate
                                select new BusInsuranceNotification
                                {

                                    bus_number = bs.bus_number,
                                    bus_id = bs.busid,
                                    bus_owner_name = bo.bus_owner_name,
                                    InsuranceCompany = bI.company_name,
                                    InsuranceValidity = bI.validity,
                                }).ToList();
                return employeeList;
            }

        }


        public static bool DeleteBus(int id)
        {
            try
            {

                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_DeleteBus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("busid", id);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();



                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
                return false;
            }



        }

        public static List<BusListModel> GetAllBusesDepoStatus()
        {

            List<BusListModel> employeeList = new List<BusListModel>();
            try
            {
                using (var item = new GMOUMISEntity())
                {

                    employeeList = (from b in item.tblBus
                                    join bd in item.tblBusDetails on b.busid equals bd.bus_id
                                    join bo in item.tblBusOwnerDetails on b.busid equals bo.bus_id
                                    join bdeop in item.tbl_masterDepo on bd.bus_operating_center equals bdeop.dipoid
                                    select new BusListModel { busEncrpNumber = b.bus_number, bus_number = b.bus_number, bus_id = b.busid, bus_operating_center = bd.bus_operating_center, seats = bd.seats, bus_owner_name = bo.bus_owner_name, registration_date = bd.registration_date }).ToList();
                    return employeeList;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return employeeList;

            }

        }

    }
}
