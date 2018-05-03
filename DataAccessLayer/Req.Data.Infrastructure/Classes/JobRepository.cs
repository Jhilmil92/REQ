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

    public class JobRepository:IJobRepository
    {
        private readonly IReqDataSource _dataContext;

        public JobRepository()
        {
            _dataContext = new DataContext();
        }
        //public JobRepository(IReqDataSource dataContext)
        //{
        //    _dataContext = dataContext;
        //}

        public void Create(Job job)
        {
            ((DbSet<Job>)_dataContext.Jobs).Add(job);
            _dataContext.Save();
        }

        public void Update(Domain.Classes.Job job)
        {
            this._dataContext.Save();
        }

        public Domain.Classes.Job GetJobById(int jobID)
        {
            var job = _dataContext.Jobs.SingleOrDefault(d=>d.JobId == jobID);
            return job;
        }

        public IQueryable<Job> GetJobs()
        {
            return _dataContext.Jobs;
        }

        public void Delete(int jobID)
        {
            var job = new Job { JobId = jobID };
            var dataContext = _dataContext as DataContext;
            dataContext.Entry(job).State = EntityState.Deleted;
            _dataContext.Save();
        }
    }
}
