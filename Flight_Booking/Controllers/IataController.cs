using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetIata()
        {
            var Iata = _context.IataDetails.ToList();
            return Ok(Iata);
        }

        #endregion

        #region GetIataById GET: api/Iata/5
        [HttpGet("{id}")]
        public IActionResult GetIataById(int id)
        {
            var Iata = _context.IataDetails.Find(id);
            if (Iata == null)
            {
                return NotFound();
            }
            return Ok(Iata);
        }
        #endregion

        #region DeleteIataById DELETE: api/Iata/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIataById(int id)
        {
            var Iata = _context.IataDetails.Find(id);
            if (Iata == null)
            {
                return NotFound();
            }

            _context.IataDetails.Remove(Iata);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertIata POST: api/Iata
        [HttpPost]
        public IActionResult InsertIata(IataDetail Iata)
        {
            _context.IataDetails.Add(Iata);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetIataById), new { id = Iata.IataId }, Iata);
        }
        #endregion

        #region UpdateIata PUT: api/Iata/5
        [HttpPut("{id}")]
        public IActionResult UpdateIata(int id, IataDetail updatedIata)
        {
            if (id != updatedIata.IataId)
            {
                return BadRequest();
            }

            var Iata = _context.IataDetails.Find(id);
            if (Iata == null)
            {
                return NotFound();
            }

            Iata.Iatacode = updatedIata.Iatacode;


            _context.IataDetails.Update(Iata);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
