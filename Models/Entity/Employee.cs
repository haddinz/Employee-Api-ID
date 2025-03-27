namespace EmployeeApi.Models.Entity;

public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Username { get; set; }
    public string? Address { get; set; }
}