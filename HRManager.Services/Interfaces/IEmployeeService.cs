using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task InsertEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task UpdateEmployeeAsync(Employee employee);
    }

}
