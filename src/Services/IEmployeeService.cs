
using Employee_Management_System_Backend.Models;

namespace Employee_Management_System_Backend.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(Guid id);
        Task<bool> Add(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(Guid id);
    }
}