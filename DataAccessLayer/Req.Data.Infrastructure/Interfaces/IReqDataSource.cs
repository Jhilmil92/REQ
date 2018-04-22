using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Interfaces
{
    public interface IReqDataSource
    {
        IQueryable<Job> Jobs { get; set; }
        IQueryable<JobType> JobTypes { get; }
        IQueryable<Priority> Priorities { get; }
        IQueryable<StakeHolder> StakeHolders { get; set; }
        IQueryable<Taker> Takers { get; set; }
        void Save();

    }
}
