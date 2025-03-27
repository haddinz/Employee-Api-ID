using AutoMapper;
using EmployeeApi.Data;
using EmployeeApi.DTO.Request;
using EmployeeApi.Models.Entity;
using EmployeeApi.Models.Interface;
using EmployeeApi.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _repository;

    public EmployeeController(ApplicationDbContext context, IMapper mapper, IEmployeeRepository repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    // GET: api/Employee/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Employee>> GetEmployee(Guid id)
    {
        // var employee = await _context.Employees.FindAsync(id);
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null) return NotFound();

        return Ok(employee);
    }

    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeReq dto)
    {
        //make it simple using auto mapper
        // var employee = new Employee
        // {
        //     Id = Guid.NewGuid(),
        //     Name = dto.Name,
        //     Email = dto.Email,
        //     Username = dto.Username,
        //     Address = dto.Address
        // };

        var employee = _mapper.Map<Employee>(dto);
        employee.Id = Guid.NewGuid();

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // PUT: api/Employee/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeUpdateReq dto)
    {
        if (!ModelState.IsValid){
            return BadRequest(new { Status = "Error", Message = "Invalid data provided.", Errors = ModelState });
        }

        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return NotFound(new { Status = "Error", Message = "Employee not found." });
        }

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.Username = dto.Username;
        employee.Address = dto.Address;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return StatusCode(500, new { Status = "Error", Message = "An error occurred while updating the employee." });
        }

        return Ok(new { Status = "Success", Message = "Employee updated successfully.", Data = employee });
    }

    // DELETE: api/Employee/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
