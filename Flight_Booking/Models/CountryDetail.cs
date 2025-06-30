using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class CountryDetail
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<AirportDetail> AirportDetails { get; set; } = new List<AirportDetail>();
}
