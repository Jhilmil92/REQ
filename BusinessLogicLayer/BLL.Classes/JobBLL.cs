using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class JobBLL:IJobBLL
    {
        private readonly IJobRepository _jobRepository;
        public JobBLL(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public void CreateJob(ReportRequestViewModel requestViewModel)
        {
            var job = new Job 
            { 
                JobDescription = requestViewModel.JobDescription,
                JobType = requestViewModel.JobType,
                CreatedOn = DateTime.Now.Date,
                //ReportedBy = assign stakeholder information
                //Stakeholder id needs to go in here (stakeholder id association).
                JobPriority = requestViewModel.JobType
            };
            _jobRepository.Create(job,taker);
        }

        public IQueryable<Domain.Classes.Job> GetJobs()
        {
            IQueryable<Job> jobs = _jobRepository.GetJobs();
            return jobs;
        }

        public Domain.Classes.Job GetJobById(int jobID)
        {
            var getJobById = _jobRepository.GetJobById(jobID);
            return getJobById;
        }

        public void UpdateJob(Job job)
        {
            _jobRepository.Update(job);
        }

        public void DeleteJob(int jobID)
        {
            _jobRepository.Delete(jobID);
        }
    }
}
