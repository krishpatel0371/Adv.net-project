using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetPaymentMethod()
        {
            var PaymentMethod = _context.PaymentMethodDetails.ToList();
            return Ok(PaymentMethod);
        }

        #endregion

        #region GetPaymentMethodById GET: api/PaymentMethod/5
        [HttpGet("{id}")]
        public IActionResult GetPaymentMethodById(int id)
        {
            var PaymentMethod = _context.PaymentMethodDetails.Find(id);
            if (PaymentMethod == null)
            {
                return NotFound();
            }
            return Ok(PaymentMethod);
        }
        #endregion

        #region DeletePaymentMethodById DELETE: api/PaymentMethod/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethodById(int id)
        {
            var PaymentMethod = _context.PaymentMethodDetails.Find(id);
            if (PaymentMethod == null)
            {
                return NotFound();
            }

            _context.PaymentMethodDetails.Remove(PaymentMethod);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertPaymentMethod POST: api/PaymentMethod
        [HttpPost]
        public IActionResult InsertPaymentMethod(PaymentMethodDetail PaymentMethod)
        {
            _context.PaymentMethodDetails.Add(PaymentMethod);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPaymentMethodById), new { id = PaymentMethod.PaymentMethodId }, PaymentMethod);
        }
        #endregion

        #region UpdatePaymentMethod PUT: api/PaymentMethod/5
        [HttpPut("{id}")]
        public IActionResult UpdatePaymentMethod(int id, PaymentMethodDetail updatedPaymentMethod)
        {
            if (id != updatedPaymentMethod.PaymentMethodId)
            {
                return BadRequest();
            }

            var PaymentMethod = _context.PaymentMethodDetails.Find(id);
            if (PaymentMethod == null)
            {
                return NotFound();
            }

            PaymentMethod.PaymentMethod = updatedPaymentMethod.PaymentMethod;


            _context.PaymentMethodDetails.Update(PaymentMethod);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
