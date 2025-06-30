using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetFlight()
        {
            var Flight = _context.FlightDetails.ToList();
            return Ok(Flight);
        }

        #endregion

        #region GetFlightById GET: api/Flight/5
        [HttpGet("{id}")]
        public IActionResult GetFlightById(int id)
        {
            var Flight = _context.FlightDetails.Find(id);
            if (Flight == null)
            {
                return NotFound();
            }
            return Ok(Flight);
        }
        #endregion

        #region DeleteFlightById DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFlightById(int id)
        {
            var Flight = _context.FlightDetails.Find(id);
            if (Flight == null)
            {
                return NotFound();
            }

            _context.FlightDetails.Remove(Flight);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertFlight POST: api/Flight
        [HttpPost]
        public IActionResult InsertFlight(FlightDetail Flight)
        {
            _context.FlightDetails.Add(Flight);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFlightById), new { id = Flight.FlightId }, Flight);
        }
        #endregion

        #region UpdateFlight PUT: api/Flight/5
        [HttpPut("{id}")]
        public IActionResult UpdateFlight(int id, FlightDetail updatedFlight)
        {
            if (id != updatedFlight.FlightId)
            {
                return BadRequest();
            }

            var Flight = _context.FlightDetails.Find(id);
            if (Flight == null)
            {
                return NotFound();
            }

            Flight.FlightNumber = updatedFlight.FlightNumber;
            Flight.AirLineId = updatedFlight.AirLineId;
            Flight.DepartureCityId = updatedFlight.DepartureCityId;
            Flight.ArrivalCityId = updatedFlight.ArrivalCityId;
            Flight.DepartureTime = updatedFlight.DepartureTime;
            Flight.ArrivalTime = updatedFlight.ArrivalTime;
            Flight.TotalSeats = updatedFlight.TotalSeats;


            _context.FlightDetails.Update(Flight);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
