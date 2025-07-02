using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class BookingDetail
{
    public int BookingId { get; set; }

    public int PassengerId { get; set; }

    public int AirportId { get; set; }

    public int FlightId { get; set; }

    public int TravelClassId { get; set; }

    public int SeatId { get; set; }

    public DateTime BookingDate { get; set; }

    public int BookingStatusId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    [JsonIgnore]
    public virtual AirportDetail? Airport { get; set; } = null!;

    [JsonIgnore]
    public virtual BookingStatusDetail? BookingStatus { get; set; } = null!;

    [JsonIgnore]
    public virtual FlightDetail? Flight { get; set; } = null!;

    [JsonIgnore]
    public virtual PassengerDetail? Passenger { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    [JsonIgnore]
    public virtual SeatDetail? Seat { get; set; } = null!;

    [JsonIgnore]
    public virtual TravelClassDetail? TravelClass { get; set; } = null!;
}
