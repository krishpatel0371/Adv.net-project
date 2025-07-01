using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public BookingController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetBooking GET: api/Booking
        [HttpGet]
        public IActionResult GetBooking()
        {
            var Booking = _context.BookingDetails.ToList();
            return Ok(Booking);
        }

        #endregion

        #region GetBookingById GET: api/Booking/5
        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var Booking = _context.BookingDetails.Find(id);
            if (Booking == null)
            {
                return NotFound();
            }
            return Ok(Booking);
        }
        #endregion

        #region DeleteBookingById DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookingById(int id)
        {
            var Booking = _context.BookingDetails.Find(id);
            if (Booking == null)
            {
                return NotFound();
            }

            _context.BookingDetails.Remove(Booking);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertBooking POST: api/Booking
        [HttpPost]
        public IActionResult InsertBooking(BookingDetail Booking)
        {
            _context.BookingDetails.Add(Booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookingById), new { id = Booking.BookingId }, Booking);
        }
        #endregion

        #region UpdateBooking PUT: api/Booking/5
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, BookingDetail updatedBooking)
        {
            if (id != updatedBooking.BookingId)
            {
                return BadRequest();
            }

            var Booking = _context.BookingDetails.Find(id);
            if (Booking == null)
            {
                return NotFound();
            }

            Booking.PassengerId = updatedBooking.PassengerId;
            Booking.AirportId = updatedBooking.AirportId;
            Booking.FlightId = updatedBooking.FlightId;
            Booking.TravelClassId = updatedBooking.TravelClassId;
            Booking.SeatId = updatedBooking.SeatId;
            Booking.BookingDate = updatedBooking.BookingDate;
            Booking.BookingStatusId = updatedBooking.BookingStatusId;

            _context.BookingDetails.Update(Booking);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
