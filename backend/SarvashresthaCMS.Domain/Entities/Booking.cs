using System;

namespace SarvashresthaCMS.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int RoomId { get; set; }
    public int? OfferId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string guestEmail { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalPrice { get; set; }
    public int NumberOfPeople { get; set; } = 1;
    public string Status { get; set; } = "Pending";
}
