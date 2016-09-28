using GPA.API.Models;
using GPA.MongoData.Interface;
using GPA.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GPA.API.Controllers
{
    public class BookingController : ApiController
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IEmailNotificationRepository emailNotificationRepository;

        public BookingController(IBookingRepository bookingRepository, IEmailNotificationRepository emailNotificationRepository)
        {
            this.bookingRepository = bookingRepository;
            this.emailNotificationRepository = emailNotificationRepository;
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

                var to = new List<string>();
                to.Add(booking.CustomerEmail);

                var time = booking.DateBooked.DayOfWeek == DayOfWeek.Saturday ?
                    string.Format("{0}:30", booking.TimeSlot) :
                    string.Format("{0}:00", booking.TimeSlot);

                if (booking.TimeSlot == 11 && booking.DateBooked.DayOfWeek == DayOfWeek.Saturday)
                {
                    time = "11:00";
                }

                var message = new StringBuilder();
                message.Append(string.Format(@"Dear {0},<br/>", booking.CustomerName));
                message.Append(string.Format(@"We are pleased to confirm your appointment with Gary Prohm.<br/>", booking.CustomerName));
                message.Append(string.Format(@"Here are the appointment details:<br/>", booking.CustomerName));
                message.Append(string.Format(@"Requested Service: {0}<br/>", booking.ServiceType));
                message.Append(string.Format(@"Date and Time: {0} {1}<br/>", booking.DateBooked.ToString("d MMM yyyy"), time));
                message.Append(string.Format(@"Phone: {0}<br/>", booking.CustomerMobile));
                message.Append(string.Format(@"Email: {0}<br/>", booking.CustomerEmail));
                message.Append(string.Format(@"Other details/comments: {0}<br/>", booking.Comment));
                message.Append(string.Format(@"<br/>Kind Regards<br/>", booking.Comment));
                message.Append(string.Format(@"<br/>Gary Prohm Automotive<br/>", booking.Comment));

                var emailNotification = new EmailNotification
                {
                    From = "gpautomotive@vodafone.co.nz",
                    To = to,
                    Subject = "Gary Prohm Automotive - Confirmed booking",
                    Message = message.ToString(),
                    IsHtml = true,
                    Status = 1
                };

                var isPool = await emailNotificationRepository.CreateSync(emailNotification);

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
                        CustomerName = requestBooking.CustomerName,
                        Comment = requestBooking.Comment
                    };
                    await bookingRepository.CreateSync(booking);

                    var message = new StringBuilder();
                    message.Append(string.Format(@"Requested Service: {0}<br/>", requestBooking.ServiceType));
                    message.Append(string.Format(@"Date and Time: {0} {1}:00<br/>", requestBooking.DateBooked.ToString("d MMM yyyy"), requestBooking.TimeSlot));
                    message.Append(string.Format(@"Phone: {0}<br/>", booking.CustomerMobile));
                    message.Append(string.Format(@"Email: {0}<br/>", booking.CustomerEmail));
                    message.Append(string.Format(@"Other details/comments: {0}<br/>", booking.Comment));

                    var to = new List<string>();
                    to.Add("gpautomotive@vodafone.co.nz");
                    //to.Add("francis.yanga@gmail.com");

                    var emailNotification = new EmailNotification
                    {
                        From = "gpautomotive@vodafone.co.nz",
                        To = to,
                        Subject = "A booking request has been made",
                        Message = message.ToString(),
                        IsHtml = true,
                        Status = 1
                    };

                    var isPool = await emailNotificationRepository.CreateSync(emailNotification);
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
