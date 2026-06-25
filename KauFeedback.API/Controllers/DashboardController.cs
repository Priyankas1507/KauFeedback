using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KauFeedback.API.Data;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var today = DateTime.UtcNow.Date;

        var todayFeedbackCount =
            await _context.FeedbackEntries
                .CountAsync(f => f.CreatedAt.Date == today);

        var averageRating =
            await _context.FeedbackEntries
                .AverageAsync(f => (double?)f.Rating) ?? 0;

        var latestFeedbacks =
            await _context.FeedbackEntries
                .OrderByDescending(f => f.CreatedAt)
                .Take(10)
                .ToListAsync();

        var departmentRatings =
            await _context.FeedbackEntries
                .Join(
                    _context.Departments,
                    f => f.DepartmentId,
                    d => d.Id,
                    (f, d) => new { d.Name, f.Rating }
                )
                .GroupBy(x => x.Name)
                .Select(g => new
                {
                    Department = g.Key,
                    AverageRating = g.Average(x => x.Rating)
                })
                .ToListAsync();

        return Ok(new
        {
            todayFeedbackCount,
            averageRating,
            departmentRatings,
            latestFeedbacks
        });
    }
}