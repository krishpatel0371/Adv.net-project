using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetState()
        {
            var State = _context.StateDetails.ToList();
            return Ok(State);
        }

        #endregion

        #region GetStateById GET: api/State/5
        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var State = _context.StateDetails.Find(id);
            if (State == null)
            {
                return NotFound();
            }
            return Ok(State);
        }
        #endregion

        #region DeleteStateById DELETE: api/State/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStateById(int id)
        {
            var State = _context.StateDetails.Find(id);
            if (State == null)
            {
                return NotFound();
            }

            _context.StateDetails.Remove(State);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertState POST: api/State
        [HttpPost]
        public IActionResult InsertState(StateDetail State)
        {
            _context.StateDetails.Add(State);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStateById), new { id = State.StateId }, State);
        }
        #endregion

        #region UpdateState PUT: api/State/5
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, StateDetail updatedState)
        {
            if (id != updatedState.StateId)
            {
                return BadRequest();
            }

            var State = _context.StateDetails.Find(id);
            if (State == null)
            {
                return NotFound();
            }

            State.StateName = updatedState.StateName;


            _context.StateDetails.Update(State);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
