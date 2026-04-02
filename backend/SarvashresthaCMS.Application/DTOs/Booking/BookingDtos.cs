using System;

namespace SarvashresthaCMS.Application.DTOs.Booking;

public record CreateBookingRequest(
    int ResortId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice
);

public record BookingResponse(
    int Id,
    int ResortId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    string Status
);

public record UpdateBookingStatusRequest(string Status);
