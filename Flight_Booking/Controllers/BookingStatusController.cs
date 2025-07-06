using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_BookingStatus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatusController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public BookingStatusController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetBookingStatus GET: api/BookingStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingStatusDetail>>> GetAll()
        {
            return await _context.BookingStatusDetails.ToListAsync();
        }
        #endregion

        #region GetBookingStatusById GET: api/BookingStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingStatusDetail>> GetById(int id)
        {
            var status = await _context.BookingStatusDetails.FindAsync(id);
            return status == null ? NotFound() : Ok(status);
        }
        #endregion

        #region DeleteBookingStatusById DELETE: api/BookingStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _context.BookingStatusDetails.FindAsync(id);
            if (status == null) return NotFound();

            _context.BookingStatusDetails.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertBookingStatus POST: api/BookingStatus
        [HttpPost]
        public async Task<IActionResult> Create(BookingStatusDetail status)
        {
            _context.BookingStatusDetails.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = status.BookingStatusId }, status);
        }

        #endregion

        #region UpdateBookingStatus PUT: api/BookingStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookingStatusDetail status)
        {
            if (id != status.BookingStatusId) return BadRequest();

            var existing = await _context.BookingStatusDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.StatusName = status.StatusName;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
