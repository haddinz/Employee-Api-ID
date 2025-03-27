namespace EmployeeApi.DTO.Response;

public class LoginRes {
    public string? Name {get; set;}
    public string? Password {get; set;}
    public int ExpiresIn {get; set;}
}