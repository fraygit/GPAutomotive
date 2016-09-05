using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.API.Models
{
    public class RequestApprovedBooking
    {
        public DateTime Date { get; set; }
        public string ServiceType { get; set; }
    }
}