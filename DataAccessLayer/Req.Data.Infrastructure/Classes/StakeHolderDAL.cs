using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Classes
{
    public class StakeHolderDAL:IStakeHolderDAL
    {
        private readonly IReqDataSource _dataContext;
        public StakeHolderDAL(IReqDataSource dataContext)
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

        public void Edit(Domain.Classes.StakeHolder stakeHolder)
        {

        }

        public void Delete(Domain.Classes.StakeHolder stakeHolder)
        {

        }
    }
}
