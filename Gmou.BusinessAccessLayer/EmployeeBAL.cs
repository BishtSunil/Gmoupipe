﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gmou.DomainModelEntities;
using Gmou.Repository;
using System.Data.SqlClient;
 

namespace Gmou.BusinessAccessLayer
{
    public class EmployeeBAL
    {
        public static List<Department> GetDepartmentList()
        {

          return  EmployeeRepository.GetDepartments().ToList();
        }

        public static List<Office>GetOfficeList()
        {
            return EmployeeRepository.GetOffice().ToList();
        }

        public static List<EmployeeFamilyDetails> GetAllEmployeelist()
        {
            return EmployeeRepository.GetAllEmployee().ToList();
        }
        public static List<EmployeeFamilyDetails> GetEmployeeDetail(int id)
        {
            return EmployeeRepository.GetEmployeeDetails(id).ToList();
        }

        public static List<EmployeeFamilyDetails> EditEmployeeDetail(int id)
        {
            var temp= EmployeeRepository.EditEmployeeDetails(id).ToList();
           // var result = temp.
            return temp;
        }
     public static int SaveEmployeeDetails(EmployeeDetails employeFamilyDetails,bool isUpdate, int empid=0)
        {
          int EmployeeID =  EmployeeRepository.SaveEmployeeDetails(employeFamilyDetails, isUpdate, empid);
           if(EmployeeID>0)
           {
               return EmployeeID;
           }
           return 0;
        }

        public static bool SaveEmployeeRegistration(EmployeeRegistartion employeeRegistartion, string user)
     {

        int result= EmployeeRepository.SaveQuickEmployee(employeeRegistartion, user);
            if(result<0)
            {
                return false;
            }
            
                return true;
          
     }
     public static byte[] GetImage(int empID)
     {
         return null;
     }
       public static List<EmployeeRegistrationDetails> GetAllReegisteredEmployees()
     {

         return EmployeeRepository.GetAllRegisteredEmployess().ToList();
     }

        public static bool DeletedRegisteredEmployee(int Id)
       {

           return EmployeeRepository.DeleteRegisteredEmployee(Id);
       }
    }
}
