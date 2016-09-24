using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.API.Models
{
    public class RequestBooking
    {
        public string ServiceType { get; set; }
        public DateTime DateBooked { get; set; }
        public int TimeSlot { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
    }
}