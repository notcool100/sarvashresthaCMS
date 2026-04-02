using System;

namespace SarvashresthaCMS.Domain.Entities;

public class Offer
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DiscountType { get; set; } = "percent"; // percent | fixed
    public decimal DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public bool AppliesToAllRooms { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
