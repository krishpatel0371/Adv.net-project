using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class AirportDetail
{
    public int AirportId { get; set; }

    public string AirportName { get; set; }

    public int CityId { get; set; }

    public int StateId { get; set; }

    public int CountryId { get; set; }

    public int Iataid { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    [JsonIgnore]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [JsonIgnore]
    public virtual CityDetail City { get; set; }

    [JsonIgnore]
    public virtual CountryDetail Country { get; set; } = null!;

    [JsonIgnore]
    public virtual IataDetail Iata { get; set; } = null!;
    
    [JsonIgnore]
    public virtual StateDetail State { get; set; } = null!;
}
