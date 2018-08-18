using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GMOULogger
{
   public class Logger
    {
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
