using EmployeeApi.DTO.Request;
using EmployeeApi.Models.Entity;

namespace EmployeeApi.Models.Interface;

public interface IEmployeeRepository {
    Task<IEnumerable<Employee?>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task CreateAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
    
}