using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class CreditCardPayments : MongoEntity
    {
        public string Username { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardNumber2 { get; set; }
        public decimal Amount { get; set; }
        public string ResponseText { get; set; }
        public string ResponseCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ParkingId { get; set; }
    }
}
