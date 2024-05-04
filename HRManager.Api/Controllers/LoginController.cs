using HRManager.Models.Entities;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountLoginRequest req)
        {
            AccountEmployeeResponse authenticatedUser = await _accountService.AuthenticateUser(req);

            if (authenticatedUser != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7e7355946837449c83f32d53cbe8f74849a36589b8b83b2e6f6a28c5e72e4e7d"));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, authenticatedUser.AccountID.ToString()),
                    new Claim(ClaimTypes.Name, authenticatedUser.Username),
                    new Claim("userID", authenticatedUser.AccountID.ToString()),
                    new Claim("accountType", authenticatedUser.AccountType.ToString()),
                    //new Claim("email", authenticatedUser.Email),
                    //new Claim("firstName", authenticatedUser.FirstName),
                    //new Claim("lastName", authenticatedUser.LastName),
                    //new Claim("dateOfEmployment", authenticatedUser.DateOfEmployment.ToString("yyyy-MM-dd")),
                    //new Claim("Photo", authenticatedUser.Photo.Uri),
                    //new Claim("phone", authenticatedUser.Phone),
                };

                var tokenOptions = new JwtSecurityToken(
                     issuer: "http://localhost:5196",
                     audience: "http://localhost:4200",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
                     );

                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new LoginResponse(token));
            }

            return Unauthorized();
        }
    }
}
