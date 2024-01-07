using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HRManagerDbContext context;

        public AccountRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await context.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await context.Accounts.FindAsync(accountId);
        }

        public async Task InsertAccountAsync(Account account)
        {
            await context.Accounts.AddAsync(account);
        }

        public async Task DeleteAccountAsync(int accountId)
        {
            Account account = await context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                context.Accounts.Remove(account);
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            context.Accounts.Update(account);
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
