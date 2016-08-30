using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class Parking : MongoEntity
    {
        public string username { get; set; }
        public string ParkingSpaceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
