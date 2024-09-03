
using DTO.Common;
using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        public AuthenController(IAdminRepository adminRepository, IAccountRepository accountRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        [HttpPost("register-admin")]
        public IActionResult RegisterAdmin(AdminRegisterModel adminRegisterModel)
        {
            AccountPostDTO accountRequestDTO = new AccountPostDTO();
            accountRequestDTO.Email = adminRegisterModel.Email;
            accountRequestDTO.Password = adminRegisterModel.Password;
            accountRequestDTO.RoleId = ConstantRole.EDU_MANGEMENT;
            accountRequestDTO.StatusId = true;
            try
            {
                if (_accountRepository.AddAccount(accountRequestDTO))
                {
                    AdminPostDTO adminRequestDTO = new AdminPostDTO();
                    adminRequestDTO.Name = adminRegisterModel.Name;
                    adminRequestDTO.AccountId = _accountRepository.GetAccountByEmail(adminRegisterModel.Email).AccountId;
                    try
                    {
                        if (_adminRepository.AddAdmin(adminRequestDTO))
                        {
                            return Ok(new ResponseMessage { Status = true, Message = "Create success" });
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                return BadRequest(new ResponseMessage { Status = true, Message = "Create fail" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(ErrorMessage.DUPLICATE))
                {
                    return BadRequest(new ResponseMessage { Status = false, Message = "Account has already existed" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
                }
            }
        }


        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var account = _accountRepository.GetAccountByEmail(loginModel.Email);
            if (account == null)
            {
                return Unauthorized(new ResponseMessage { Status = false, Message = "Account not exist" });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, account.Password))
            {
                return Unauthorized(new ResponseMessage { Status = false, Message = "Password is incorrect" });
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Sid, account.AccountId.ToString()),
                new Claim(ClaimTypes.Role, account.Role.RoleName)

            };
            var token = GetToken(authClaims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
               issuer: _configuration["JWT:ValidIssuer"],
               audience: _configuration["JWT:ValidAudience"],
               expires: DateTime.Now.AddSeconds(10),
               claims: authClaims,
               signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
           );
            return token;


        }
    }
}
