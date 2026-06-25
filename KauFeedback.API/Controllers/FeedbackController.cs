using Microsoft.AspNetCore.Mvc;
using KauFeedback.API.Data;
using KauFeedback.API.DTOs;
using KauFeedback.API.Models;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api")]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeedbackController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("feedback")]
    public async Task<IActionResult> SubmitFeedback(
        FeedbackRequest request)
    {
        if (request.Rating < 1 || request.Rating > 5)
        {
            return BadRequest("Rating must be between 1 and 5");
        }

        var feedback = new FeedbackEntry
        {
            PatientName = request.PatientName,
            Age = request.Age,
            VisitType = request.VisitType,
            DepartmentId = request.DepartmentId,
            ServiceId = request.ServiceId,
            LocationId = request.LocationId,
            Rating = request.Rating,
            Comments = request.Comments,
            CreatedAt = DateTime.UtcNow
        };

        _context.FeedbackEntries.Add(feedback);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Feedback submitted successfully"
        });
    }
}