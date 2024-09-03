using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class AdminDAO
    {
        private readonly StudentManagementContext _context;
        public AdminDAO(StudentManagementContext context)
        {
            _context = context;
        }

        public List<Admin> GetAdmins()
        {
            return _context.Admins.Include(a => a.Account).ToList();
        }

        public Admin GetAdminById(int id)
        {
            return _context.Admins.Include(a => a.Account).FirstOrDefault(a => a.AdminId == id);
        }

        public Admin GetAdminByAccountId(int accountId)
        {
            return _context.Admins.Include(a => a.Account).FirstOrDefault(a => a.AccountId == accountId);
        }

        public int AddAdmin(Admin admin)
        {
            try
            {
                if (admin != null)
                {
                    _context.Admins.Add(admin);
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


    }
}
