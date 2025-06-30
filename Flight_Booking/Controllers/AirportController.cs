using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAirport()
        {
            var Airport = _context.AirportDetails.ToList();
            return Ok(Airport);
        }

        #endregion

        #region GetAirportById GET: api/Airport/5
        [HttpGet("{id}")]
        public IActionResult GetAirportById(int id)
        {
            var Airport = _context.AirportDetails.Find(id);
            if (Airport == null)
            {
                return NotFound();
            }
            return Ok(Airport);
        }
        #endregion

        #region DeleteAirportById DELETE: api/Airport/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAirportById(int id)
        {
            var Airport = _context.AirportDetails.Find(id);
            if (Airport == null)
            {
                return NotFound();
            }

            _context.AirportDetails.Remove(Airport);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertAirport POST: api/Airport
        [HttpPost]
        public IActionResult InsertAirport(AirportDetail Airport)
        {
            _context.AirportDetails.Add(Airport);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAirportById), new { id = Airport.AirportId }, Airport);
        }
        #endregion

        #region UpdateAirport PUT: api/Airport/5
        [HttpPut("{id}")]
        public IActionResult UpdateAirport(int id, AirportDetail updatedAirport)
        {
            if (id != updatedAirport.AirportId)
            {
                return BadRequest();
            }

            var Airport = _context.AirportDetails.Find(id);
            if (Airport == null)
            {
                return NotFound();
            }

            Airport.AirportName = updatedAirport.AirportName;
            Airport.CityId = updatedAirport.CityId;
            Airport.StateId = updatedAirport.StateId;
            Airport.CountryId = updatedAirport.CountryId;
            Airport.Iataid = updatedAirport.Iataid;


            _context.AirportDetails.Update(Airport);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
