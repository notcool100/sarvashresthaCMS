using System;

namespace SarvashresthaCMS.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int ResortId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string guestEmail { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Pending";
}
