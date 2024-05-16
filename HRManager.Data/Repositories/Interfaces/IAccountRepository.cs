using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IDisposable
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(int accountId);
        Task InsertAccountAsync(Account account);
        Task DeleteAccountAsync(int accountId);
        Task UpdateAccountAsync(Account account);
        Task<Account> GetAccountByUsernameAsync(string username);
        Task SaveAsync();
    }

}
