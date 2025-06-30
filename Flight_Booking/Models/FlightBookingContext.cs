using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Flight_Booking.Models;

public partial class FlightBookingContext : DbContext
{
    public FlightBookingContext()
    {
    }

    public FlightBookingContext(DbContextOptions<FlightBookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AirlineDetail> AirlineDetails { get; set; }

    public virtual DbSet<AirportDetail> AirportDetails { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<BookingStatusDetail> BookingStatusDetails { get; set; }

    public virtual DbSet<CityDetail> CityDetails { get; set; }

    public virtual DbSet<CountryDetail> CountryDetails { get; set; }

    public virtual DbSet<FlightDetail> FlightDetails { get; set; }

    public virtual DbSet<IataDetail> IataDetails { get; set; }

    public virtual DbSet<PassengerDetail> PassengerDetails { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PaymentMethodDetail> PaymentMethodDetails { get; set; }

    public virtual DbSet<PaymentStatusDetail> PaymentStatusDetails { get; set; }

    public virtual DbSet<SeatDetail> SeatDetails { get; set; }

    public virtual DbSet<StateDetail> StateDetails { get; set; }

    public virtual DbSet<TravelClassDetail> TravelClassDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=LAPTOP-1648GI4Q; database=Flight_Booking; trusted_connection=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AirlineDetail>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airline___6B872817D0C3D9CD");

            entity.ToTable("Airline_Details");

            entity.Property(e => e.AirlineId).HasColumnName("AirLineID");
            entity.Property(e => e.AirlineName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
        });

        modelBuilder.Entity<AirportDetail>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airport___E3DBE08A90EF0FA2");

            entity.ToTable("Airport_Details");

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.AirportName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iataid).HasColumnName("IATAID");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.City).WithMany(p => p.AirportDetails)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Airport_D__CityI__48CFD27E");

            entity.HasOne(d => d.Country).WithMany(p => p.AirportDetails)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Airport_D__Count__4AB81AF0");

            entity.HasOne(d => d.Iata).WithMany(p => p.AirportDetails)
                .HasForeignKey(d => d.Iataid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Airport_D__IATAI__4BAC3F29");

            entity.HasOne(d => d.State).WithMany(p => p.AirportDetails)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Airport_D__State__49C3F6B7");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking___73951ACDBE72E2D1");

