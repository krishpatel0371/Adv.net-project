using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class CityDetail
{
    public int CityID { get; set; }

    public string CityNameFull { get; set; } = null!;

    public string CityNameShort { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    [JsonIgnore]
    public virtual ICollection<AirportDetail> AirportDetails { get; set; } = new List<AirportDetail>();
    [JsonIgnore]
    public virtual ICollection<FlightDetail> FlightDetailArrivalCities { get; set; } = new List<FlightDetail>();
    [JsonIgnore]
    public virtual ICollection<FlightDetail> FlightDetailDepartureCities { get; set; } = new List<FlightDetail>();
}
