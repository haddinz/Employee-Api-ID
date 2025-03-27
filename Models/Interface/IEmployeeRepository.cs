using EmployeeApi.Models.Entity;

namespace EmployeeApi.Models.Interface;

public interface IEmployeeRepository {
    Task<Employee?> GetByIdAsync(Guid id);
}