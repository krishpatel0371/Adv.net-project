using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flight_Booking.Models;

public partial class PassengerDetail
{
    public int PassengerID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
