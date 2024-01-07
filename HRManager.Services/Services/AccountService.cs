using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountEmployeeResponse>> GetAccountsAsync()
        {
            var accounts = await _accountRepository.GetAccountsAsync();
            return _mapper.Map<IEnumerable<AccountEmployeeResponse>>(accounts);
        }

        public async Task<AccountEmployeeResponse> GetAccountByIdAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            return _mapper.Map<AccountEmployeeResponse>(account);
        }


        public async Task InsertAccountAsync(Account account)
        {
            await _accountRepository.InsertAccountAsync(account);
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _accountRepository.DeleteAccountAsync(accountId);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAccountAsync(account);
        }
    }

}
