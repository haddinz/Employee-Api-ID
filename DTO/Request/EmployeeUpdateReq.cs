namespace EmployeeApi.DTO.Request;

public class EmployeeUpdateReq{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Username { get; set; }
    public string? Address { get; set; }
}