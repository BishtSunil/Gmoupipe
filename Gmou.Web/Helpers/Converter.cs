using AutoMapper;
using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Gmou.Web.Helpers
{
    public class Converter
    {
        public static TDest ModelConverter<TSource, TDest>(TSource viewModel)
        {
            Mapper.CreateMap<TSource, TDest>();
            TDest result = Mapper.Map<TSource, TDest>(viewModel);

            return result;
        } 
        
        public static EmployeeDetails ConvertToDomainModel(SaveEmployeeModel model)
        {
            EmployeeDetails employeedetails = new EmployeeDetails();
            EmployeeFamilyDetails employeeFamilydetails = new EmployeeFamilyDetails();
            EmployeeFamliy employeeFamily = new EmployeeFamliy();
            employeedetails.FirstName = model.FirstName;
            employeedetails.MiddleName = model.MiddleName ;
            employeedetails.LastName = model.LastName ;
            employeedetails.Caste = model.Caste ;
            employeedetails.FatherName = model.FatherName;
            employeedetails.Village = model.Village;
            employeedetails.PostOffice = model.PostOffice;
            employeedetails.Patti = model.Patti;
            employeedetails.Tehsil = model.Tehsil;
            employeedetails.Thana = model.Thana;
            employeedetails.District = model.District;
            employeedetails.State = model.State;
            employeedetails.BirthDate = model.BirthDate;
            employeedetails.JoiningDate = model.JoiningDate;
            employeedetails.Contact = model.Contact;
            employeedetails.Qaulification = model.Qaulification;
            employeedetails.Qaulification = model.Qaulification;
            employeedetails.IdentificationMark1 = model.IdentificationMark1;
            employeedetails.IdentificationMark2 = model.IdentificationMark2;
            employeedetails.UAN = model.UAN;
            employeedetails.WorkingOffice = model.WorkingOffice;
            employeedetails.WorkingDepartment = model.WorkingDepartment;
            employeedetails.PAN = model.PAN;
            employeedetails.Thana = model.Thana;
            employeedetails.LocalAddress = model.LocalAddress;

            employeeFamily.WifeName = model.employeeFamily.WifeName;
            employeeFamily.NextToKin = model.employeeFamily.NextToKin;
            employeeFamily.WifeDOB = model.employeeFamily.WifeDOB;
            employeeFamily.NomineeName = model.employeeFamily.NomineeName;
            employeeFamily.NomineeIdentificationMark1 = model.employeeFamily.NomineeIdentificationMark1;
            employeeFamily.NomineeIdentificationMark2 = model.employeeFamily.NomineeIdentificationMark2;
            employeeFamilydetails.RelationShip = model.employeeFamilyDetails.RelationShip;
            employeeFamilydetails.MemberName = model.employeeFamilyDetails.MemberName;

            employeedetails.employeeFamilyDetails = employeeFamilydetails;
            employeedetails.employeeFamily = employeeFamily;
            employeedetails.Image = model.Image;
            employeedetails.Document = model.Document;
            employeedetails.Designation = model.employeeservicerecord.Designation;
            employeedetails.workingDepartment = model.employeeservicerecord.workingDepartment;
            employeedetails.workingOffice = model.employeeservicerecord.workingOffice;
            employeedetails.AppointmentDate = model.employeeservicerecord.AppointmentDate;
            employeedetails.PayScale = model.employeeservicerecord.PayScale;
            return employeedetails;


        }

        public static EmployeeRegistartion ConverToDomainModel(EmployeeRegistrationViewModel model)
        {
            EmployeeRegistartion employeeRegistartion = new EmployeeRegistartion();
            employeeRegistartion.first_name = model.FirstName;
            employeeRegistartion.middle_name = model.MiddleName;
            employeeRegistartion.last_name = model.LastName;
            employeeRegistartion.username = model.UserName;
            employeeRegistartion.password = model.Password;
            employeeRegistartion.confirmpassword = model.ConfirmPassword;
            employeeRegistartion.department_id = model.Department;
            employeeRegistartion.date_of_birth = model.DateOfBirth;
            return employeeRegistartion;
        }


        public static FinalSummary ConvertFinalSummary(FinalSummaryData model)
        {


            FinalSummary obj = new FinalSummary()
            {
                                 pumpid      =           model. pumpid,
         smeter_petrol =             model.smeter_petrol ,
   emeter_petrol =     model.emeter_petrol ,
     ownersale_petrol =         model. ownersale_petrol ,
     cashsale_petrol =      model. cashsale_petrol ,
     staffsale_petrol =     model. staffsale_petrol ,
     othersale_petrol =     model. othersale_petrol ,
     ownerquanity_petrol =      model. ownerquanity_petrol ,
     cashquanity_petrol=        model. cashquanity_petrol,
     staffquanity_petrol =      model. staffquanity_petrol ,
     otherquanity_petrol =      model. otherquanity_petrol ,
   smeter_diesel =     model.smeter_diesel ,
   emeter_diesel =     model.emeter_diesel ,
     ownersale_diesel=      model. ownersale_diesel,
     cashsale_diesel=       model. cashsale_diesel,
     staffsale_diesel =     model. staffsale_diesel ,
     othersale_diesel =     model. othersale_diesel ,
     ownerquanity_diesel=       model. ownerquanity_diesel,
     cashquanity_diesel =       model. cashquanity_diesel ,
     staffquanity_diesel =      model. staffquanity_diesel ,
     otherquanity_diesel=       model. otherquanity_diesel,
     ownersale_lub =        model. ownersale_lub ,
     cashsale_lub =     model. cashsale_lub ,
     staffsale_lub =        model. staffsale_lub ,
     othersale_lub =        model. othersale_lub ,
     ownerquanity_lub =     model. ownerquanity_lub ,
     cashquanity_lub=       model. cashquanity_lub,
     staffquanity_lub =     model. staffquanity_lub ,
     otherquanity_lub =     model. otherquanity_lub ,
   summary_date =      model.summary_date ,
   inserted_by=    13


            };
            return obj;
        }
    }

   
}