using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.AccountDTO
{
    public class AccountLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
