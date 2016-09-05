using GPA.API.Models;
using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GPA.API.Controllers
{
    public class ApprovedBookingController : ApiController
    {
        private readonly IBookingRepository bookingRepository;

        public ApprovedBookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<List<Booking>> GetApprovedBooking(RequestApprovedBooking requestApprovedBooking)
        {
            var approvedBoookings = await bookingRepository.GetApprovedBookings(requestApprovedBooking.Date, requestApprovedBooking.ServiceType);
            return approvedBoookings.OrderBy(n => n.TimeSlot).ToList();
        }
    }
}
