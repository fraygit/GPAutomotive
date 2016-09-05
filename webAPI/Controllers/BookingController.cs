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
    public class BookingController : ApiController
    {
        private readonly IBookingRepository bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public async Task<List<CalendarBooking>> GetBookingForCalendar(string serviceType)
        {
            var calendarData = await bookingRepository.GetCalendarBookings(DateTime.Today.AddDays(1), DateTime.Now.AddMonths(3), serviceType);
            return calendarData;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<bool> ApproveBooking(RequestApproveBooking requestApproveBooking)
        {
            var booking = await bookingRepository.Get(requestApproveBooking.BookingId);
            if (booking != null)
            {
                if (requestApproveBooking.IsApproved)
                {
                    booking.IsApproved = true;
                }
                else
                {
                    booking.IsDeniedBooking = true;
                }
                await bookingRepository.Update(requestApproveBooking.BookingId, booking);
                return true;
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Can not find booking request specified."),
                ReasonPhrase = "Please select another booking request."
            });
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public async Task<Booking> CreateBooking(RequestBooking requestBooking)
        {
            var bookingDate = requestBooking.DateBooked.Date;
            if (await bookingRepository.IsAvailable(bookingDate, requestBooking.TimeSlot, requestBooking.ServiceType))
            {
                try
                {
                    var booking = new Booking
                    {
                        ServiceType = requestBooking.ServiceType,
                        DateBooked = bookingDate,
                        TimeSlot = requestBooking.TimeSlot,
                        CustomerEmail = requestBooking.CustomerEmail,
                        CustomerMobile = requestBooking.CustomerMobile,
                        CustomerName = requestBooking.CustomerName
                    };
                    await bookingRepository.CreateSync(booking);
                    return booking;
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Error occured."),
                        ReasonPhrase = ex.Message
                    });
                }
            }
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("The time slot specifed is already booked."),
                ReasonPhrase = "Please select other time slot or service."
            });

        }
    }
}
