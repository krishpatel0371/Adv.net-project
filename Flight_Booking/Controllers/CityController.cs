using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public CityController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetCity GET: api/City
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDetail>>> GetAll()
        {
            return await _context.CityDetails.ToListAsync();
        }

        #endregion

        #region GetCityById GET: api/City/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDetail>> GetById(int id)
        {
            var city = await _context.CityDetails.FindAsync(id);
            return city == null ? NotFound() : Ok(city);
        }
        #endregion

        #region DeleteCityById DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await _context.CityDetails.FindAsync(id);
            if (city == null) return NotFound();

            _context.CityDetails.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertCity POST: api/City
        [HttpPost]
        public async Task<IActionResult> Create(CityDetail city)
        {
            _context.CityDetails.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = city.CityID }, city);
        }
        #endregion

        #region UpdateCity PUT: api/City/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CityDetail city)
        {
            if (id != city.CityID) return BadRequest();

            var existing = await _context.CityDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.CityNameFull = city.CityNameFull;
            existing.CityNameShort = city.CityNameShort;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        //#region Get Cities by Country or State
        //[HttpGet("filter")]
        //public async Task<ActionResult<IEnumerable<CityDetail>>> Filter([FromQuery] int? stateId, [FromQuery] int? countryId)
        //{
        //    var query = _context.CityDetails.AsQueryable();

        //    return await query.ToListAsync();
        //}
        //#endregion

        //[HttpGet("dropdown-country-state")]
        //public IActionResult GetCountryStateDropdown()
        //{
        //    var countries = _context.CountryDetails
        //        .Select(c => new { c.CountryId, c.CountryName })
        //        .ToList();

        //    var states = _context.StateDetails
        //        .Select(s => new { s.StateId, s.StateName, s.CountryId })
        //        .ToList();

        //    return Ok(new { Countries = countries, States = states });
        //}

    }
}
