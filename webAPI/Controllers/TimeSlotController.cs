using GPA.API.Models;
using GPA.MongoData.Interface;
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
    public class TimeSlotController : ApiController
    {
        private readonly IBookingRepository bookingRepository;

        public TimeSlotController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<List<int>> GetAvailableTimeSlow(RequestTimeSlot requestTimeSlot)
        {
            var timeSlots = await bookingRepository.GetAvailableTimeSlot(requestTimeSlot.Date, requestTimeSlot.ServiceType);
            return timeSlots;
        }
    }
}
