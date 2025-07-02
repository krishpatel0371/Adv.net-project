
----------------Passenger Table----------------
CREATE TABLE Passenger_Details (
    PassengerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(60) NOT NULL,
    LastName VARCHAR(60) NOT NULL,
    Gender CHAR(1) NOT NULL,
    DOB DATE NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(10) NOT NULL,
    PassportNumber VARCHAR(15) NOT NULL UNIQUE,
    Address VARCHAR(300) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);


INSERT INTO Passenger_Details (FirstName, LastName, Gender, DOB, Email, PhoneNumber, PassportNumber, Address, Modified) VALUES
('John', 'Doe', 'M', '1990-01-01', 'john@example.com', '9876543210', 'A123456', 'Mumbai, India', GETDATE()),
('Jane', 'Smith', 'F', '1985-05-12', 'jane@example.com', '8765432109', 'B234567', 'San Francisco, USA', GETDATE()),
('Raj', 'Kapoor', 'M', '1978-03-30', 'raj@example.com', '9988776655', 'C345678', 'Munich, Germany', GETDATE()),
('Anna', 'White', 'F', '1995-07-22', 'anna@example.com', '8899001122', 'D456789', 'Brisbane, Australia', GETDATE()),
('Tom', 'Brown', 'M', '1992-11-11', 'tom@example.com', '7788990011', 'E567890', 'Toronto, Canada', GETDATE());

select * from Passenger_Details

