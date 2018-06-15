using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Interfaces
{
    public interface IReqDataSource
    {
        IQueryable<Job> Jobs { get; }
        IQueryable<StakeHolder> StakeHolders { get;}
        IQueryable<Taker> Takers { get; }
        IQueryable<User> Users { get; }
        void Save();

    }
}
