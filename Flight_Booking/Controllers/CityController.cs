using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCity()
        {
            var City = _context.CityDetails.ToList();
            return Ok(City);
        }

        #endregion

        #region GetCityById GET: api/City/5
        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var City = _context.CityDetails.Find(id);
            if (City == null)
            {
                return NotFound();
            }
            return Ok(City);
        }
        #endregion

        #region DeleteCityById DELETE: api/City/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCityById(int id)
        {
            var City = _context.CityDetails.Find(id);
            if (City == null)
            {
                return NotFound();
            }

            _context.CityDetails.Remove(City);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertCity POST: api/City
        [HttpPost]
        public IActionResult InsertCity(CityDetail City)
        {
            _context.CityDetails.Add(City);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCityById), new { id = City.CityID }, City);
        }
        #endregion

        #region UpdateCity PUT: api/City/5
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, CityDetail updatedCity)
        {
            if (id != updatedCity.CityID)
            {
                return BadRequest();
            }

            var City = _context.CityDetails.Find(id);
            if (City == null)
            {
                return NotFound();
            }

            City.CityNameFull = updatedCity.CityNameFull;
            City.CityNameShort = updatedCity.CityNameShort;
            

            _context.CityDetails.Update(City);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
