using AutoMapper;
using EmployeeApi.DTO.Request;
using EmployeeApi.Models.Entity;
using EmployeeApi.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _repository;

    public EmployeeController(IMapper mapper, IEmployeeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        var employee = await _repository.GetAllAsync();
        return Ok(employee);
    }

    // GET: api/Employee/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Employee>> GetEmployee(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null) return NotFound();

        return Ok(employee);
    }

    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeReq dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        employee.Id = Guid.NewGuid();

        await _repository.CreateAsync(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }

    // PUT: api/Employee/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeUpdateReq dto)
    {
        if (!ModelState.IsValid){
            return BadRequest(new { Status = "Error", Message = "Invalid data provided.", Errors = ModelState });
        }

        var employeeId = await _repository.GetByIdAsync(id);

        if (employeeId == null){
            return NotFound(new { Status = "Error", Message = "Employee not found." });
        }

        var employee = _mapper.Map<Employee>(dto);

        try
        {
            await _repository.UpdateAsync(employee);
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
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}
