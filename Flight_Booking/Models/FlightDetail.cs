using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class FlightDetail
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; }

    public int AirLineId { get; set; }

    public int DepartureCityId { get; set; }

    public int ArrivalCityId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int TotalSeats { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual AirlineDetail AirLine { get; set; } = null!;
    [JsonIgnore]
    public virtual CityDetail ArrivalCity { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    [JsonIgnore]
    public virtual CityDetail DepartureCity { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<SeatDetail> SeatDetails { get; set; } = new List<SeatDetail>();
}
