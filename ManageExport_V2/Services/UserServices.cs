using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManageExport_V2.Services
{
    public class UserServices : IUserServices
    {
        private ExportContext _context{ get; set; }
        public UserServices(ExportContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetSubsidiaryAgent(string str)
        {
            try
            {
                var subsidiaryAgents = _context?.Users?.Where(x => x.Email.Contains(str) || x.AgentName.Contains(str) ||
                                                                   x.Phone.Contains(str) || x.FirstName.Contains(str) ||
                                                                   x.LastName.Contains(str)).AsEnumerable();
                return subsidiaryAgents;
            }
            catch (Exception e)
            {
                throw e;
            }
        }       
        public IEnumerable<User> GetSubsidiaryAgents()
        {
            return null;
        }
    }
}
