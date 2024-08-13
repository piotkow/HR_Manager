using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository : IDisposable
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task InsertDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int departmentId);
        Task UpdateDepartmentAsync(Department department);
        Task SaveAsync();
    }

}
