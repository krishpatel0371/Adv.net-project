using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAirline()
        {
            var Airline = _context.AirlineDetails.ToList();
            return Ok(Airline);
        }

        #endregion

        #region GetAirlineById GET: api/Airline/5
        [HttpGet("{id}")]
        public IActionResult GetAirlineById(int id)
        {
            var Airline = _context.AirlineDetails.Find(id);
            if (Airline == null)
            {
                return NotFound();
            }
            return Ok(Airline);
        }
        #endregion

        #region DeleteAirlineById DELETE: api/Airline/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAirlineById(int id)
        {
            var Airline = _context.AirlineDetails.Find(id);
            if (Airline == null)
            {
                return NotFound();
            }

            _context.AirlineDetails.Remove(Airline);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertAirline POST: api/Airline
        [HttpPost]
        public IActionResult InsertAirline(AirlineDetail Airline)
        {
            _context.AirlineDetails.Add(Airline);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAirlineById), new { id = Airline.AirlineId }, Airline);
        }
        #endregion

        #region UpdateAirline PUT: api/Airline/5
        [HttpPut("{id}")]
        public IActionResult UpdateAirline(int id, AirlineDetail updatedAirline)
        {
            if (id != updatedAirline.AirlineId)
            {
                return BadRequest();
            }

            var Airline = _context.AirlineDetails.Find(id);
            if (Airline == null)
            {
                return NotFound();
            }

            Airline.AirlineName = updatedAirline.AirlineName;


            _context.AirlineDetails.Update(Airline);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
