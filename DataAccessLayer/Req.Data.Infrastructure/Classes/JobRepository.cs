using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Classes
{

    public class JobRepository:IJobRepository
    {
        private readonly IReqDataSource _dataContext;

        public JobRepository(IReqDataSource dataContext)
        {
            _dataContext = dataContext;
        }


        public void CreateJob(Job job, Taker taker)
        {
            Job newJob = job;
            _dataContext.Jobs = newJob as IQueryable<Job>;
            _dataContext.Save();
        }

        public void EditJob(Domain.Classes.Job job)
        {
            int jobID = job.JobId;
            var oldJob = _dataContext.Jobs.SingleOrDefault(d => d.JobId == jobID);

        }

        public Domain.Classes.Job GetJobById(int jobID)
        {
            
        }

        public IQueryable<Job> GetJobs()
        {
            return _dataContext.Jobs;
        }

        public void DeleteJob(int jobID)
        {
        }
    }
}
