using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Interfaces
{
    public interface IJobRepository
    {
        Job Create(Job job);
        Job Update(Job job);
        Job GetJobById(int jobID);
        IQueryable<Job> GetJobs();
        void Delete(int jobID);
    }
}
