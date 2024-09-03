using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DTO.Common;
using DTO.GetDTO;
using DTO.PostDTO;
using Repository.IRepository;

namespace Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly StudentManagementContext _context;

        public AccountRepository(IMapper mapper, StudentManagementContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public bool AddAccount(AccountPostDTO accountRequestDTO)
        {
            if (accountRequestDTO == null)
            {
                throw new Exception(ErrorMessage.NULL);
            }

            AccountDAO accountDAO = new AccountDAO(_context);
            if (accountDAO.GetAccountByEmail(accountRequestDTO.Email) != null)
            {
                throw new Exception(ErrorMessage.DUPLICATE);
            }

            try
            {
                return accountDAO.AddCount(_mapper.Map<Account>(accountRequestDTO)) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public AccountGetDTO GetAccountByEmail(string email)
        {
            AccountDAO accountDAO = new AccountDAO(_context);
            return _mapper.Map<AccountGetDTO>(accountDAO.GetAccountByEmail(email));
        }

        public AccountGetDTO GetAccountByEmailAndPassword(string email, string password)
        {
            AccountDAO acctDAO = new AccountDAO(_context);
            return _mapper.Map<AccountGetDTO>(acctDAO.GetAccountByEmailAndPassword(email, password));
        }

        public AccountGetDTO GetAccountById(int id)
        {
            AccountDAO accountDAO = new AccountDAO(_context);
            return _mapper.Map<AccountGetDTO>(accountDAO.GetAccountById(id));
        }

        public List<AccountGetDTO> GetAccounts()
        {
            AccountDAO accountDAO = new AccountDAO(_context);
            return _mapper.Map<List<AccountGetDTO>>(accountDAO.GetAccounts());
        }
    }
}
