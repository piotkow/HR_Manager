using HRManager.Models.Entities;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountEmployeeResponse>> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            return accounts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountEmployeeResponse>> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAccount([FromBody]AccountRequest accountReq)
        {
            var insertedAccount = await _accountService.InsertAccountAsync(accountReq);
            return CreatedAtAction("GetAccountById", new { id = insertedAccount.AccountID }, insertedAccount);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody]AccountRequest accountReq)
        {
            await _accountService.UpdateAccountAsync(id,accountReq);
            return Ok(accountReq);
        }
    }

}
