using AutoMapper;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.Interfaces;

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

        public async Task<AccountEmployeeResponse> GetAccountByUsernameAsync(string username)
        {
            var account = await _unitOfWork.AccountRepository.GetAccountByUsernameAsync(username);
            return _mapper.Map<AccountEmployeeResponse>(account);
        }

        public async Task<Account> InsertAccountAsync(AccountRequest accountReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            string hashPassword = PasswordHasher.PasswordHasher.HashPassword(accountReq.Password);
            accountReq.Password = hashPassword;
            var account = _mapper.Map<Account>(accountReq);
            await _unitOfWork.AccountRepository.InsertAccountAsync(account);
            await _unitOfWork.CommitAsync();
            return account;
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            await _unitOfWork.AccountRepository.DeleteAccountAsync(accountId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAccountAsync(int accountId, AccountRequest accountReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var account = await _unitOfWork.AccountRepository.GetAccountByIdAsync(accountId);
            account.EmployeeID = accountReq.EmployeeID;
            account.Username = accountReq.Username;
            account.Password = accountReq.Password;
            account.AccountType = accountReq.AccountType;
            await _unitOfWork.AccountRepository.UpdateAccountAsync(account);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AccountEmployeeResponse> AuthenticateUser(AccountLoginRequest user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    return null;
                }

                var existingUser = await _unitOfWork.AccountRepository.GetAccountByUsernameAsync(user.Username);

                bool correctPassword = PasswordHasher.PasswordHasher.VerifyPassword(user.Password, existingUser.Password);

                if (existingUser != null && correctPassword)
                {
                    return _mapper.Map<AccountEmployeeResponse>(existingUser);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }

}
