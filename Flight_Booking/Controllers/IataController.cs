using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IataController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public IataController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetIata GET: api/Iata
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IataDetail>>> GetAll()
        {
            return await _context.IataDetails.ToListAsync();
        }

        #endregion

        #region GetIataById GET: api/Iata/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IataDetail>> GetById(int id)
        {
            var iata = await _context.IataDetails.FindAsync(id);
            return iata == null ? NotFound() : Ok(iata);
        }
        #endregion

        #region DeleteIataById DELETE: api/Iata/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var iata = await _context.IataDetails.FindAsync(id);
            if (iata == null)
                return NotFound();

            _context.IataDetails.Remove(iata);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region InsertIata POST: api/Iata
        [HttpPost]
        public async Task<IActionResult> Create(IataDetail iata)
        {
            _context.IataDetails.Add(iata);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = iata.IataId }, iata);
        }
        #endregion

        #region UpdateIata PUT: api/Iata/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IataDetail iata)
        {
            if (id != iata.IataId)
                return BadRequest();

            var existing = await _context.IataDetails.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Iatacode = iata.Iatacode;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Get by Code (e.g., filter by IATA code)
        [HttpGet("code/{code}")]
        public async Task<ActionResult<IataDetail>> GetByCode(string code)
        {
            var iata = await _context.IataDetails.FirstOrDefaultAsync(i => i.Iatacode == code);
            return iata == null ? NotFound() : Ok(iata);
        }
        #endregion
    }
}
