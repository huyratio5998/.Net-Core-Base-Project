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
        #region SubsidiaryAgent
        public Task<User> GetSubsidiaryAgentById(int id)
        {
            try
            {
                Task<User> user = _unitOfWork.Users.GetSingleById(id);
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
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
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<IQueryable<User>> GetSubsidiaryAgents()
        {
            return await _unitOfWork.Users.GetMulti(x => x.UserType.Equals(UserType.SubsidiaryAgent) && x.IsActive);
        }

        public async Task CreateSubsidiaryAgent(User user)
        {
            //Insert record     
            int NewestID = _unitOfWork.Users.getNewId().Equals(null) ? 0 : (int)_unitOfWork.Users.getNewId();
            user.AgentCode = $"SA_{NewestID + 1}";
            user.CreatedDate = user.ModifiedDate = DateTime.UtcNow;
            user.ExpireContractDate = user.ExpireContractDate.ToUniversalTime();
            user.UserType = UserType.SubsidiaryAgent;
            user.IsActive = true;
            _unitOfWork.Users.Add(user);
            await _unitOfWork.Commit();

        }
        public async Task UpdateSubsidiaryAgent(User user)
        {
            user.ModifiedDate = DateTime.UtcNow;
            user.ExpireContractDate = user.ExpireContractDate.ToUniversalTime();
            await _unitOfWork.Users.Update(user);
            await _unitOfWork.Commit();
        }
        public async Task DeleteSubsidiaryAgent(int id)
        {
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.Commit();

        }
        public async Task DeleteVirtual(int id)
        {
            var entity = await _unitOfWork.Users.GetSingleById(id);
            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;
            await _unitOfWork.Users.Update(entity);
            await _unitOfWork.Commit();
        }
        #endregion

        public bool CheckLogin(string username, string password)
        {
            return _unitOfWork.Users.CheckLogin(username, password);
        }

        public Task<IQueryable<User>> GetSupply()
        {
            try
            {
                var users= _unitOfWork.Users.GetMulti(x => x.UserType.Equals(UserType.Supply) && x.IsActive);
                return users;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
