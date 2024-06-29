using System;
using Employee_Management_System_Backend.Data;
using Employee_Management_System_Backend.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}