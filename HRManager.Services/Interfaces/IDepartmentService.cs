using HRManager.Models.Entities;
using HRManager.Services.DTOs.DepartmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        Task<Department> InsertDepartmentAsync(DepartmentRequest departmentReq);
        Task DeleteDepartmentAsync(int departmentId);
        Task UpdateDepartmentAsync(int departmentId, DepartmentRequest departmentReq);
    }

}
