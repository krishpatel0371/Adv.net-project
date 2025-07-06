using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public FlightController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetFlight GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightDetail>>> GetAll()
        {
            return await _context.FlightDetails.ToListAsync();
        }

        #endregion

        #region GetFlightById GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightDetail>> GetById(int id)
        {
            var flight = await _context.FlightDetails.FindAsync(id);
            return flight == null ? NotFound() : Ok(flight);
        }
        #endregion

        #region DeleteFlightById DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await _context.FlightDetails.FindAsync(id);
            if (flight == null) return NotFound();

            _context.FlightDetails.Remove(flight);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertFlight POST: api/Flight
        [HttpPost]
        public async Task<IActionResult> Create(FlightDetail flight)
        {
            _context.FlightDetails.Add(flight);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = flight.FlightId }, flight);
        }
        #endregion

        #region UpdateFlight PUT: api/Flight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FlightDetail flight)
        {
            if (id != flight.FlightId) return BadRequest();

            var existing = await _context.FlightDetails.FindAsync(id);
            if (existing == null) return NotFound();

            existing.FlightNumber = flight.FlightNumber;
            existing.AirLineId = flight.AirLineId;
            existing.DepartureCityId = flight.DepartureCityId;
            existing.ArrivalCityId = flight.ArrivalCityId;
            existing.DepartureTime = flight.DepartureTime;
            existing.ArrivalTime = flight.ArrivalTime;
            existing.TotalSeats = flight.TotalSeats;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Filter flights by airline or cities
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<FlightDetail>>> Filter(
           [FromQuery] int? airlineId,
           [FromQuery] int? departureCityId,
           [FromQuery] int? arrivalCityId)
        {
            var query = _context.FlightDetails.AsQueryable();

            if (airlineId.HasValue)
                query = query.Where(f => f.AirLineId == airlineId);

            if (departureCityId.HasValue)
                query = query.Where(f => f.DepartureCityId == departureCityId);

            if (arrivalCityId.HasValue)
                query = query.Where(f => f.ArrivalCityId == arrivalCityId);

            return await query.ToListAsync();
        }
        #endregion

        [HttpGet("dropdown-data")]
        public IActionResult GetFlightDropdownData()
        {
            var airlines = _context.AirlineDetails
                .Select(a => new { a.AirlineId, a.AirlineName })
                .ToList();

            var cities = _context.CityDetails
                .Select(c => new { c.CityID, c.CityNameFull })
                .ToList();

            return Ok(new
            {
                Airlines = airlines,
                DepartureCities = cities,
                ArrivalCities = cities
            });
        }

    }
}
