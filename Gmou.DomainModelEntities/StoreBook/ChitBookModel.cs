using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmou.DomainModelEntities.StoreBook
{

    //@busid INT,
    //@chitbookno INT,
    //@chitserialstart INT,
    //@chhitserialend INT,
    //@issuedby INT,
    //@comment nvarchar(500)=null
    public class ChitBookModel
    {
        public int BusID { get; set; }
        public int ChitBookNumber { get; set; }
        public int ChitSerialStart { get; set; }
        public int ChitSerialEnd { get; set; }
        public int IssuedBy { get; set; }
        public string Comment { get; set; }
    }

    public class ChitBookDetails
    {
        public int ChitBookID { get; set; }
        public string  BusNumber { get; set; }
        public int ChitBookNumber { get; set; }
        public int ChitSerialStart { get; set; }
        public int ChitSerialEnd { get; set; }
        public string IssuedBy { get; set; }
        public string Comment { get; set; }
        public DateTime IssuedDate { get; set; }
        public ChitBookDetails(int chitbookid, string busnumber, int chitbooknumber, int chitserialstart, int chitserialend, DateTime issueddate,string issuedby, string comment)
        {

            this.ChitBookID=chitbookid;
            this.BusNumber=busnumber;
            this.ChitBookNumber=chitbooknumber;
            this.ChitSerialStart=chitserialstart;
            this.ChitSerialEnd=chitserialend;
            this.IssuedDate = issueddate;
            this.IssuedBy=issuedby;
            this.Comment=comment;
        }
    }
}
