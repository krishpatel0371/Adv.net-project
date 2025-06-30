using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class PaymentDetail
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public int PaymentMethodId { get; set; }

    public int PaymentStatusId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual BookingDetail Booking { get; set; } = null!;
    [JsonIgnore]
    public virtual PaymentMethodDetail PaymentMethod { get; set; } = null!;
    [JsonIgnore]
    public virtual PaymentStatusDetail PaymentStatus { get; set; } = null!;
}
