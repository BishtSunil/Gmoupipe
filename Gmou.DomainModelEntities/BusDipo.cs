using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities
{
   public class BusDipo
    {
       public int DipoID { get; set; }
       public String DipoName  { get; set; }
    }

    public class BusDipoModel
    {

        public List<BusDipo> DipoList { get; set; }
    }
}
