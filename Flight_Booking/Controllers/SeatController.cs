using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public SeatController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetSeat GET: api/Seat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatDetail>>> GetAll()
        {
            return await _context.SeatDetails.ToListAsync();
        }

        #endregion

        #region GetSeatById GET: api/Seat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeatDetail>> GetById(int id)
        {
            var seat = await _context.SeatDetails.FindAsync(id);
            return seat == null ? NotFound() : Ok(seat);
        }
        #endregion

        #region DeleteSeatById DELETE: api/Seat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seat = await _context.SeatDetails.FindAsync(id);
            if (seat == null)
                return NotFound();

            _context.SeatDetails.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region InsertSeat POST: api/Seat
        [HttpPost]
        public async Task<IActionResult> Create(SeatDetail seat)
        {
            _context.SeatDetails.Add(seat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = seat.SeatId }, seat);
        }
        #endregion

        #region UpdateSeat PUT: api/Seat/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SeatDetail updatedSeat)
        {
            if (id != updatedSeat.SeatId)
                return BadRequest();

            var seat = await _context.SeatDetails.FindAsync(id);
            if (seat == null)
                return NotFound();

            seat.SeatNumber = updatedSeat.SeatNumber;
            seat.FlightId = updatedSeat.FlightId;
            seat.IsBooked = updatedSeat.IsBooked;

            _context.Entry(seat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Get Dropdown Flights for Seat

        [HttpGet("dropdown/flights")]
        public async Task<IActionResult> GetFlightsForDropdown()
        {
            var flights = await _context.FlightDetails
                .Select(f => new
                {
                    f.FlightId,
                    f.FlightNumber
                })
                .ToListAsync();

            return Ok(flights);
        }

        #endregion

    }
}
