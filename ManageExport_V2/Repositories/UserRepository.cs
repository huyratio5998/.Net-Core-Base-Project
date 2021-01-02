using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ExportContext context): base(context)
        {

        }
        public int getNewId()
        {
            return _context.Users.OrderByDescending(x => x.Id).FirstOrDefault().Id;
        }
        public bool CheckLogin(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                return _context.Users.Any(x => x.Username.Equals(username) && x.Password.Equals(password));
            }
            return false;
        }

           
    }
}
