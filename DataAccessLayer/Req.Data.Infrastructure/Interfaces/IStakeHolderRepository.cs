using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Interfaces
{
    public interface IStakeHolderRepository
    {
        void Create(StakeHolder stakeHolder);
        IQueryable<StakeHolder> GetStakeHolders();
        StakeHolder GetStakeHolderById(int stakeHolderID);
        void Update(StakeHolder stakeHolder);
        void Delete (int stakeHolderId);
    }
}
