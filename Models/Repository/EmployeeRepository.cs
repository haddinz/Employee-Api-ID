using EmployeeApi.Data;
using EmployeeApi.Models.Entity;
using EmployeeApi.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Models.Repository;

public class EmployeeRepository : IEmployeeRepository {
    private readonly ApplicationDbContext _context;

    public EmployeeRepository (ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(Guid id){
        return await _context.Employees.FindAsync(id);
    }
}