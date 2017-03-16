using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Gmou.Web.Helpers
{
    public class SMSGateway
    {
        public static  int SendSMS(string Message)
        {
            WebClient Client = new WebClient();
            String RequestURL, RequestData;

            RequestURL = "https://redoxygen.net/sms.dll?Action=SendSMS";

            RequestData = "AccountId=" + "CI00174965"
                + "&Email=" + System.Web.HttpUtility.UrlEncode("sonybulls@gmail.com")
                + "&Password=" + System.Web.HttpUtility.UrlEncode("y8cqMEh4")
                + "&Recipient=" + System.Web.HttpUtility.UrlEncode("9927543200")
                + "&Message=" + System.Web.HttpUtility.UrlEncode(Message);

            byte[] PostData = Encoding.ASCII.GetBytes(RequestData);
            byte[] Response = Client.UploadData(RequestURL, PostData);

            String Result = Encoding.ASCII.GetString(Response);
            int ResultCode = System.Convert.ToInt32(Result.Substring(0, 4));

            return ResultCode;
        }
    }
}