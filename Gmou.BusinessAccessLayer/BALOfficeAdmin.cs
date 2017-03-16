using Gmou.DataRepository;
using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.BusinessAccessLayer
{
  public  class BALOfficeAdmin
    {

      public static List<OfficeAdvanceAdminModel> BALGetAllAdvanceByDate()
      {
          return OfficeAdminRepository.GetAllAdvanceByDate().ToList();

      }
      public static bool BALInsertAdvance(OfficeAdvanceAdmin model)
      {
          return OfficeAdminRepository.InsertAdvnaceMemo(model);
      }
    }
}
