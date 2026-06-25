using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KauFeedback.API.Data;
using KauFeedback.API.Models;
using KauFeedback.API.DTOs;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("departments")]
    public async Task<IActionResult> AddDepartment(
        DepartmentRequest request)
    {
        var department = new Department
        {
            Name = request.Name
        };

        _context.Departments.Add(department);

        await _context.SaveChangesAsync();

        return Ok(department);
    }

    [HttpPost("services")]
    public async Task<IActionResult> AddService(
        ServiceRequest request)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == request.DepartmentId);

        if (department == null)
        {
            return BadRequest("Department not found");
        }

        var service = new Service
        {
            Name = request.Name,
            DepartmentId = request.DepartmentId
        };

        _context.Services.Add(service);

        await _context.SaveChangesAsync();

        return Ok(service);
    }

    [HttpGet("entries")]
    public async Task<IActionResult> GetEntries()
    {
        var entries = await _context.FeedbackEntries
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();

        return Ok(entries);
    }
}