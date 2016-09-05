using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.API.Models
{
    public class RequestApproveBooking
    {
        public string BookingId { get; set; }
        public bool IsApproved { get; set; }
    }
}