using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPA.API.Models
{
    public class RequestTimeSlot
    {
        public string ServiceType { get; set; }
        public DateTime Date { get; set; }
    }
}