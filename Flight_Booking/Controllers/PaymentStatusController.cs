using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public PaymentStatusController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetPaymentStatus GET: api/PaymentStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentStatusDetail>>> GetAll()
        {
            return await _context.PaymentStatusDetails.ToListAsync();
        }

        #endregion

        #region GetPaymentStatusById GET: api/PaymentStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentStatusDetail>> GetById(int id)
        {
            var status = await _context.PaymentStatusDetails.FindAsync(id);
            return status == null ? NotFound() : Ok(status);
        }
        #endregion

        #region DeletePaymentStatusById DELETE: api/PaymentStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _context.PaymentStatusDetails.FindAsync(id);
            if (status == null)
                return NotFound();

            _context.PaymentStatusDetails.Remove(status);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertPaymentStatus POST: api/PaymentStatus
        [HttpPost]
        public async Task<IActionResult> Create(PaymentStatusDetail status)
        {
            _context.PaymentStatusDetails.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = status.PaymentStatusId }, status);
        }
        #endregion

        #region UpdatePaymentStatus PUT: api/PaymentStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentStatusDetail updated)
        {
            if (id != updated.PaymentStatusId)
                return BadRequest();

            var status = await _context.PaymentStatusDetails.FindAsync(id);
            if (status == null)
                return NotFound();

            status.StatusName = updated.StatusName;

            _context.Entry(status).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
