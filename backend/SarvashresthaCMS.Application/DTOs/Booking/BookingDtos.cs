using System;

namespace SarvashresthaCMS.Application.DTOs.Booking;

public record CreateBookingRequest(
    int RoomId,
    int? OfferId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    decimal DiscountAmount,
    decimal FinalPrice
);

public record BookingResponse(
    int Id,
    int RoomId,
    int? OfferId,
    string GuestName,
    string GuestEmail,
    DateTime CheckIn,
    DateTime CheckOut,
    decimal TotalPrice,
    decimal DiscountAmount,
    decimal FinalPrice,
    string Status
);

public record UpdateBookingStatusRequest(string Status);
