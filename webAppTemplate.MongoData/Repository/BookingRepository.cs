using MongoDB.Bson;
using MongoDB.Driver;
using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using GPA.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPA.MongoData.Common;

namespace GPA.MongoData.Repository
{
    public class BookingRepository : EntityService<Booking>, IBookingRepository
    {

        public async Task<bool> IsAvailable(DateTime bookingDate, int timeSlot, string serviceType)
        {
            var builder = Builders<Booking>.Filter;
            var filter = builder.Eq("DateBooked", bookingDate.Date) & builder.Eq("TimeSlot", timeSlot) & builder.Eq("ServiceType", serviceType);
            var bookings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (bookings.Count > 1)
                return false;
            return true;
        }

        public async Task<List<Booking>> GetRequest(string serviceType)
        {
            var builder = Builders<Booking>.Filter;
            var filter = builder.Eq("IsApproved", false) & builder.Eq("IsDeniedBooking", false) & builder.Eq("ServiceType", serviceType);
            var bookings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();

            if (bookings.Any())
            {
                return bookings.Where(n => n.DateBooked > DateTime.Now.Date).ToList();
            }
            return null;
        }

        public async Task<List<int>> GetAvailableTimeSlot(DateTime date, string serviceType)
        {
            var builder = Builders<Booking>.Filter;
            var filter = builder.Eq("DateBooked", date.Date);
            var bookings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            
            var timeSlots = new List<int>();

            if (bookings.Any())
            {
                for (var i = 8; i < 17; i++)
                {
                    if (!bookings.Any(n => n.TimeSlot == i && n.IsApproved == true))
                    {
                        timeSlots.Add(i);
                    }
                }
            }
            else
            {
                for (var i = 8; i < 17; i++)
                {
                    timeSlots.Add(i);
                }
            }
            return timeSlots;
        }

        public async Task<List<Booking>> GetApprovedBookings(DateTime bookingDate, string serviceType)
        {
            var builder = Builders<Booking>.Filter;
            var filter = builder.Eq("IsApproved", true) &
                builder.Eq("ServiceType", serviceType) &
                builder.Eq("DateBooked", bookingDate.Date);

            var bookings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return bookings;
        }

        public async Task<List<CalendarBooking>> GetCalendarBookings(DateTime startDate, DateTime endDate, string serviceType)
        {
            var builder = Builders<Booking>.Filter;
            var filter = builder.Eq("IsApproved", true) & 
                builder.Eq("IsDeniedBooking", false) &
                builder.Eq("ServiceType", serviceType);

            var bookings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            bookings = bookings.Where(n => n.DateBooked >= startDate.ToUniversalTime() && n.DateBooked <= endDate.ToUniversalTime()).ToList();

            var calendarBookings = new List<CalendarBooking>();
            foreach (DateTime day in Helper.EachDay(startDate, endDate))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                if (bookings.Count(n => n.DateBooked.Date == day.Date) > 7)
                {
                    calendarBookings.Add(new CalendarBooking
                    {
                        allday = true,
                        start = day.ToString("yyyy-MM-dd"),
                        title = "Not available",
                        color = "red"
                    });
                }
                else
                {
                    calendarBookings.Add(new CalendarBooking
                    {
                        allday = true,
                        start = day.ToString("yyyy-MM-dd"),
                        title = "Available",
                        color = "green"
                    });
                }
            }
            return calendarBookings;
            return null;
        }

    }
}
