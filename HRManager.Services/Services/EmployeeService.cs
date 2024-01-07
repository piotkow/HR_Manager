using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper= mapper;
        }

        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeePositionTeamResponse>>(employees);
        }

        public async Task<EmployeePositionTeamResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            return _mapper.Map<EmployeePositionTeamResponse>(employee);
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await _employeeRepository.InsertEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }

}
