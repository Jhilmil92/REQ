using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
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
        public StakeHolderBLL(IStakeHolderRepository stakeHolderRepository)
        {
            _stakeHolderRepository = stakeHolderRepository;
        }

        public void CreateStakeHolder(StakeHolder stakeHolder)
        {
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
    }
}
