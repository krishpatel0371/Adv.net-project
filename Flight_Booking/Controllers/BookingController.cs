using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<BookingDetail>>> GetAll()
        {
            return await _context.BookingDetails.ToListAsync();
        }

        #endregion

        #region GetBookingById GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetail>> GetById(int id)
        {
            var booking = await _context.BookingDetails.FindAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }
        #endregion

        #region DeleteBookingById DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.BookingDetails.FindAsync(id);
            if (booking == null) return NotFound();

            _context.BookingDetails.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertBooking POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> Create(BookingDetail booking)
        {
            _context.BookingDetails.Add(booking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = booking.BookingId }, booking);
        }
        #endregion

        #region UpdateBooking PUT: api/Booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookingDetail booking)
        {
            if (id != booking.BookingId) return BadRequest();

            var existing = await _context.BookingDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.PassengerId = booking.PassengerId;
            existing.AirportId = booking.AirportId;
            existing.FlightId = booking.FlightId;
            existing.TravelClassId = booking.TravelClassId;
            existing.SeatId = booking.SeatId;
            existing.BookingDate = booking.BookingDate;
            existing.BookingStatusId = booking.BookingStatusId;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Get Bookings by Passenger, Status, or Date
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<BookingDetail>>> Filter(
            [FromQuery] int? passengerId,
            [FromQuery] int? statusId,
            [FromQuery] DateTime? date)
        {
            var query = _context.BookingDetails.AsQueryable();

            if (passengerId.HasValue)
                query = query.Where(b => b.PassengerId == passengerId);

            if (statusId.HasValue)
                query = query.Where(b => b.BookingStatusId == statusId);

            if (date.HasValue)
                query = query.Where(b => b.BookingDate.Date == date.Value.Date);

            return await query.ToListAsync();
        }
        #endregion

        [HttpGet("dropdown-data")]
        public async Task<IActionResult> GetBookingDropdownData()
        {
            var passengers = await _context.PassengerDetails
                .Select(p => new { p.PassengerID, FullName = p.FirstName + " " + p.LastName })
                .ToListAsync();

            var airports = await _context.AirportDetails
                .Select(a => new { a.AirportId, a.AirportName })
                .ToListAsync();

            var flights = await _context.FlightDetails
                .Select(f => new { f.FlightId, f.FlightNumber })
                .ToListAsync();

            var travelClasses = await _context.TravelClassDetails
                .Select(tc => new { tc.TravelClassId, tc.TravelClassName })
                .ToListAsync();

            var seats = await _context.SeatDetails
                .Where(s => !s.IsBooked) // Only available seats
                .Select(s => new { s.SeatId, s.SeatNumber })
                .ToListAsync();

            var bookingStatuses = await _context.BookingStatusDetails
                .Select(bs => new { bs.BookingStatusId, bs.StatusName })
                .ToListAsync();

            return Ok(new
            {
                Passengers = passengers,
                Airports = airports,
                Flights = flights,
                TravelClasses = travelClasses,
                Seats = seats,
                BookingStatuses = bookingStatuses
            });
        }

    }
}
