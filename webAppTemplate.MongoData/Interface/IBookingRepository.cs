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
        Task<bool> CheckBookingNoBookingConflict(Booking booking);
        Task<Booking> Book(string parkingSpaceId, string username, DateTime from, DateTime to);
        Task<Booking> Park(string bookingId, DateTime checkIn);
    }
}
