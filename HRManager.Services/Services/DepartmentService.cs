using AutoMapper;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.DepartmentDTO;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetDepartmentsAsync();
            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(departmentId);
            return department;
        }

        public async Task<Department> InsertDepartmentAsync(DepartmentRequest departmentReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var department = _mapper.Map<Department>(departmentReq);
            await _unitOfWork.DepartmentRepository.InsertDepartmentAsync(department);
            await _unitOfWork.CommitAsync();
            return department;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            await _unitOfWork.DepartmentRepository.DeleteDepartmentAsync(departmentId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDepartmentAsync(int departmentId, DepartmentRequest departmentReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var department = await _unitOfWork.DepartmentRepository.GetDepartmentByIdAsync(departmentId);
            _mapper.Map(departmentReq, department);
            await _unitOfWork.DepartmentRepository.UpdateDepartmentAsync(department);
            await _unitOfWork.CommitAsync();
        }
    }

}
