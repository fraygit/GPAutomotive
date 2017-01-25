using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class BlockedDate : MongoEntity
    {
        public DateTime Blocked { get; set; }
    }
}
