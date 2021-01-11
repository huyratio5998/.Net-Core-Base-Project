using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool CheckLogin(string username, string password);
        int? getNewId();        
       
    }
}
