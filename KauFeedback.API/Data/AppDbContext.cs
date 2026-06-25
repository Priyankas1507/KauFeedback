using Microsoft.EntityFrameworkCore;
using KauFeedback.API.Models;

namespace KauFeedback.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Service> Services => Set<Service>();

    public DbSet<Location> Locations => Set<Location>();

    public DbSet<FeedbackEntry> FeedbackEntries => Set<FeedbackEntry>();
}