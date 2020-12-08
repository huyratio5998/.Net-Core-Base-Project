using ManageExport_V2.Models;
using ManageExport_V2.Models.Entity;
using ManageExport_V2.Repositories.Interfaces;
using ManageExport_V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services
{
    public class UserServices : IUserServices
    {
        private IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IQueryable<User>> GetSubsidiaryAgent(string str)
        {
            try
            {
                return _unitOfWork.Users.GetMulti(x => x.UserType.Equals(UserType.SubsidiaryAgent) && (x.Email.Contains(str) || 
                                                                   x.AgentName.Contains(str) ||
                                                                   x.Phone.Contains(str) || x.FirstName.Contains(str) ||
                                                                   x.LastName.Contains(str)));
            }
            catch(Exception e)
            {
                throw e;
            }            
        }
        public async Task<IQueryable<User>> GetSubsidiaryAgents()
        {
            return await _unitOfWork.Users.GetMulti(x => x.UserType.Equals(UserType.SubsidiaryAgent));
        }

        public  async Task CreateSubsidiaryAgent(User user)
        {
            user.UserType = UserType.SubsidiaryAgent;
            _unitOfWork.Users.Add(user);
            await _unitOfWork.Commit();
        }
        public async Task UpdateSubsidiaryAgent(User user)
        {
           await _unitOfWork.Users.Update(user);
            await _unitOfWork.Commit();
        }
        public async Task DeleteSubsidiaryAgent(int id)
        {
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.Commit();
        }
        public bool CheckLogin(string username, string password)
        {
            return _unitOfWork.Users.CheckLogin(username, password);
        }
    }
}
