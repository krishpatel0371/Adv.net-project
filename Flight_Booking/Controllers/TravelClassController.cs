using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelClassController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public TravelClassController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetTravelClass GET: api/TravelClass
        [HttpGet]
        public IActionResult GetTravelClass()
        {
            var TravelClass = _context.TravelClassDetails.ToList();
            return Ok(TravelClass);
        }

        #endregion

        #region GetTravelClassById GET: api/TravelClass/5
        [HttpGet("{id}")]
        public IActionResult GetTravelClassById(int id)
        {
            var TravelClass = _context.TravelClassDetails.Find(id);
            if (TravelClass == null)
            {
                return NotFound();
            }
            return Ok(TravelClass);
        }
        #endregion

        #region DeleteTravelClassById DELETE: api/TravelClass/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTravelClassById(int id)
        {
            var TravelClass = _context.TravelClassDetails.Find(id);
            if (TravelClass == null)
            {
                return NotFound();
            }

            _context.TravelClassDetails.Remove(TravelClass);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertTravelClass POST: api/TravelClass
        [HttpPost]
        public IActionResult InsertTravelClass(TravelClassDetail TravelClass)
        {
            _context.TravelClassDetails.Add(TravelClass);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTravelClassById), new { id = TravelClass.TravelClassId }, TravelClass);
        }
        #endregion

        #region UpdateTravelClass PUT: api/TravelClass/5
        [HttpPut("{id}")]
        public IActionResult UpdateTravelClass(int id, TravelClassDetail updatedTravelClass)
        {
            if (id != updatedTravelClass.TravelClassId)
            {
                return BadRequest();
            }

            var TravelClass = _context.TravelClassDetails.Find(id);
            if (TravelClass == null)
            {
                return NotFound();
            }

            TravelClass.TravelClassName = updatedTravelClass.TravelClassName;

            _context.TravelClassDetails.Update(TravelClass);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
