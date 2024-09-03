using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private StudentManagementContext _context;
        public AccountDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.Include(a => a.Role).ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.Include(a => a.Role).FirstOrDefault(a => a.AccountId == id);
        }

        public int AddCount(Account account)
        {
            try
            {
                if (account != null)
                {
                    account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                    _context.Accounts.Add(account);
                    return _context.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public Account? GetAccountByEmail(string email)
        {
            return _context.Accounts.Include(a => a.Role).FirstOrDefault(a => (a.Email ?? String.Empty).Equals(email));
        }

        public Account GetAccountByEmailAndPassword(string email, string password)
        {
            return _context.Accounts.FirstOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
        }
    }
}
