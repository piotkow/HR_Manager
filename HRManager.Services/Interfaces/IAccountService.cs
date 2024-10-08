﻿using HRManager.Models.Entities;
using HRManager.Services.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountEmployeeResponse>> GetAccountsAsync();
        Task<AccountEmployeeResponse> GetAccountByIdAsync(int accountId);
        Task<AccountEmployeeResponse> GetAccountByEmployeeAsync(int employeeId);
        Task<Account> InsertAccountAsync(AccountRequest accountReq);
        Task DeleteAccountAsync(int accountId);
        Task UpdateAccountAsync(int accountId, AccountRequest accountReq);
        Task<AccountEmployeeResponse> AuthenticateUser(AccountLoginRequest user);
    }



}
