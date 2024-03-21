
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web_Api.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeeAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            await _employeeService.CreateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employee);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var existingEmployee = await _employeeService.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

    }
}

