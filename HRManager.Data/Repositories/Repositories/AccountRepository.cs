using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


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
            return await context.Accounts.Include(a=>a.Employee).ThenInclude(e => e.Photo).ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await context.Accounts.Include(a=>a.Employee).ThenInclude(e => e.Photo).FirstOrDefaultAsync(a=>a.AccountID==accountId);
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

        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            var account = await context.Accounts.Include(a => a.Employee).ThenInclude(e => e.Photo).FirstOrDefaultAsync(a => a.Username == username);
            return account;
        }

        public async Task<Account> GetAccountByEmployeeAsync(int employeeId)
        {
            var account = await context.Accounts.Include(a=>a.Employee).SingleOrDefaultAsync(e => e.EmployeeID==employeeId);
            return account;
        }
    }


}
