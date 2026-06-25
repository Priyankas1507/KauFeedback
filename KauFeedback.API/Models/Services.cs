namespace KauFeedback.API.Models;

public class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
}