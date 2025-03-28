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

    public async Task<IEnumerable<Employee>> GetAllAsync() {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id){
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee?> GetByEmailAsync(String email){
        return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task CreateAsync(Employee dto) {
        _context.Employees.Add(dto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee) {
        var existingEmployee = await _context.Employees.FindAsync(employee.Id);
        if (existingEmployee != null) {
            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid id) {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null) {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}