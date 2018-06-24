using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Services.Classes;
using DataAccessLayer.Req.Data.Services.Interfaces;
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
        private readonly ILogChangeService _logChangeService;

        public JobRepository()
        {
            _dataContext = new DataContext();
            _logChangeService = new LogChangeService();
        }
        //public JobRepository(IReqDataSource dataContext)
        //{
        //    _dataContext = dataContext;
        //}

        public Job Create(Job job)
        {
            ((DbSet<Job>)_dataContext.Jobs).Add(job);
            this._dataContext.Save();
            //Task.Run(() => {
            //    _logChangeService.LogAddChanges(job);
            //}); 
            return job;
        }

        public Job Update(Domain.Classes.Job job)
        {
            var dataContext = _dataContext as DataContext;
            var entity = dataContext.Jobs.Find(job.JobId);
            dataContext.Entry(entity).CurrentValues.SetValues(job);
            _logChangeService.LogChanges((DbContext)_dataContext, job);
            this._dataContext.Save();
            return entity;
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
