using Employee_Management_System_Backend.Data;
using Employee_Management_System_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_Backend.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public EmployeeController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _context.Employees.ToList(); // get all employees from the database
            return Ok(employees);  // return all employees
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById([FromRoute] int id)
        {
            var employee = _context.Employees.Find(id); // get employee by id
            if (employee == null)
            {
                return NotFound(); // return 404 Not Found
            }
            return Ok(employee); // return employee by id    
        }

    }

}