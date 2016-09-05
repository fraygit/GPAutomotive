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
    public class BookingRequestController : ApiController
    {
        private readonly IBookingRepository bookingRepository;

        public BookingRequestController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<Booking>> GetReques(string serviceType)
        {
            var bookingRequests = await bookingRepository.GetRequest(serviceType);
            return bookingRequests;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<bool> Update(string id, Booking bookingRequest)
        {
            var bookingRequests = await bookingRepository.Update(id, bookingRequest);
            return true;
        }
    }
}
