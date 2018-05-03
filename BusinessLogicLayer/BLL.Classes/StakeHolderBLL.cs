using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
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

        public StakeHolderBLL()
        {
            _stakeHolderRepository = new StakeHolderRepository();
        }

        //public StakeHolderBLL(IStakeHolderRepository stakeHolderRepository)
        //{
        //    _stakeHolderRepository = stakeHolderRepository;
        //}

        public void CreateStakeHolder(RegistrationViewModel registrationViewModel)
        {
            var stakeHolder = new StakeHolder
            {
                StakeHolderOrganization = registrationViewModel.StakeHolderOrganization,
                UserName = registrationViewModel.StakeHolderUserName,
                Password = registrationViewModel.StakeHolderPassword
            };
            _stakeHolderRepository.Create(stakeHolder);
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

        public StakeHolder ValidateLoginCredentials(LoginViewModel loginViewModel)
        {
            //fetch login credentials from the database and check it against the view model.
            //Return bool status accordingly.
            bool isValid = false;
            StakeHolder stakeHolder = null;
            var stakeHolders = _stakeHolderRepository.GetStakeHolders().ToArray();
            stakeHolder = stakeHolders.SingleOrDefault((d => (d.UserName == loginViewModel.UserName && d.Password == loginViewModel.Password)));
            if(stakeHolder != null)
            {
                    isValid = true;
            }
            if (isValid)
            {
                return stakeHolder;
            }
            else
            {
                return null;
            }
        }
    }
}
