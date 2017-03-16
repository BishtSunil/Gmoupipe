using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gmou.Repository;


namespace Gmou.BusinessAccessLayer
{
    public class GetUserLogin
    {
        public static bool UserLogin(string username, string password)
        {
          var  result=   Gmou.Repository.EmployeeRepository.GetLogin(username, password).ToList();
          return true;
        }

    }
}
