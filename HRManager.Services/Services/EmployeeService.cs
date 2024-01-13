using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetEmployeesAsync()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeePositionTeamResponse>>(employees);
        }

        public async Task<EmployeePositionTeamResponse> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(employeeId);
            return _mapper.Map<EmployeePositionTeamResponse>(employee);
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.InsertEmployeeAsync(employee);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _unitOfWork.EmployeeRepository.DeleteEmployeeAsync(employeeId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.UpdateEmployeeAsync(employee);
            await _unitOfWork.SaveAsync();
        }
    }

}
