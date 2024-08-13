using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Data.Repositories.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HRManagerDbContext context;

        public DepartmentRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await context.Departments.FindAsync(departmentId);
        }

        public async Task InsertDepartmentAsync(Department department)
        {
            await context.Departments.AddAsync(department);
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            Department department = await context.Departments.FindAsync(departmentId);
            if (department != null)
            {
                context.Departments.Remove(department);
            }
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            context.Departments.Update(department);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
