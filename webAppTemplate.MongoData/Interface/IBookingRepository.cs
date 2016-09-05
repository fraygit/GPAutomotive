using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA.MongoData.Interface
{
    public interface IBookingRepository : IEntityService<Booking>
    {
        Task<bool> IsAvailable(DateTime bookingDate, int timeSlot, string serviceType);
        Task<List<CalendarBooking>> GetCalendarBookings(DateTime startDate, DateTime endDate, string serviceType);
        Task<List<int>> GetAvailableTimeSlot(DateTime date, string serviceType);
        Task<List<Booking>> GetRequest(string serviceType);
        Task<List<Booking>> GetApprovedBookings(DateTime bookingDate, string serviceType);
    }
}