            entity.ToTable("Booking_Details");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BookingStatusId).HasColumnName("BookingStatusID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.TravelClassId).HasColumnName("TravelClassID");

            entity.HasOne(d => d.Airport).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Airpo__75A278F5");

            entity.HasOne(d => d.BookingStatus).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Booki__797309D9");

            entity.HasOne(d => d.Flight).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Fligh__76969D2E");

            entity.HasOne(d => d.Passenger).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Passe__74AE54BC");

            entity.HasOne(d => d.Seat).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__SeatI__787EE5A0");

            entity.HasOne(d => d.TravelClass).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.TravelClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking_D__Trave__778AC167");
        });

        modelBuilder.Entity<BookingStatusDetail>(entity =>
        {
            entity.HasKey(e => e.BookingStatusId).HasName("PK__BookingS__54F9C0BD727F3339");

            entity.ToTable("BookingStatus_Details");

            entity.HasIndex(e => e.StatusName, "UQ__BookingS__05E7698A6EEB5D2D").IsUnique();

            entity.Property(e => e.BookingStatusId).HasColumnName("BookingStatusID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CityDetail>(entity =>
        {
            entity.HasKey(e => e.CityID).HasName("PK__City_Det__F2D21A9696BC4205");

            entity.ToTable("City_Details");

            entity.Property(e => e.CityID).HasColumnName("CityID");
            entity.Property(e => e.CityNameFull)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CityNameShort)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
        });

        modelBuilder.Entity<CountryDetail>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country___10D160BF53DC0E02");

            entity.ToTable("Country_Details");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
        });

        modelBuilder.Entity<FlightDetail>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight_D__8A9E148E3EB86845");

            entity.ToTable("Flight_Details");

            entity.HasIndex(e => e.FlightNumber, "UQ__Flight_D__2EAE6F504C617F97").IsUnique();

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.AirLineId).HasColumnName("AirLineID");
            entity.Property(e => e.ArrivalCityId).HasColumnName("ArrivalCityID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartureCityId).HasColumnName("DepartureCityID");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Modified).HasColumnType("datetime");

            // if we are not write jsonignor in model so it is used.
            //entity.HasOne(d => d.AirLine).WithMany(p => p.FlightDetails)
            //    .HasForeignKey(d => d.AirLine)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Flight_De__AirLi__534D60F1");

            // if we are  write jsonignor in model so it is used.
            entity.HasOne(d => d.AirLine).WithMany(p => p.FlightDetails)
               .HasForeignKey(d => d.AirLineId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__Flight_De__AirLi__534D60F1");

            entity.HasOne(d => d.ArrivalCity).WithMany(p => p.FlightDetailArrivalCities)
                .HasForeignKey(d => d.ArrivalCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flight_De__Arriv__5535A963");

            entity.HasOne(d => d.DepartureCity).WithMany(p => p.FlightDetailDepartureCities)
                .HasForeignKey(d => d.DepartureCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Flight_De__Depar__5441852A");
        });

        modelBuilder.Entity<IataDetail>(entity =>
        {
            entity.HasKey(e => e.IataId).HasName("PK__IATA_Det__EE3A4AE382D420EF");

            entity.ToTable("IATA_Details");

            entity.HasIndex(e => e.Iatacode, "UQ__IATA_Det__EFD6F5BEE31E0986").IsUnique();

            entity.Property(e => e.IataId).HasColumnName("IATAID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iatacode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IATACode");
            entity.Property(e => e.Modified).HasColumnType("datetime");
        });

        modelBuilder.Entity<PassengerDetail>(entity =>
        {
            entity.HasKey(e => e.PassengerID).HasName("PK__Passenge__88915F90D147668F");

            entity.ToTable("Passenger_Details");

            entity.HasIndex(e => e.PassportNumber, "UQ__Passenge__45809E710310F1F9").IsUnique();

            entity.Property(e => e.PassengerID).HasColumnName("PassengerID");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment___9B556A58B0205AB9");

            entity.ToTable("Payment_Details");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatusID");

            entity.HasOne(d => d.Booking).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment_D__Booki__02084FDA");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment_D__Payme__02FC7413");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.PaymentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment_D__Payme__03F0984C");
        });

        modelBuilder.Entity<PaymentMethodDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F39A9E595A");

            entity.ToTable("PaymentMethod_Details");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentStatusDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId).HasName("PK__PaymentS__34F8AC1F2AAF36E9");

            entity.ToTable("PaymentStatus_Details");

            entity.HasIndex(e => e.StatusName, "UQ__PaymentS__05E7698A365581F9").IsUnique();

            entity.Property(e => e.PaymentStatusId).HasColumnName("PaymentStatusID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SeatDetail>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seat_Det__311713D34E97F02B");

            entity.ToTable("Seat_Details");

            entity.HasIndex(e => new { e.FlightId, e.SeatNumber }, "UQ_Seat_Flight").IsUnique();

            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.IsBooked).HasDefaultValue(false);
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.SeatDetails)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seat_Deta__Fligh__5AEE82B9");
        });

        modelBuilder.Entity<StateDetail>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__State_De__C3BA3B5A977E79BD");

            entity.ToTable("State_Details");

            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.StateName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TravelClassDetail>(entity =>
        {
            entity.HasKey(e => e.TravelClassId).HasName("PK__TravelCl__15CF2C09E6889E39");

            entity.ToTable("TravelClass_Details");

            entity.Property(e => e.TravelClassId).HasColumnName("TravelClassID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Modified).HasColumnType("datetime");
            entity.Property(e => e.TravelClassName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
