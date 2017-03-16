﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
   public  class Department
    {
       
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; } 

       public Department(int departmentID, string departmentName)
        {
            this.DepartmentID = departmentID;
            this.DepartmentName = departmentName;

        }

      
    }

   public class Buses
   {

       public int BusID { get; set; }
       public string BusNumber { get; set; }

       public Buses(int BusID, string BusNumber)
       {
           this.BusID = BusID;
           this.BusNumber = BusNumber;

       }


   }

    public class Office
    {

        public int OfficeAreaID { get; set; }
        public String OfficeAreaName { get; set; }

        public Office(int officeareaID, string officeareaName)
        {
            this.OfficeAreaID = officeareaID;
            this.OfficeAreaName = officeareaName;
        }
    }
}
