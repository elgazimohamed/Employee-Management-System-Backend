using Employee_Management_System_Backend.Data;
using Employee_Management_System_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Backend.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly ApplicationDBContext _context;

        public EmployeeService(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<bool> Add(Employee employee)
        {
            try
            {
                if (await _context.Employees.AnyAsync(e => e.Email == employee.Email))
                {
                    return false; // Email already in use
                }

                // Ensure the employee Id is set if not provided
                if (employee.Id == Guid.Empty)
                {
                    employee.Id = Guid.NewGuid();
                }

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return true; // Employee added successfully
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error when adding a new employee: ", ex);
            }
        }
        public async Task<bool> Update(Employee employee)
        {
            try
            {
                if (await _context.Employees.AnyAsync(e => e.Email == employee.Email && e.Id != employee.Id))
                {
                    return false; // Email already in use by another employee
                }

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return true; // Employee updated successfully
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error when updating the employee : ", ex);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false; // Employee not found
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true; // Employee deleted successfully
        }


    }
}