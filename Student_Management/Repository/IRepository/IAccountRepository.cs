using DTO.GetDTO;
using DTO.PostDTO;

namespace Repository.IRepository
{
    public interface IAccountRepository
    {
        public List<AccountGetDTO> GetAccounts();

        public AccountGetDTO GetAccountById(int id);

        public bool AddAccount(AccountPostDTO accountRequestDTO);
        public AccountGetDTO GetAccountByEmail(string email);
        public AccountGetDTO GetAccountByEmailAndPassword(string email, string password);

    }
}