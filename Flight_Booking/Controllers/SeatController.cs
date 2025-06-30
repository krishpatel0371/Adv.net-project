using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetSeat()
        {
            var Seat = _context.SeatDetails.ToList();
            return Ok(Seat);
        }

        #endregion

        #region GetSeatById GET: api/Seat/5
        [HttpGet("{id}")]
        public IActionResult GetSeatById(int id)
        {
            var Seat = _context.SeatDetails.Find(id);
            if (Seat == null)
            {
                return NotFound();
            }
            return Ok(Seat);
        }
        #endregion

        #region DeleteSeatById DELETE: api/Seat/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSeatById(int id)
        {
            var Seat = _context.SeatDetails.Find(id);
            if (Seat == null)
            {
                return NotFound();
            }

            _context.SeatDetails.Remove(Seat);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertSeat POST: api/Seat
        [HttpPost]
        public IActionResult InsertSeat(SeatDetail Seat)
        {
            _context.SeatDetails.Add(Seat);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSeatById), new { id = Seat.SeatId }, Seat);
        }
        #endregion

        #region UpdateSeat PUT: api/Seat/5
        [HttpPut("{id}")]
        public IActionResult UpdateSeat(int id, SeatDetail updatedSeat)
        {
            if (id != updatedSeat.SeatId)
            {
                return BadRequest();
            }

            var Seat = _context.SeatDetails.Find(id);
            if (Seat == null)
            {
                return NotFound();
            }

            Seat.SeatNumber = updatedSeat.SeatNumber;
            Seat.FlightId = updatedSeat.FlightId;
            Seat.IsBooked = updatedSeat.IsBooked;

            _context.SeatDetails.Update(Seat);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