----------------City Table----------------
CREATE TABLE City_Details (
    CityID INT PRIMARY KEY IDENTITY(1,1),
    CityNameFull VARCHAR(60) NOT NULL,
    CityNameShort VARCHAR(3) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO City_Details (CityNameFull, CityNameShort, Modified) VALUES
('Mumbai', 'BOM', GETDATE()),
('San Francisco', 'SFO', GETDATE()),
('Munich', 'MUC', GETDATE()),
('Brisbane', 'BNE', GETDATE()),
('Toronto', 'YYZ', GETDATE());


----------------State Table----------------
CREATE TABLE State_Details (
    StateID INT PRIMARY KEY IDENTITY(1,1),
    StateName VARCHAR(60) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL 
);

INSERT INTO State_Details (StateName, Modified) VALUES
('Maharashtra', GETDATE()),
('California', GETDATE()),
('Bavaria', GETDATE()),
('Queensland', GETDATE()),
('Ontario', GETDATE());


----------------Country Table----------------
CREATE TABLE Country_Details (
    CountryID INT PRIMARY KEY IDENTITY(1,1),
    CountryName VARCHAR(60) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL 
);

INSERT INTO Country_Details (CountryName, Modified) VALUES
('India', GETDATE()),
('USA', GETDATE()),
('Germany', GETDATE()),
('Australia', GETDATE()),
('Canada', GETDATE());


----------------IATA Table----------------
CREATE TABLE IATA_Details (
    IATAID INT PRIMARY KEY IDENTITY(1,1),
    IATACode VARCHAR(3) NOT NULL UNIQUE,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO IATA_Details (IATACode, Modified) VALUES
('BOM', GETDATE()),
('SFO', GETDATE()),
('MUC', GETDATE()),
('BNE', GETDATE()),
('YYZ', GETDATE());


----------------Airport Table----------------
CREATE TABLE Airport_Details (
    AirportID INT PRIMARY KEY IDENTITY(1,1),
    AirportName VARCHAR(250) NOT NULL,
    CityID INT NOT NULL,
    StateID INT NOT NULL,
    CountryID INT NOT NULL,
    IATAID INT NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (CityID) REFERENCES City_Details(CityID),
    FOREIGN KEY (StateID) REFERENCES State_Details(StateID),
    FOREIGN KEY (CountryID) REFERENCES Country_Details(CountryID),
    FOREIGN KEY (IATAID) REFERENCES IATA_Details(IATAID)
);

INSERT INTO Airport_Details (AirportName, CityID, StateID, CountryID, IATAID, Modified) VALUES
('Mumbai Airport', 1, 1, 1, 1, GETDATE()),
('SFO Airport', 2, 2, 2, 2, GETDATE()),
('Munich Airport', 3, 3, 3, 3, GETDATE()),
('Brisbane Airport', 4, 4, 4, 4, GETDATE()),
('Toronto Airport', 5, 5, 5, 5, GETDATE());

----------------Airline Table----------------
CREATE TABLE Airline_Details (
    AirLineID INT PRIMARY KEY IDENTITY(1,1),
    AirlineName VARCHAR(50) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO Airline_Details (AirlineName, Modified) VALUES
('Air India', GETDATE()),
('United Airlines', GETDATE()),
('Lufthansa', GETDATE()),
('Qantas', GETDATE()),
('Air Canada', GETDATE());

----------------Flight Table----------------
CREATE TABLE Flight_Details (
    FlightID INT PRIMARY KEY IDENTITY(1,1),
    FlightNumber VARCHAR(20) NOT NULL UNIQUE,
    AirLineID INT NOT NULL,
    DepartureCityID INT NOT NULL,
    ArrivalCityID INT NOT NULL,
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    TotalSeats INT NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (AirLineID) REFERENCES Airline_Details(AirLineID),
    FOREIGN KEY (DepartureCityID) REFERENCES City_Details(CityID),
    FOREIGN KEY (ArrivalCityID) REFERENCES City_Details(CityID)
);

INSERT INTO Flight_Details (FlightNumber, AirLineID, DepartureCityID, ArrivalCityID, DepartureTime, ArrivalTime, TotalSeats, Modified) VALUES
('AI101', 1, 1, 2, GETDATE(), DATEADD(HOUR, 8, GETDATE()), 180, GETDATE()),
('UA202', 2, 2, 3, GETDATE(), DATEADD(HOUR, 9, GETDATE()), 200, GETDATE()),
('LH303', 3, 3, 4, GETDATE(), DATEADD(HOUR, 10, GETDATE()), 150, GETDATE()),
('QF404', 4, 4, 5, GETDATE(), DATEADD(HOUR, 11, GETDATE()), 160, GETDATE()),
('AC505', 5, 5, 1, GETDATE(), DATEADD(HOUR, 12, GETDATE()), 170, GETDATE());


----------------Seat Table----------------
CREATE TABLE Seat_Details (
    SeatID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT NOT NULL,
    SeatNumber VARCHAR(3) NOT NULL,
    IsBooked BIT DEFAULT 0,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (FlightID) REFERENCES Flight_Details(FlightID),
    CONSTRAINT UQ_Seat_Flight UNIQUE (FlightID, SeatNumber)
);

INSERT INTO Seat_Details (FlightID, SeatNumber, IsBooked, Modified) VALUES
(1, '1A', 0, GETDATE()),
(2, '2B', 1, GETDATE()),
(3, '3C', 0, GETDATE()),
(4, '4D', 1, GETDATE()),
(5, '5E', 0, GETDATE());

----------------TravelClass Table----------------
CREATE TABLE TravelClass_Details (
    TravelClassID INT PRIMARY KEY IDENTITY(1,1),
    TravelClassName VARCHAR(10) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO TravelClass_Details (TravelClassName, Modified) VALUES
('Economy', GETDATE()),
('Business', GETDATE()),
('First', GETDATE()),
('Premium', GETDATE()),
('Basic', GETDATE());

----------------Booking_Status Table----------------
CREATE TABLE BookingStatus_Details (
    BookingStatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(20) NOT NULL UNIQUE,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO BookingStatus_Details (StatusName, Modified) VALUES
('Booked', GETDATE()),
('Cancelled', GETDATE()),
('CheckedIn', GETDATE()),
('Boarded', GETDATE()),
('Completed', GETDATE());

----------------Booking Table----------------
CREATE TABLE Booking_Details (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    PassengerID INT NOT NULL,
    AirportID INT NOT NULL,
    FlightID INT NOT NULL,
    TravelClassID INT NOT NULL,
    SeatID INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    BookingStatusID INT NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,

    FOREIGN KEY (PassengerID) REFERENCES Passenger_Details(PassengerID),
    FOREIGN KEY (AirportID) REFERENCES Airport_Details(AirportID),
    FOREIGN KEY (FlightID) REFERENCES Flight_Details(FlightID),
    FOREIGN KEY (TravelClassID) REFERENCES TravelClass_Details(TravelClassID),
    FOREIGN KEY (SeatID) REFERENCES Seat_Details(SeatID),
    FOREIGN KEY (BookingStatusID) REFERENCES BookingStatus_Details(BookingStatusID)
);

INSERT INTO Booking_Details (PassengerID, AirportID, FlightID, TravelClassID, SeatID, BookingDate, BookingStatusID, Modified)
VALUES 
(1, 1, 1, 1, 1, GETDATE(), 1, GETDATE()),  -- Booked
(2, 2, 2, 2, 2, GETDATE(), 2, GETDATE()),  -- Cancelled
(3, 3, 3, 3, 3, GETDATE(), 3, GETDATE()),  -- CheckedIn
(4, 4, 4, 4, 4, GETDATE(), 4, GETDATE()),  -- Boarded
(5, 5, 5, 5, 5, GETDATE(), 5, GETDATE());  -- Completed


----------------PaymentMethod Table----------------
CREATE TABLE PaymentMethod_Details (
    PaymentMethodID INT PRIMARY KEY IDENTITY(1,1),
    PaymentMethod VARCHAR(20) NOT NULL,
	Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO PaymentMethod_Details (PaymentMethod, Modified) VALUES
('Credit Card', GETDATE()),
('Debit Card', GETDATE()),
('Net Banking', GETDATE()),
('UPI', GETDATE()),
('Cash', GETDATE());

----------------PaymentStatus_Details Table----------------
CREATE TABLE PaymentStatus_Details (
    PaymentStatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName VARCHAR(20) NOT NULL UNIQUE,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

INSERT INTO PaymentStatus_Details (StatusName, Modified) VALUES
('Paid', GETDATE()),
('Pending', GETDATE()),
('Failed', GETDATE()),
('Refunded', GETDATE()),
('Declined', GETDATE());

----------------Payment Table----------------
CREATE TABLE Payment_Details (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT NOT NULL,
    PaymentDate DATE NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethodID INT NOT NULL,
    PaymentStatusID INT NOT NULL, -- FK to PaymentStatus_Details
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    FOREIGN KEY (BookingID) REFERENCES Booking_Details(BookingID),
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethod_Details(PaymentMethodID),
    FOREIGN KEY (PaymentStatusID) REFERENCES PaymentStatus_Details(PaymentStatusID)
);

INSERT INTO Payment_Details (
    BookingID, PaymentDate, Amount, PaymentMethodID, PaymentStatusID, Modified
) VALUES
(1, '2025-06-25', 4500.00, 1, 1, GETDATE()),  -- Paid with Method 1
(2, '2025-06-25', 3800.50, 2, 2, GETDATE()),  -- Pending with Method 2
(3, '2025-06-25', 5200.75, 3, 3, GETDATE()),  -- Failed with Method 3
(4, '2025-06-26', 6100.00, 4, 1, GETDATE()),  -- Paid with Method 4
(5, '2025-06-27', 2950.20, 5, 4, GETDATE());  -- Refunded with Method 5






