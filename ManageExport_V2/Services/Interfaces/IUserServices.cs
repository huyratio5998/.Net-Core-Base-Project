using ManageExport_V2.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManageExport_V2.Services.Interfaces
{
    public interface IUserServices
    {
        IEnumerable<User> GetSubsidiaryAgents();
        IEnumerable<User> GetSubsidiaryAgent(string str);
    }
}
