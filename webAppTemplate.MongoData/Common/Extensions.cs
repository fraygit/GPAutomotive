using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Common
{
    public static class Extensions
    {
        public static DateTime ConvertToLocal(this DateTime value)
        {
            TimeZoneInfo nzSTZone = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            var convertedDatetime = TimeZoneInfo.ConvertTimeFromUtc(value, nzSTZone);
            return convertedDatetime;
        }
    }
}
