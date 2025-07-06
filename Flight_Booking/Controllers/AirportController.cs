using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public AirportController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAirport GET: api/Airport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportDetail>>> GetAll()
        {
            return await _context.AirportDetails.ToListAsync();
        }

        #endregion

        #region GetAirportById GET: api/Airport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportDetail>> GetById(int id)
        {
            var airport = await _context.AirportDetails.FindAsync(id);
            return airport == null ? NotFound() : Ok(airport);
        }
        #endregion

        #region DeleteAirportById DELETE: api/Airport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var airport = await _context.AirportDetails.FindAsync(id);
            if (airport == null) return NotFound();

            _context.AirportDetails.Remove(airport);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertAirport POST: api/Airport
        [HttpPost]
        public async Task<IActionResult> Create(AirportDetail airport)
        {
            _context.AirportDetails.Add(airport);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = airport.AirportId }, airport);
        }
        #endregion

        #region UpdateAirport PUT: api/Airport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AirportDetail airport)
        {
            if (id != airport.AirportId) return BadRequest();

            var existing = await _context.AirportDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.AirportName = airport.AirportName;
            existing.CityId = airport.CityId;
            existing.StateId = airport.StateId;
            existing.CountryId = airport.CountryId;
            existing.Iataid = airport.Iataid;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Get airports by city, state, country

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<AirportDetail>>> Filter([FromQuery] int? cityId, [FromQuery] int? stateId, [FromQuery] int? countryId)
        {
            var query = _context.AirportDetails.AsQueryable();

            if (cityId.HasValue)
                query = query.Where(a => a.CityId == cityId);

            if (stateId.HasValue)
                query = query.Where(a => a.StateId == stateId);

            if (countryId.HasValue)
                query = query.Where(a => a.CountryId == countryId);

            return await query.ToListAsync();
        }
        #endregion

        [HttpGet("dropdown-data")]
        public async Task<IActionResult> GetAllDropdownData()
        {
            var countries = await _context.CountryDetails
                .Select(c => new { c.CountryId, c.CountryName })
                .ToListAsync();

            //var states = await _context.StateDetails
            //    .Select(s => new { s.StateId, s.StateName, s.CountryId }) // include CountryId for filtering
            //    .ToListAsync();

            //var cities = await _context.CityDetails
            //    .Select(c => new { c.CityID, c.CityNameFull, c.StateId }) // include StateId for filtering
            //    .ToListAsync();

            var iataCodes = await _context.IataDetails
                .Select(i => new { i.IataId, i.Iatacode })
                .ToListAsync();

            return Ok(new
            {
                Countries = countries,
                //States = states,
                //Cities = cities,
                IataCodes = iataCodes
            });
        }



    }
}
