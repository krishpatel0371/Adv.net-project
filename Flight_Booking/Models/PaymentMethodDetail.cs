using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class PaymentMethodDetail
{
    public int PaymentMethodId { get; set; }

    public string? PaymentMethod { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
}
