using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetBookingStatus()
        {
            var BookingStatus = _context.BookingStatusDetails.ToList();
            return Ok(BookingStatus);
        }

        #endregion

        #region GetBookingStatusById GET: api/BookingStatus/5
        [HttpGet("{id}")]
        public IActionResult GetBookingStatusById(int id)
        {
            var BookingStatus = _context.BookingStatusDetails.Find(id);
            if (BookingStatus == null)
            {
                return NotFound();
            }
            return Ok(BookingStatus);
        }
        #endregion

        #region DeleteBookingStatusById DELETE: api/BookingStatus/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBookingStatusById(int id)
        {
            var BookingStatus = _context.BookingStatusDetails.Find(id);
            if (BookingStatus == null)
            {
                return NotFound();
            }

            _context.BookingStatusDetails.Remove(BookingStatus);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertBookingStatus POST: api/BookingStatus
        [HttpPost]
        public IActionResult InsertBookingStatus(BookingStatusDetail BookingStatus)
        {
            _context.BookingStatusDetails.Add(BookingStatus);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookingStatusById), new { id = BookingStatus.BookingStatusId }, BookingStatus);
        }
        #endregion

        #region UpdateBookingStatus PUT: api/BookingStatus/5
        [HttpPut("{id}")]
        public IActionResult UpdateBookingStatus(int id, BookingStatusDetail updatedBookingStatus)
        {
            if (id != updatedBookingStatus.BookingStatusId)
            {
                return BadRequest();
            }

            var BookingStatus = _context.BookingStatusDetails.Find(id);
            if (BookingStatus == null)
            {
                return NotFound();
            }

            BookingStatus.StatusName = updatedBookingStatus.StatusName;
            

            _context.BookingStatusDetails.Update(BookingStatus);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
