using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Models
{
    public class EmployeeViewModel
    {
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        //public DateTime BirthDate { get; set; }
        //public string Address { get; set; }
        //public int City { get; set; }
        //public int Contact { get; set; }
        //public int EmergencyContact { get; set; }
        //public string Qaulifiaction { get; set; }
        //public DateTime joiningDate { get; set; }
        //public int EmployeeType { get; set; }
        public SelectList lstDepartment { get; set; }
        public SelectList lstOffices { get; set; }
        public int? OfficeID { get; set; }
        public SelectList lstEmployee { get; set; }

        public SelectList lststation { get; set; }
    }

    public class BusesModel
    {
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        //public DateTime BirthDate { get; set; }
        //public string Address { get; set; }
        //public int City { get; set; }
        //public int Contact { get; set; }
        //public int EmergencyContact { get; set; }
        //public string Qaulifiaction { get; set; }
        //public DateTime joiningDate { get; set; }
        //public int EmployeeType { get; set; }
        public SelectList lstBuses{ get; set; }
      

    }

    public class SaveEmployeeModel
    {
        private DateTime dt;
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Caste { get; set; }
        public string FatherName { get; set; }
        public string Village { get; set; }
        public string PostOffice { get; set; }
        public string Patti { get; set; }
        public string Thana { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public DateTime BirthDate
        {
            get
            {
                return dt;
            }
            set
            {
                dt = value;
            }
        }
        public DateTime JoiningDate { get; set; }
        public double Contact { get; set; }
        public string Qaulification { get; set; }
        public string LocalAddress { get; set; }
        public string IdentificationMark1 { get; set; }
        public string IdentificationMark2 { get; set; }
        public string EmergencyContact { get; set; }
        public string UAN { get; set; }
        public string PAN { get; set; }
        public string WorkingOffice { get; set; }
        public string WorkingDepartment { get; set; }
        public EmployeeFamliyModel employeeFamily { get; set; }
        public EmployeeFamilyDetailsModel employeeFamilyDetails { get; set; }
        public EmployeeServiceRecord employeeservicerecord { get; set; }
        public byte[] Document { get; set; }
        public byte[] Image { get; set; }
    }
    public class EmployeeFamliyModel
    {

        public string WifeName { get; set; }
        public string NextToKin { get; set; }
        public DateTime WifeDOB { get; set; }
        public string NomineeName { get; set; }
        public string NomineeIdentificationMark1 { get; set; }
        public string NomineeIdentificationMark2 { get; set; }
    }

    public class EmployeeFamilyDetailsModel
    {

        public string RelationShip { get; set; }
        public string MemberName { get; set; }

    }
    public class EmployeeServiceRecord
    {

        public string Designation { get; set; }
        public string workingOffice { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PayScale { get; set; }

        public string workingDepartment { get; set; }

    }
    public class Images
    {
        public string FileName { get; set; }
        public int Length { get; set; }
        public string ContentType { get; set; }
    }
    public class BusInsuranceNotificationViewModel
    {

        public int bus_id { get; set; }
        public string bus_number { get; set; }
        public string bus_owner_name { get; set; }
        public string InsuranceCompany { get; set; }
        public DateTime InsuranceValidity { get; set; }
    }
    public class EmployeeRegistrationViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } 
            public DateTime DateOfBirth { get; set; }
            public int Department { get; set; }  

    }
}