using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public AirlineController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAirline GET: api/Airline
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirlineDetail>>> GetAll()
        {
            return await _context.AirlineDetails.ToListAsync();
        }

        #endregion

        #region GetAirlineById GET: api/Airline/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirlineDetail>> GetById(int id)
        {
            var airline = await _context.AirlineDetails.FindAsync(id);
            return airline == null ? NotFound() : Ok(airline);
        }
        #endregion

        #region DeleteAirlineById DELETE: api/Airline/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var airline = await _context.AirlineDetails.FindAsync(id);
            if (airline == null) return NotFound();

            _context.AirlineDetails.Remove(airline);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertAirline POST: api/Airline
        [HttpPost]
        public async Task<IActionResult> Create(AirlineDetail airline)
        {
            _context.AirlineDetails.Add(airline);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = airline.AirlineId }, airline);
        }
        #endregion

        #region UpdateAirline PUT: api/Airline/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AirlineDetail airline)
        {
            if (id != airline.AirlineId) return BadRequest();

            var existing = await _context.AirlineDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.AirlineName = airline.AirlineName;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
