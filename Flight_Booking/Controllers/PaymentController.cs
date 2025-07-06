using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        #region Configuration Fields
        private readonly FlightBookingContext _context;

        public PaymentController(FlightBookingContext context)
        {
            _context = context;
        }
        #endregion

        #region GetPayment GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetAll()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        #endregion

        #region GetPaymentById GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetById(int id)
        {
            var payment = await _context.PaymentDetails.FindAsync(id);
            return payment == null ? NotFound() : Ok(payment);
        }

        #endregion

        #region DeletePaymentById DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.PaymentDetails.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.PaymentDetails.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region InsertPayment POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> Create(PaymentDetail payment)
        {
            _context.PaymentDetails.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, payment);
        }
        #endregion

        #region UpdatePayment PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentDetail payment)
        {
            if (id != payment.PaymentId)
                return BadRequest();

            var existing = await _context.PaymentDetails.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.BookingId = payment.BookingId;
            existing.PaymentDate = payment.PaymentDate;
            existing.Amount = payment.Amount;
            existing.PaymentMethodId = payment.PaymentMethodId;
            existing.PaymentStatusId = payment.PaymentStatusId;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Get Dropdown Data for Payment

        [HttpGet("dropdown-data")]
        public async Task<IActionResult> GetPaymentDropdownData()
        {
            var bookings = await _context.BookingDetails
                .Select(b => new
                {
                    b.BookingId,
                    Display = "Booking #" + b.BookingId
                })
                .ToListAsync();

            var paymentMethods = await _context.PaymentMethodDetails
                .Select(pm => new
                {
                    pm.PaymentMethodId,
                    pm.PaymentMethod
                })
                .ToListAsync();

            var paymentStatuses = await _context.PaymentStatusDetails
                .Select(ps => new
                {
                    ps.PaymentStatusId,
                    ps.StatusName
                })
                .ToListAsync();

            return Ok(new
            {
                Bookings = bookings,
                PaymentMethods = paymentMethods,
                PaymentStatuses = paymentStatuses
            });
        }

        #endregion

    }
}
