using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public PassengerController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetPassenger GET: api/Passenger
        [HttpGet]
        public IActionResult GetPassenger()
        {
            var passenger = _context.PassengerDetails.ToList();
            return Ok(passenger);
        }

        #endregion

        #region GetpassengerById GET: api/passenger/5
        [HttpGet("{id}")]
        public IActionResult GetpassengerById(int id)
        {
            var passenger = _context.PassengerDetails.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return Ok(passenger);
        }
        #endregion

        #region DeletepassengerById DELETE: api/passenger/5
        [HttpDelete("{id}")]
        public IActionResult DeletepassengerById(int id)
        {
            var passenger = _context.PassengerDetails.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.PassengerDetails.Remove(passenger);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Insertpassenger POST: api/passenger
        [HttpPost]
        public IActionResult InsertHospital(PassengerDetail passenger)
        {
            _context.PassengerDetails.Add(passenger);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetpassengerById), new { id = passenger.PassengerID }, passenger);
        }
        #endregion

        #region Updatepassenger PUT: api/passenger/5
        [HttpPut("{id}")]
        public IActionResult UpdateHospital(int id, PassengerDetail updatedpassenger)
        {
            if (id != updatedpassenger.PassengerID)
            {
                return BadRequest();
            }

            var passenger = _context.PassengerDetails.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            passenger.FirstName = updatedpassenger.FirstName;
            passenger.LastName = updatedpassenger.LastName;
            passenger.Gender = updatedpassenger.Gender;
            passenger.Dob = updatedpassenger.Dob;
            passenger.Email = updatedpassenger.Email;
            passenger.PhoneNumber = updatedpassenger.PhoneNumber;
            passenger.PassportNumber = updatedpassenger.PassportNumber;
            passenger.Address = updatedpassenger.Address;

            _context.PassengerDetails.Update(passenger);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion


    }
}
