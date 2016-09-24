using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class Booking : MongoEntity
    {
        public string ServiceType { get; set; }
        public DateTime DateBooked { get; set; }
        public bool IsApproved { get; set; }
        public int TimeSlot { get; set; }
        public bool IsDeniedBooking { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
    }
}
