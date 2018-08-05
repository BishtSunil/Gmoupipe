using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Gmou.Web.Helpers
{
    public class SMSGateway
    {
        public static void SendVivraniSMS(decimal amount, long? contact, string ownername, string bunumber, decimal totalamount, string vivranid)
        {

            string sNumber = "7536865966";
            // string sMessage = "Dear Sir, abcg12344 / abcdef Advance Amount has been taken for Rs.1200 on Date : 28/3/17 Thanks, GMOU LTD";
            // string sSenderID = "SMSHUB";
            if(contact==null)
            {
                contact = 9890242125;
            }
            string emaaseg = CreateMEssage(SMSMessage.Vivrani, amount, ownername, bunumber, contact, totalamount, vivranid);
            string sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=sbisht&password=P9890242125&msisdn=" + sNumber + "&sid=GMOULT&msg=" + emaaseg + "&fl=0&gwid=2";
            //string sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" +
            //spwd + "&msisdn =" + sNumber + "&sid =" + sSenderID + "&msg =" + sMessage + "&fl =1 & gwid = 2";
            string sResponse = GetResponse(sURL);
            // Response.Write(sResponse);
        }
        //public static void SendFuelSMS(decimal amount, long contact, string ownername, string bunumber, decimal quantity)
        //{

        //    string sNumber = "9412965801";
        //    // string sMessage = "Dear Sir, abcg12344 / abcdef Advance Amount has been taken for Rs.1200 on Date : 28/3/17 Thanks, GMOU LTD";
        //    // string sSenderID = "SMSHUB";
        //   // string emaaseg = CreateMEssage(SMSMessage.Vivrani, amount, ownername, bunumber, contact, totalamount, vivranid);
        //    string sURL = "http://103.16.142.110/vendorsms/pushsms.aspx?user=sbisht&password=P9890242125&msisdn=" + contact + "&sid=GMOULT&msg=" + emaaseg + "&fl=0&gwid=2";
        //    //string sURL = "http://cloud.smsindiahub.in/vendorsms/pushsms.aspx?user=" + sUser + "&password=" +
        //    //spwd + "&msisdn =" + sNumber + "&sid =" + sSenderID + "&msg =" + sMessage + "&fl =1 & gwid = 2";
        //    string sResponse = GetResponse(sURL);
        //    // Response.Write(sResponse);
        //}


            public static void SendFuelSMS(decimal amount, long? contact, string ownername, string bunumber, string StationName, string FuelType)

        {

        }
        public static string CreateMEssage(SMSMessage op, decimal amount, string ownername, string busnumber, long? contact, decimal totalamount, string vivranid)
        {
            string vivraniamount = string.Format("Rs." + amount);
            string vivranitotalamount = string.Format("Rs." + totalamount);

            switch (op)
            {
                case SMSMessage.Advance:
                    {
                        string emssage = String.Format("Dear Sir, {0} / {1} Advance Amount has been taken for {2} on Date : {3} Thanks, GMOU LTD", "UK12PA-090", "Mr.Kamal kumar", "Rs.5000", DateTime.Now.ToShortDateString());
                        return emssage;

                    }
                case SMSMessage.Vivrani:
                    {
                        // string emssage = String.Format("Dear Sir, {0}/ {1} Vivrani No. {2} has been generated for {3} on Date : {4}. Total {5} Vivrani Generated till on this {6} Thanks, GMOU LTD", busnumber, ownername, vivranid, String.Format("Rs. {0:0.00}", amount), DateTime.Now.ToShortDateString(), String.Format("Rs. {0:0.00}", totalamount), CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));
                        string emssage = string.Format(" Dear Sir, {0}/ {1} Vivrani No. {2} generated for {3} on Date : {4}. Total {5} Vivrani Thanks, GMOU LTD", busnumber, ownername, vivranid, String.Format("Rs. {0:0.00}", amount), DateTime.Now.ToShortDateString(), String.Format("Rs. {0:0.00}", totalamount));
                        return emssage;
                        //Dear Sir, ##VechileNumber##/ ##Owner's Name ## Vivrani No. ##Vivrani number## generated for ##Rs.1200## on Date : ##Date##. Total ##Total## Vivrani Thanks, GMOU LTD
                    }
                case SMSMessage.Fuel:
                    {
                        string emssage = String.Format("Dear Sir, {0}/ {1} Fuel is filled for {2} of {3} at {4} on Date : {5} Thanks, GMOU LTD", "UK12PA-090", "Mr.Kamal kumar", "Rs.5000", DateTime.Now.ToShortDateString());
                        return emssage;

                    }

            }
            return null;

        }
        public static string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest
            .Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request
                .GetResponse();
                Stream receiveStream = response.GetResponseStream(
                );
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch
            {
                return "";
            }
        }
        //public static string CreateMessage(string ownername, string busnumber, string staionname, string fueltype)
        //{
        //    string message = "{0}/ {1} You Account has been debited for {2} for {3} at {4} on Date : {5} Thanks, GMOU LTD";
        //}


        //    Dear Sir, 
        public enum SMSMessage { Advance, Vivrani, Fuel }

}
    }