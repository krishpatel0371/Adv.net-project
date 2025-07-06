using System.Diagnostics.Metrics;
using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public CountryController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetCountry GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDetail>>> GetAll()
        {
            return await _context.CountryDetails.ToListAsync();
        }

        #endregion

        #region GetCountryById GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetail>> GetById(int id)
        {
            var country = await _context.CountryDetails.FindAsync(id);
            return country == null ? NotFound() : Ok(country);
        }
        #endregion

        #region DeleteCountryById DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _context.CountryDetails.FindAsync(id);
            if (country == null) return NotFound();

            _context.CountryDetails.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertCountry POST: api/Country
        [HttpPost]
        public async Task<IActionResult> Create(CountryDetail country)
        {
            _context.CountryDetails.Add(country);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = country.CountryId }, country);
        }
        #endregion

        #region UpdateCountry PUT: api/Country/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CountryDetail country)
        {
            if (id != country.CountryId) return BadRequest();

            var existing = await _context.CountryDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.CountryName = country.CountryName;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
