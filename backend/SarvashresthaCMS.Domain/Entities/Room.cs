namespace SarvashresthaCMS.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public string ViewType { get; set; } = string.Empty;
    public string[] Amenities { get; set; } = [];
    public string[] ImageUrls { get; set; } = [];
    public string BedType { get; set; } = string.Empty;
    public int SizeSqFt { get; set; }
}
