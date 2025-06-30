using System.Diagnostics.Metrics;
using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCountry()
        {
            var Country = _context.CountryDetails.ToList();
            return Ok(Country);
        }

        #endregion

        #region GetCountryById GET: api/Country/5
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var Country = _context.CountryDetails.Find(id);
            if (Country == null)
            {
                return NotFound();
            }
            return Ok(Country);
        }
        #endregion

        #region DeleteCountryById DELETE: api/Country/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCountryById(int id)
        {
            var Country = _context.CountryDetails.Find(id);
            if (Country == null)
            {
                return NotFound();
            }

            _context.CountryDetails.Remove(Country);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertCountry POST: api/Country
        [HttpPost]
        public IActionResult InsertCountry(CountryDetail Country)
        {
            _context.CountryDetails.Add(Country);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCountryById), new { id = Country.CountryId }, Country);
        }
        #endregion

        #region UpdateCountry PUT: api/Country/5
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, CountryDetail updatedCountry)
        {
            if (id != updatedCountry.CountryId)
            {
                return BadRequest();
            }

            var Country = _context.CountryDetails.Find(id);
            if (Country == null)
            {
                return NotFound();
            }

            Country.CountryName = updatedCountry.CountryName;


            _context.CountryDetails.Update(Country);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
