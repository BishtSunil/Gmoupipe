﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
    public class Employee
    {
        public int ID { get; set; }
        public string incID { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string EmergencyContact { get; set; }
        public string Qaulifiaction { get; set; }
        public DateTime joiningDate { get; set; }
        public int EmployeeType { get; set; }
        public string Office { get; set; }
        public bool MartailStatus { get; set; }
        public string Department { get; set; }
        public int lstDepartment { get; set; }
        public int lstOffices { get; set; }
        public int? OfficeID { get; set; }

        public Employee(int id, int employeeID, string first_name, string middlename, string lastname, DateTime dateofbirth, string contactno, string city, string address, DateTime dateofjoining, string employeetype, string workingdepartment, string workingoffice)
        {
            this.ID = id;
            //this.incID = Encrypt.encryptMethod(id.ToString());
            this.EmployeeId = employeeID;
            this.FirstName = first_name;
            this.LastName = lastname;
            this.MiddleName = middlename;
            this.BirthDate = dateofbirth;
            this.Contact = contactno;
            this.City = city;
            this.Address = address;
            this.joiningDate = dateofbirth;
            this.EmployeeType = EmployeeType;
            this.Department = workingdepartment;
            this.Office = workingoffice;
        }
        public Employee(int id, int employeeID, string first_name, string middlename, string lastname, DateTime dateofbirth, string contactno, string emergencyno, string city, string address, string qualification, DateTime dateofjoining, string employeetype, bool matrialstatus, string workingdepartment, string workingoffice)
        {
            this.ID = id;
            this.EmployeeId = employeeID;
            this.FirstName = first_name;
            this.LastName = lastname;
            this.MiddleName = middlename;
            this.BirthDate = dateofbirth;
            this.Contact = contactno;
            this.EmergencyContact = emergencyno;
            this.City = city;
            this.Address = address;
            this.Qaulifiaction = qualification;
            this.joiningDate = dateofbirth;
            this.EmployeeType = EmployeeType;
            this.MartailStatus = matrialstatus;
            this.Department = workingdepartment;
            this.Office = workingoffice;
        }


    }

    public class EmployeeDetails
    {
        public int Id { get; set; }
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
        public DateTime BirthDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public double Contact { get; set; }
        public string Qaulification { get; set; }
        public string LocalAddress { get; set; }
        public string City { get; set; }
        public string IdentificationMark1 { get; set; }
        public string IdentificationMark2 { get; set; }
        public string EmergencyContact { get; set; }
        public string UAN { get; set; }
        public string PAN { get; set; }
        public string WorkingOffice { get; set; }
        public string WorkingDepartment { get; set; }
        public EmployeeFamliy employeeFamily { get; set; }
        public EmployeeFamilyDetails employeeFamilyDetails { get; set; }
        public EmployeeServiceRecord EmployeeserviceRecord { get; set; }
        public byte[] Image { get; set; }
        public string ImageString { get; set; }

        public string Designation { get; set; }
        public string workingOffice { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PayScale { get; set; }
        public byte[] Document { get; set; }
        public string workingDepartment { get; set; }

    }
    public class EmployeeFamliy : EmployeeDetails
    {
        public int EmployeeID { get; set; }
        public string encrpID { get; set; }

        public string WifeName { get; set; }
        public string NextToKin { get; set; }
        public DateTime WifeDOB { get; set; }
        public string NomineeName { get; set; }
        public string NomineeIdentificationMark1 { get; set; }
        public string NomineeIdentificationMark2 { get; set; }
    }
    public class EmployeeServiceRecord
    {
        
        public string Designation { get; set; }
        public string workingOffice { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PayScale { get; set; }
        public byte[] Document { get; set; }
        public string workingDepartment { get; set; }
    }
    public class EmployeeFamilyDetails : EmployeeFamliy
    {
        public int EmployeeID { get; set; }
        public string RelationShip { get; set; }
        public string MemberName { get; set; }
        public EmployeeFamilyDetails() { }

        public EmployeeFamilyDetails(string firstname, string middlename, string lastname, string cast,
            string fathername, string village, string postoffice, string patti, string thana,
            string tehsil, string district, string state, DateTime dateofbirth,
            DateTime joiningdate, double contact, string qualification, string localaddress,
            string IdentificationMark1, string Identificationmark2, string UAN, string PAN,
            string wifename, string nexttokin, DateTime DOBwife, string nomineename,
            string nomineeIdentification1, string nomineeIdentification2, string relationship,
            string membername, string officename, string departmentname, byte[] image)
        {
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LastName = lastname;
            this.Caste = cast;
            this.FatherName = fathername;
            this.Village = village;
            this.PostOffice = postoffice;
            this.Patti = patti;
            this.Tehsil = tehsil;
            this.District = district;
            this.State = state;
            this.BirthDate = dateofbirth;
            this.JoiningDate = joiningdate;
            this.Contact = contact;
            this.Qaulification = qualification;
            this.LocalAddress = localaddress;
            this.IdentificationMark1 = IdentificationMark1;
            this.IdentificationMark2 = IdentificationMark2;
            this.UAN = UAN;
            this.PAN = PAN;
            this.WifeName = wifename;
            this.NextToKin = nexttokin;
            this.WifeDOB = DOBwife;
            this.NomineeName = nomineename;
            this.NomineeIdentificationMark1 = nomineeIdentification1;
            this.NomineeIdentificationMark2 = nomineeIdentification2;
            this.RelationShip = relationship;
            this.MemberName = membername;
            this.WorkingOffice = officename;
            this.WorkingDepartment = departmentname;
            this.ImageString = Convert.ToBase64String(image);

        }
        public EmployeeFamilyDetails(int id, int empId, string firstname, string middlename, string lastname, string cast,
          string fathername, string village, string postoffice, string patti, string thana,
          string tehsil, string district, string state, DateTime dateofbirth,
          DateTime joiningdate, double contact, string qualification, string localaddress,
          string identificationmark1, string identificationmark2, string UAN, string PAN,
          string wifename, string nexttokin, DateTime DOBwife, string nomineename,
          string nomineeIdentification1, string nomineeIdentification2, string relationship,
          string membername, string officeID, string departmentID)
        {

            this.encrpID = Encrypt.encryptMethod((id).ToString());
            this.EmployeeID = id;
            this.Id = empId;
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LastName = lastname;
            this.Caste = cast;
            this.FatherName = fathername;
            this.Thana = thana;
            this.Village = village;
            this.PostOffice = postoffice;
            this.Patti = patti;
            this.Tehsil = tehsil;
            this.District = district;
            this.State = state;
            this.BirthDate = dateofbirth;
            this.JoiningDate = joiningdate;
            this.Contact = contact;
            this.Qaulification = qualification;
            this.LocalAddress = localaddress;
            this.IdentificationMark1 = identificationmark1;
            this.IdentificationMark2 = identificationmark2;
            this.UAN = UAN;
            this.PAN = PAN;
            this.WifeName = wifename;
            this.NextToKin = nexttokin;
            this.WifeDOB = DOBwife;
            this.NomineeName = nomineename;
            this.NomineeIdentificationMark1 = nomineeIdentification1;
            this.NomineeIdentificationMark2 = nomineeIdentification2;
            this.RelationShip = relationship;
            this.MemberName = membername;
            this.WorkingOffice = officeID.ToString();
            this.WorkingDepartment = departmentID.ToString();

        }
        public EmployeeFamilyDetails(int id, string firstname, string middlename, string lastname, string cast,
         string fathername, string village, string postoffice, string patti, string thana,
         string tehsil, string district, string state, DateTime dateofbirth,
         DateTime joiningdate, double contact, string qualification, string localaddress,
         string identificationmark1, string identificationmark2, string UAN, string PAN,
         string wifename, string nexttokin, DateTime DOBwife, string nomineename,
         string nomineeIdentification1, string nomineeIdentification2, string relationship,
         string membername, string officeID, string departmentID, byte[] image, string designation, string workingoffice, DateTime appointmentdat, string paysacle, byte[] document, string workingDepartment)
        {

            this.encrpID = Encrypt.encryptMethod((id).ToString());
            this.EmployeeID = id;

            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LastName = lastname;
            this.Caste = cast;
            this.FatherName = fathername;
            this.Thana = thana;
            this.Village = village;
            this.PostOffice = postoffice;
            this.Patti = patti;
            this.Tehsil = tehsil;
            this.District = district;
            this.State = state;
            this.BirthDate = dateofbirth;
            this.JoiningDate = joiningdate;
            this.Contact = contact;
            this.Qaulification = qualification;
            this.LocalAddress = localaddress;
            this.IdentificationMark1 = identificationmark1;
            this.IdentificationMark2 = identificationmark2;
            this.UAN = UAN;
            this.PAN = PAN;
            this.WifeName = wifename;
            this.NextToKin = nexttokin;
            this.WifeDOB = DOBwife;
            this.NomineeName = nomineename;
            this.NomineeIdentificationMark1 = nomineeIdentification1;
            this.NomineeIdentificationMark2 = nomineeIdentification2;
            this.RelationShip = relationship;
            this.MemberName = membername;
            this.WorkingOffice = officeID.ToString();
            this.WorkingDepartment = departmentID.ToString();
            this.ImageString = Convert.ToBase64String(image);
            this.Document = document;
            this.Designation = designation;
            this.workingOffice = workingoffice;
            this.AppointmentDate = appointmentdat;
            this.PayScale = paysacle;
            this.workingDepartment = workingDepartment;
        }


    }

    public class EmployeeRegistartion
    {
        public int serial_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int department_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string created_by { get; set; }
        public System.DateTime created_on { get; set; }
        public DateTime date_of_birth { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
        
    }
    public class EmployeeRegistrationDetails
    {
         public int SerialID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string  DepartmentName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string Created_By { get; set; }
        public System.DateTime Created_On { get; set; }
        
        public EmployeeRegistrationDetails(int serialId, string firstname,string middlename,string lastname,string department, string username,string password, string created_by,DateTime created_on)
        {
            this.SerialID = serialId;
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LastName = lastname;
            this.DepartmentName = department;
            this.UserName = username;
            this.Password = password;
            this.Created_By = created_by;
            this.Created_On = created_on;

        }
    }

}
