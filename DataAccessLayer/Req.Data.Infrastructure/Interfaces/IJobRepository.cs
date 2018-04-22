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
        void CreateJob(Job job,Taker taker);
        void EditJob(Job job);
        Job GetJobById(int jobID);
        IQueryable<Job> GetJobs();
        void DeleteJob(int jobID);
    }
}
