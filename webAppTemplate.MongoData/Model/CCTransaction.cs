using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class CCTransaction : MongoEntity
    {
        public string Username { get; set; }
        public string TransactionRef { get; set; }
        public string SessionId { get; set; }
        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public int Status { get; set; }
        public string CardNumber2 { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AuthCode { get; set; }
        public string DpsBillingId { get; set; }
    }
}
