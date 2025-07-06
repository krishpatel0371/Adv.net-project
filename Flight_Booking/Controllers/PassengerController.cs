using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<PassengerDetail>>> GetAll()
        {
            return await _context.PassengerDetails.ToListAsync();
        }

        #endregion

        #region GetpassengerById GET: api/passenger/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerDetail>> GetById(int id)
        {
            var passenger = await _context.PassengerDetails.FindAsync(id);
            return passenger == null ? NotFound() : Ok(passenger);
        }
        #endregion

        #region DeletepassengerById DELETE: api/passenger/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var passenger = await _context.PassengerDetails.FindAsync(id);
            if (passenger == null)
                return NotFound();

            _context.PassengerDetails.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Insertpassenger POST: api/passenger
        [HttpPost]
        public async Task<IActionResult> Create(PassengerDetail passenger)
        {
            _context.PassengerDetails.Add(passenger);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = passenger.PassengerID }, passenger);
        }
        #endregion

        #region Updatepassenger PUT: api/passenger/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PassengerDetail updatedPassenger)
        {
            if (id != updatedPassenger.PassengerID)
                return BadRequest();

            var passenger = await _context.PassengerDetails.FindAsync(id);
            if (passenger == null)
                return NotFound();

            passenger.FirstName = updatedPassenger.FirstName;
            passenger.LastName = updatedPassenger.LastName;
            passenger.Gender = updatedPassenger.Gender;
            passenger.Dob = updatedPassenger.Dob;
            passenger.Email = updatedPassenger.Email;
            passenger.PhoneNumber = updatedPassenger.PhoneNumber;
            passenger.PassportNumber = updatedPassenger.PassportNumber;
            passenger.Address = updatedPassenger.Address;

            _context.Entry(passenger).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion


    }
}
