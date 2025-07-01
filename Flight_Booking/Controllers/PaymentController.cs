using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetPayment()
        {
            var Payment = _context.PaymentDetails.ToList();
            return Ok(Payment);
        }

        #endregion

        #region GetPaymentById GET: api/Payment/5
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var Payment = _context.PaymentDetails.Find(id);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        #endregion

        #region DeletePaymentById DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentById(int id)
        {
            var Payment = _context.PaymentDetails.Find(id);
            if (Payment == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(Payment);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertPayment POST: api/Payment
        [HttpPost]
        public IActionResult InsertPayment(PaymentDetail Payment)
        {
            _context.PaymentDetails.Add(Payment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPaymentById), new { id = Payment.PaymentId }, Payment);
        }
        #endregion

        #region UpdatePayment PUT: api/Payment/5
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, PaymentDetail updatedPayment)
        {
            if (id != updatedPayment.PaymentId)
            {
                return BadRequest();
            }

            var Payment = _context.PaymentDetails.Find(id);
            if (Payment == null)
            {
                return NotFound();
            }

            Payment.BookingId = updatedPayment.BookingId;
            Payment.PaymentDate = updatedPayment.PaymentDate;
            Payment.Amount = updatedPayment.Amount;
            Payment.PaymentMethodId = updatedPayment.PaymentMethodId;
            Payment.PaymentStatusId = updatedPayment.PaymentStatusId;


            _context.PaymentDetails.Update(Payment);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
