using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Gmou.Web.Helpers
{
    public class GMOUHelper
    {

        public static DateTime ConvertTOIST(DateTime dt )
        {
            var currentzone = TimeZone.CurrentTimeZone;

           // DateTime time1 = new DateTime(2008, 12, 11, 6, 0, 0);  // your DataTimeVariable
           TimeZoneInfo timeZone1 = TimeZoneInfo.FindSystemTimeZoneById(currentzone.StandardName.ToString());
            TimeZoneInfo timeZone2 = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime newTime = TimeZoneInfo.ConvertTime(dt, timeZone1, timeZone2);

            return newTime;
        }

        public static void Log(string logMessage)
        {
            string oath2 = HttpContext.Current.Server.MapPath("~/Log.txt");
            using (StreamWriter w = File.AppendText(oath2))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }

    }
}