using SarvashresthaCMS.Application.DTOs.Dashboard;
using SarvashresthaCMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Services;

public class DashboardService(
    IBookingRepository bookingRepository,
    IRoomRepository roomRepository) : IDashboardService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;

    public async Task<DashboardDto> GetDashboardDataAsync()
    {
        var totalRevenue = await _bookingRepository.GetTotalRevenueAsync();
        var occupancyRate = await _roomRepository.GetOccupancyRateAsync();
        var newBookings = await _bookingRepository.GetNewBookingsCountAsync(30);

        // Previous period stats (mocked for demo purposes, would normally calculate from DB)
        var lastMonthRevenue = totalRevenue * 0.89m; // 12.4% growth mock
        var lastMonthOccupancy = occupancyRate * 0.95; // 5% growth mock
        var lastMonthBookings = (int)(newBookings * 1.03); // -3% growth mock

        var dashboard = new DashboardDto
        {
            Stats = new DashboardStatsDto
            {
                TotalRevenue = totalRevenue,
                RevenueGrowthPercentage = 12.4,
                AverageOccupancy = Math.Round(occupancyRate, 1),
                OccupancyGrowthPercentage = 5.1,
                NewBookings = newBookings,
                BookingsGrowthPercentage = -2.8
            }
        };

        // Monthly Revenue Trend (Mocked 6 months trend)
        var monthlyRevenue = await _bookingRepository.GetMonthlyRevenueAsync(6);
        dashboard.RevenueTrend = monthlyRevenue.Select(r => new RevenueTrendDto
        {
            Month = r.Month,
            Revenue = r.Revenue,
            PreviousRevenue = r.Revenue * 0.85m // Mocked previous year comparison
        }).ToList();

        // Recent Bookings
        var recentBookingsRaw = await _bookingRepository.GetRecentBookingsAsync(5);
        var rooms = await _roomRepository.GetAllAsync();

        dashboard.RecentBookings = recentBookingsRaw.Select(b => new RecentBookingDto
        {
            Id = b.Id,
            GuestName = b.GuestName,
            RoomName = rooms.FirstOrDefault(r => r.Id == b.RoomId)?.Name ?? "Unknown Room",
            CheckIn = b.CheckIn,
            Status = b.Status,
            Amount = b.TotalPrice
        }).ToList();

        // Recent Status
        var recentRooms = await _roomRepository.GetRecentRoomsAsync(3);
        dashboard.RecentStatus = recentRooms.Select(r => new RoomStatusDto
        {
            Id = r.Id,
            Name = r.Name,
            Status = r.IsAvailable ? "Active" : "Review",
            Subtitle = r.IsAvailable ? "Ready for Check-in" : "Maintenance Check",
            ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945?w=200&h=200&fit=crop" // Mock image
        }).ToList();

        return dashboard;
    }
}
