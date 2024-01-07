using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository : IDisposable
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task InsertEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task UpdateEmployeeAsync(Employee employee);
        Task SaveAsync();
    }

}
