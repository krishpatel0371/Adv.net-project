using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public PaymentMethodController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetPaymentMethod GET: api/PaymentMethod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethodDetail>>> GetAll()
        {
            return await _context.PaymentMethodDetails.ToListAsync();
        }

        #endregion

        #region GetPaymentMethodById GET: api/PaymentMethod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDetail>> GetById(int id)
        {
            var method = await _context.PaymentMethodDetails.FindAsync(id);
            return method == null ? NotFound() : Ok(method);
        }
        #endregion

        #region DeletePaymentMethodById DELETE: api/PaymentMethod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var method = await _context.PaymentMethodDetails.FindAsync(id);
            if (method == null)
                return NotFound();

            _context.PaymentMethodDetails.Remove(method);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region InsertPaymentMethod POST: api/PaymentMethod
        [HttpPost]
        public async Task<IActionResult> Create(PaymentMethodDetail method)
        {
            _context.PaymentMethodDetails.Add(method);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = method.PaymentMethodId }, method);
        }
        #endregion

        #region UpdatePaymentMethod PUT: api/PaymentMethod/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentMethodDetail method)
        {
            if (id != method.PaymentMethodId)
                return BadRequest();

            var existing = await _context.PaymentMethodDetails.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.PaymentMethod = method.PaymentMethod;
            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
