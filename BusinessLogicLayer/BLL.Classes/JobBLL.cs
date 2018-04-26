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
        private readonly IStakeHolderRepository _stakeHolderRepository;
        private readonly ITakerRepository _takerRepository;
        public JobBLL(IJobRepository jobRepository, IStakeHolderRepository stakeHolderRepository,ITakerRepository takerRepository)
        {
            _jobRepository = jobRepository;
            _stakeHolderRepository = stakeHolderRepository;
            _takerRepository = takerRepository;
        }

        public void CreateJob(ReportRequestViewModel requestViewModel)
        {
            var stakeHolder = _stakeHolderRepository.GetStakeHolderById(requestViewModel.StakeHolderId);
            var job = new Job 
            { 
                JobDescription = requestViewModel.JobDescription,
                JobType = requestViewModel.JobType,
                CreatedOn = DateTime.Now.Date,
                ReportedBy = stakeHolder,
                JobPriority = requestViewModel.JobPriority
            };
            //Push job to the priority Queue
            //_jobRepository.Create(job,taker);
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

        public void PushJobToQueue(Job job)
        {
            throw new NotImplementedException();
        }


        public ICollection<Job> GetJobsByStakeHolderId(int stakeHolderId)
        {
            var jobs = _jobRepository.GetJobs();
            ICollection<Job> jobsByStakeHolderId = null;
            foreach(var job in jobs)
            {
                if(job.ReportedBy.StakeHolderId == stakeHolderId)
                {
                    jobsByStakeHolderId.Add(job);
                }
            }
            return jobsByStakeHolderId;
        }
    }
}
