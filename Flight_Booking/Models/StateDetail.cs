using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class StateDetail
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<AirportDetail> AirportDetails { get; set; } = new List<AirportDetail>();
}
