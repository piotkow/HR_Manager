using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountEmployeeResponse>> GetAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAccountsAsync();
            return _mapper.Map<IEnumerable<AccountEmployeeResponse>>(accounts);
        }

        public async Task<AccountEmployeeResponse> GetAccountByIdAsync(int accountId)
        {
            var account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(accountId);
            return _mapper.Map<AccountEmployeeResponse>(account);
        }

        public async Task InsertAccountAsync(Account account)
        {
            await _unitOfWork.AccountRepository.InsertAccountAsync(account);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _unitOfWork.AccountRepository.DeleteAccountAsync(accountId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _unitOfWork.AccountRepository.UpdateAccountAsync(account);
            await _unitOfWork.SaveAsync();
        }
    }

}
