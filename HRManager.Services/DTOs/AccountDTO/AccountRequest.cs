using HRManager.Models.Entities;
using HRManager.Models.Enums;

namespace HRManager.Services.DTOs.AccountDTO
{
    public class AccountRequest
    {
        public required int EmployeeID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required Role AccountType { get; set; }
    }
}
