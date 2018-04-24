using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Classes
{
    public class StakeHolderRepository:IStakeHolderRepository
    {
        private readonly IReqDataSource _dataContext;
        public StakeHolderRepository(IReqDataSource dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(Domain.Classes.StakeHolder stakeHolder)
        {
            var currentStakeHolder = new StakeHolder
            {
                StakeHolderId = stakeHolder.StakeHolderId,
                StakeHolderOrganization = stakeHolder.StakeHolderOrganization
            };
        }

        public IQueryable<Domain.Classes.StakeHolder> GetStakeHolders()
        {
            return _dataContext.StakeHolders;
        }

        public StakeHolder GetStakeHolderById(int stakeHolderID)
        {
            var stakeHolder = _dataContext.StakeHolders.SingleOrDefault(d=>d.StakeHolderId == stakeHolderID);
            return stakeHolder;
        }

        public void Update(Domain.Classes.StakeHolder stakeHolder)
        {
            this._dataContext.Save();
        }

        public void Delete(int stakeHolderId)
        {
            var stakeHolder = new StakeHolder
            {
                StakeHolderId = stakeHolderId
            };
            var dataContext = _dataContext as DataContext;
            dataContext.Entry(stakeHolder).State = EntityState.Deleted;
            _dataContext.Save();
        }

    }
}
