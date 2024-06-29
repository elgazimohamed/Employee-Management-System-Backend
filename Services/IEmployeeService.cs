using System;
using Employee_Management_System_Backend.Models;

namespace Employee_Management_System_Backend.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(Guid id);
        Task Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(Guid id);
    }
}