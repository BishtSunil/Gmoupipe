using Gmou.DomainModelEntities;
using Gmou.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gmou.Web.Helpers
{
    public static  class HelpersMVC
    {
        public static MvcHtmlString GenerateVivraniDiv(int vivraniId)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.AddCssClass("fragment");
            tag.MergeAttribute("href", "/WayBill/EditVivraniDetails?vivraniID=" + vivraniId);
            TagBuilder tagspan = new TagBuilder("span");
            
            TagBuilder tagdiv = new TagBuilder("div");
           
            TagBuilder tagh3 = new TagBuilder("h3");
            tagh3.InnerHtml = "Pending Vivrani";
            TagBuilder tagtext = new TagBuilder("p");
            tagtext.AddCssClass("text");
            tagtext.SetInnerText(string.Format("You have one Pending Vivrani , Serial Nummber: "+vivraniId));
     
          
          
            tagdiv.InnerHtml += tagspan;
            tagdiv.InnerHtml += tagh3;
            tagdiv.InnerHtml += tagtext;
            tag.InnerHtml += tagdiv;
            return new MvcHtmlString(tag.ToString());
            //            <a class="fragment" href="google.com">
            //    <div>
            //        <span id='close'>x</span>
            //    <img src ="http://placehold.it/116x116" alt="some description"/> 
            //    <h3>the title will go here</h3>
            //        <h4> www.myurlwill.com </h4>
            //    <p class="text">
            //        this is a short description yada yada peanuts etc this is a short description yada yada peanuts etc this is a short description yada yada peanuts etc this is a short description yada yada peanuts etcthis is a short description yada yada peanuts etc 
            //    </p>
            //</div>
            //</a>
        }
        public static MvcHtmlString GenerateTable(List<PlacesModel> model)
        {

            TagBuilder tag = new TagBuilder("table");
          
            foreach(var item in model)
            {
                TagBuilder tagtr = new TagBuilder("tr");
                TagBuilder tagtd = new TagBuilder("td");
                tagtd.InnerHtml = item.Name;
                tagtr.InnerHtml += tagtd;
                tag.InnerHtml += tagtr;
            }
            return new MvcHtmlString(tag.ToString());

        }
        public static MvcHtmlString GenerateTicketDetails()
        {
            String temtext = string.Empty;
            for (int k = 0; k < 3; k++)
            {
                TagBuilder tag = new TagBuilder("tr");
                TagBuilder tagtd = new TagBuilder("td");
                var textBox = new TagBuilder("input");
                // Add attributes
                textBox.MergeAttribute("type", "text");

                textBox.MergeAttribute("id", "txtStationFrom");
                textBox.AddCssClass("txt100");
                tagtd.InnerHtml += textBox;
                tag.InnerHtml += tagtd;

                var textBox1 = new TagBuilder("input");
                // Add attributes
                textBox.MergeAttribute("type", "text");

                textBox.MergeAttribute("id", "txtStationTo");
                textBox.AddCssClass("txt100");
                tagtd.InnerHtml += textBox;
                tag.InnerHtml += tagtd;

                var textBox2 = new TagBuilder("input");
                // Add attributes
                textBox.MergeAttribute("type", "text");

                textBox.MergeAttribute("id", "txtTicketNo");
                textBox.AddCssClass("txt100");
                tagtd.InnerHtml += textBox;
                tag.InnerHtml += tagtd;

                var textBox3 = new TagBuilder("input");
                // Add attributes
                textBox.MergeAttribute("type", "text");

                textBox.MergeAttribute("id", "txtFare");
                textBox.AddCssClass("txt100");
                tagtd.InnerHtml += textBox;
                tag.InnerHtml += tagtd;
                temtext += tag.ToString();
            }



            return new MvcHtmlString(temtext.ToString());
        }
        public static MvcHtmlString Generatedivs(BusInsuranceNotification model)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.AddCssClass("fragment");
            TagBuilder tagspan = new TagBuilder("span");
            tagspan.SetInnerText("X");
            TagBuilder tagdiv = new TagBuilder("div");
            tagspan.Attributes["id"] = "close_"+model.bus_id;
            tagspan.AddCssClass("close");
            TagBuilder tagh3 = new TagBuilder("h3");
            tagh3.InnerHtml = "Insurance Expirtion Warning";
            TagBuilder tagtext = new TagBuilder("p");
            tagtext.AddCssClass("text");
            tagtext.SetInnerText(string.Format("Bus Insurance validity will expire on {0} for Bus no.{1}.Fitness Will be Expire on {0}.Contact {2}",model.InsuranceValidity,model.bus_number,model.bus_owner_name));
            tagdiv.InnerHtml += tagspan;
            tagdiv.InnerHtml += tagh3;
            tagdiv.InnerHtml += tagtext;
            tag.InnerHtml += tagdiv;
            return new MvcHtmlString(tag.ToString());
//            <a class="fragment" href="google.com">
//    <div>
//        <span id='close'>x</span>
//    <img src ="http://placehold.it/116x116" alt="some description"/> 
//    <h3>the title will go here</h3>
//        <h4> www.myurlwill.com </h4>
//    <p class="text">
//        this is a short description yada yada peanuts etc this is a short description yada yada peanuts etc this is a short description yada yada peanuts etc this is a short description yada yada peanuts etcthis is a short description yada yada peanuts etc 
//    </p>
//</div>
//</a>
        }
    }
}