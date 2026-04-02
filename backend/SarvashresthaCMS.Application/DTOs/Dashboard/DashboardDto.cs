using System;
using System.Collections.Generic;

namespace SarvashresthaCMS.Application.DTOs.Dashboard;

public class DashboardDto
{
    public DashboardStatsDto Stats { get; set; } = new();
    public List<RevenueTrendDto> RevenueTrend { get; set; } = new();
    public List<RecentBookingDto> RecentBookings { get; set; } = new();
    public List<RoomStatusDto> RecentStatus { get; set; } = new();
}

public class DashboardStatsDto
{
    public decimal TotalRevenue { get; set; }
    public double RevenueGrowthPercentage { get; set; }
    public double AverageOccupancy { get; set; }
    public double OccupancyGrowthPercentage { get; set; }
    public int NewBookings { get; set; }
    public double BookingsGrowthPercentage { get; set; }
}

public class RevenueTrendDto
{
    public string Month { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal PreviousRevenue { get; set; }
}

public class RecentBookingDto
{
    public int Id { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class RoomStatusDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
