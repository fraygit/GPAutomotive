using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class CalendarBooking
    {
        public string title { get; set; }
        public string start { get; set; }
        public bool allday { get; set; }
        public string color { get; set; }
    }
}
