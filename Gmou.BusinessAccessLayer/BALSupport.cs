﻿using Gmou.DataRepository;
using Gmou.DomainModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.BusinessAccessLayer
{
  public   class BALSupport
    {
      public static List<PlacesModel> GetAllPlaces()
      {
        return  SupportingRepository.GetAllPlaces().ToList();
      }

      public static List<CashSheetSummaryModel> GetCashSheetChart()
      {
          return SupportingRepository.GetCashSheetChart().ToList();
      }
      public static List<ServiceRoute> BALSaveServiceRoute(ServiceRoute model)
      {
          return SupportingRepository.SaveServiceRoute(model).ToList() ;
      }
      public static MontlyBusReport BALGetVivraniReports(int id, DateTime dt )
      {

          return ReportsRepository.GetVivraniReports(id, dt.Month, dt.Year);
      }

        public static FuelStationModel BALGetFuelStationNo(string UserName)
        {
            return FuelRepository.GetFuelAdmin(UserName);

        }
    }
}
