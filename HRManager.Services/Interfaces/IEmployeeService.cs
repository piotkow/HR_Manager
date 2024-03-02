using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeePositionTeamResponse>> GetEmployeesAsync();
        Task<EmployeePositionTeamResponse> GetEmployeeByIdAsync(int employeeId);
        Task<Employee> InsertEmployeeAsync(EmployeeRequest employeeReq);
        Task DeleteEmployeeAsync(int employeeId);
        Task UpdateEmployeeAsync(int employeeId, EmployeeRequest employeeReq);
    }

}
