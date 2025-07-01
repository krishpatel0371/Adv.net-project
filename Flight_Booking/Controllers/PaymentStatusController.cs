using Flight_Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetPaymentStatus()
        {
            var PaymentStatus = _context.PaymentStatusDetails.ToList();
            return Ok(PaymentStatus);
        }

        #endregion

        #region GetPaymentStatusById GET: api/PaymentStatus/5
        [HttpGet("{id}")]
        public IActionResult GetPaymentStatusById(int id)
        {
            var PaymentStatus = _context.PaymentStatusDetails.Find(id);
            if (PaymentStatus == null)
            {
                return NotFound();
            }
            return Ok(PaymentStatus);
        }
        #endregion

        #region DeletePaymentStatusById DELETE: api/PaymentStatus/5
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentStatusById(int id)
        {
            var PaymentStatus = _context.PaymentStatusDetails.Find(id);
            if (PaymentStatus == null)
            {
                return NotFound();
            }

            _context.PaymentStatusDetails.Remove(PaymentStatus);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region InsertPaymentStatus POST: api/PaymentStatus
        [HttpPost]
        public IActionResult InsertPaymentStatus(PaymentStatusDetail PaymentStatus)
        {
            _context.PaymentStatusDetails.Add(PaymentStatus);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPaymentStatusById), new { id = PaymentStatus.PaymentStatusId }, PaymentStatus);
        }
        #endregion

        #region UpdatePaymentStatus PUT: api/PaymentStatus/5
        [HttpPut("{id}")]
        public IActionResult UpdatePaymentStatus(int id, PaymentStatusDetail updatedPaymentStatus)
        {
            if (id != updatedPaymentStatus.PaymentStatusId)
            {
                return BadRequest();
            }

            var PaymentStatus = _context.PaymentStatusDetails.Find(id);
            if (PaymentStatus == null)
            {
                return NotFound();
            }

            PaymentStatus.StatusName = updatedPaymentStatus.StatusName;


            _context.PaymentStatusDetails.Update(PaymentStatus);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
