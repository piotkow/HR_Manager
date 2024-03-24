using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRManagerDbContext context;

        public EmployeeRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await context.Employees.Include(e=>e.Position).Include(e=>e.Team).Include(e=>e.Photo).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await context.Employees.Include(e=>e.Position).Include(e=>e.Team).Include(e=>e.Photo).FirstOrDefaultAsync(e=>e.EmployeeID==employeeId);
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            Employee employee = await context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            context.Employees.Update(employee);
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
