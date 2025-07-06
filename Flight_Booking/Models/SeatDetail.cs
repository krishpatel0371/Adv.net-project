using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class SeatDetail
{
    public int SeatId { get; set; }

    public int FlightId { get; set; }

    public string SeatNumber { get; set; }

    public bool IsBooked { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [JsonIgnore]
    public virtual FlightDetail? Flight { get; set; } = null!;
}
