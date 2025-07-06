using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelClassController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public TravelClassController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetTravelClass GET: api/TravelClass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelClassDetail>>> GetTravelClass()
        {
            return await _context.TravelClassDetails.ToListAsync();
        }

        #endregion

        #region GetTravelClassById GET: api/TravelClass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelClassDetail>> GetTravelClassById(int id)
        {
            var travelClass = await _context.TravelClassDetails.FindAsync(id);
            if (travelClass == null)
                return NotFound();

            return Ok(travelClass);
        }
        #endregion

        #region DeleteTravelClassById DELETE: api/TravelClass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelClassById(int id)
        {
            var travelClass = await _context.TravelClassDetails.FindAsync(id);
            if (travelClass == null)
                return NotFound();

            _context.TravelClassDetails.Remove(travelClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region InsertTravelClass POST: api/TravelClass
        [HttpPost]
        public async Task<ActionResult<TravelClassDetail>> InsertTravelClass(TravelClassDetail travelClass)
        {
            _context.TravelClassDetails.Add(travelClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTravelClassById), new { id = travelClass.TravelClassId }, travelClass);
        }
        #endregion

        #region UpdateTravelClass PUT: api/TravelClass/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravelClass(int id, TravelClassDetail updatedTravelClass)
        {
            if (id != updatedTravelClass.TravelClassId)
                return BadRequest();

            var travelClass = await _context.TravelClassDetails.FindAsync(id);
            if (travelClass == null)
                return NotFound();

            travelClass.TravelClassName = updatedTravelClass.TravelClassName;

            _context.Entry(travelClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
