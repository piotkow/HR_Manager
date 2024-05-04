using HRManager.Models.Enums;
using HRManager.Services.DTOs.PhotoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.AccountDTO
{
    public class AccountEmployeeResponse
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public PhotoResponse? Photo { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}
