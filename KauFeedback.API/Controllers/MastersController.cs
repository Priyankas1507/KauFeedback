using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KauFeedback.API.Data;

namespace KauFeedback.API.Controllers;

[ApiController]
[Route("api")]
public class MastersController : ControllerBase
{
    private readonly AppDbContext _context;

    public MastersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("masters")]
    public async Task<IActionResult> GetMasters()
    {
        var departments = await _context.Departments.ToListAsync();

        var services = await _context.Services.ToListAsync();

        var locations = await _context.Locations.ToListAsync();

        return Ok(new
        {
            departments,
            services,
            locations
        });
    }
}