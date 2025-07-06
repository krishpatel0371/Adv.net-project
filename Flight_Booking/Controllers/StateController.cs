using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public StateController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetState GET: api/State
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDetail>>> GetState()
        {
            return await _context.StateDetails.ToListAsync();
        }

        #endregion

        #region GetStateById GET: api/State/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StateDetail>> GetStateById(int id)
        {
            var state = await _context.StateDetails.FindAsync(id);
            return state == null ? NotFound() : Ok(state);
        }
        #endregion

        #region DeleteStateById DELETE: api/State/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStateById(int id)
        {
            var state = await _context.StateDetails.FindAsync(id);
            if (state == null)
                return NotFound();

            _context.StateDetails.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertState POST: api/State
        [HttpPost]
        public async Task<ActionResult<StateDetail>> InsertState(StateDetail state)
        {
            _context.StateDetails.Add(state);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStateById), new { id = state.StateId }, state);
        }
        #endregion

        #region UpdateState PUT: api/State/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState(int id, StateDetail updatedState)
        {
            if (id != updatedState.StateId)
                return BadRequest();

            var state = await _context.StateDetails.FindAsync(id);
            if (state == null)
                return NotFound();

            state.StateName = updatedState.StateName;
            _context.Entry(state).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        [HttpGet("dropdown-country")]
        public IActionResult GetCountryDropdown()
        {
            var countries = _context.CountryDetails
                .Select(c => new { c.CountryId, c.CountryName })
                .ToList();

            return Ok(countries);
        }

    }
}
