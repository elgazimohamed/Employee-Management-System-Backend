using Employee_Management_System_Backend.Data;
using Employee_Management_System_Backend.Models;
using Employee_Management_System_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_Backend.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
            var result = await _employeeService.Add(employee);
            if (!result)
            {
                return BadRequest("Email already in use.");
            }
            return Ok("Employee added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }
            var result = await _employeeService.Update(employee);
            if (!result)
            {
                return BadRequest("Email already in use by another employee.");
            }
            return Ok("Employee updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _employeeService.Delete(id);
            if (!result)
            {
                return NotFound("Employee not found.");
            }
            return Ok("Employee deleted successfully.");
        }

    }

}