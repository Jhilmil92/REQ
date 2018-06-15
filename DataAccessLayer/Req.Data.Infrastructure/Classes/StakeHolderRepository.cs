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

        public StakeHolderRepository()
        {
            _dataContext = new DataContext();
        }
        //public StakeHolderRepository(IReqDataSource dataContext)
        //{
        //    _dataContext = dataContext;
        //}

        public StakeHolder Create(StakeHolder stakeHolder)
        {
            ((DbSet<StakeHolder>)(_dataContext.StakeHolders)).Add(stakeHolder);
            _dataContext.Save();
            return stakeHolder;
        }

        public IQueryable<StakeHolder> GetStakeHolders()
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
