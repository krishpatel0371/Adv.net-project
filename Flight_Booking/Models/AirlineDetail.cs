using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class AirlineDetail
{
    public int AirlineId { get; set; }

    public string? AirlineName { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    [JsonIgnore]
    public virtual ICollection<FlightDetail> FlightDetails { get; set; } = new List<FlightDetail>();
}
