using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var accounts = _accountRepository.GetAccounts();
            if (accounts.Count == 0)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult AddAccount(AccountPostDTO accountRequestDTO)
        {
            if (_accountRepository.AddAccount(accountRequestDTO))
            {
                return Ok("Create success");
            }
            return BadRequest("Create fail");
        }
    }
}
