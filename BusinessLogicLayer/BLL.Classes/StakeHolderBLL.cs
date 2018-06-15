using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Classes
{
    public class StakeHolderBLL:IStakeHolderBLL
    {
        private readonly IStakeHolderRepository _stakeHolderRepository;
        private readonly IUserRepository _userRepository;

        public StakeHolderBLL(IUserRepository userRepository, IStakeHolderRepository _stakeHolderRepository)
        {
            this._stakeHolderRepository = _stakeHolderRepository;
            this._userRepository = userRepository;
        }

        //public StakeHolderBLL(IStakeHolderRepository stakeHolderRepository)
        //{
        //    _stakeHolderRepository = stakeHolderRepository;
        //}

        public void CreateStakeHolder(StakeHolderRegistrationViewModel registrationViewModel)
        {
            var stakeHolder = new StakeHolder
            {
                StakeHolderOrganization = registrationViewModel.StakeHolderOrganization,
                
            };

          var createdStakeHolder =  _stakeHolderRepository.Create(stakeHolder);

          _userRepository.CreateUser(registrationViewModel.StakeHolderUserName, registrationViewModel.StakeHolderPassword, UserType.StakeHolder, createdStakeHolder.StakeHolderId);
        }

        public IQueryable<StakeHolder> GetStakeHolders()
        {
            return _stakeHolderRepository.GetStakeHolders();
        }

        public StakeHolder GetStakeHolderById(int stakeHolderId)
        {
            var stakeHolder = _stakeHolderRepository.GetStakeHolderById(stakeHolderId);
            return stakeHolder;
        }

        public void UpdateStakeHolder(StakeHolder stakeHolder)
        {
            _stakeHolderRepository.Update(stakeHolder);
        }

        public void DeleteStakeHolder(int stakeHolderID)
        {
            _stakeHolderRepository.Delete(stakeHolderID);
        }

    }
}
