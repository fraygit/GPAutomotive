using GPA.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Model
{
    public class AdminUserToken : MongoEntity
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Source { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
