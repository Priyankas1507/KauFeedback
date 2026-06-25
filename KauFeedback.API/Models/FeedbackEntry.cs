namespace KauFeedback.API.Models;

public class FeedbackEntry
{
    public int Id { get; set; }

    public string PatientName { get; set; } = string.Empty;

    public int Age { get; set; }

    public string VisitType { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public int ServiceId { get; set; }

    public int LocationId { get; set; }

    public int Rating { get; set; }

    public string Comments { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}