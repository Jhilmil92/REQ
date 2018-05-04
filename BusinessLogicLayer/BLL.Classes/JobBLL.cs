using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using Req.Enums;
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

        public JobBLL()
        {
            _jobRepository = new JobRepository();
            _stakeHolderRepository = new StakeHolderRepository();
            _takerRepository = new TakerRepository();
        }

        //public JobBLL(IJobRepository jobRepository, IStakeHolderRepository stakeHolderRepository,ITakerRepository takerRepository)
        //{
        //    _jobRepository = jobRepository;
        //    _stakeHolderRepository = stakeHolderRepository;
        //    _takerRepository = takerRepository;
        //}

        public void CreateJob(ReportRequestViewModel requestViewModel)
        {
            var stakeHolder = _stakeHolderRepository.GetStakeHolderById(requestViewModel.StakeHolderId);
            var job = new Job 
            { 
                JobDescription = requestViewModel.JobDescription,
                JobType = requestViewModel.JobType,
                CreatedOn = DateTime.Now.Date,
                //ReportedBy = stakeHolder,
                ReportedById = stakeHolder.StakeHolderId,
                JobPriority = requestViewModel.JobPriority
            };

            _jobRepository.Create(job);
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

        public void UpdateJob(UpdateJobViewModel job)
        {
            var currentJob = _jobRepository.GetJobById(job.JobId);
            if (currentJob != null)
            {
                currentJob.EstimatedTime = job.EstimatedTime ;
                currentJob.ActualTimeTaken = job.ActualTimeTaken ;
                currentJob.Status = job.JobStatus;
            }
            _jobRepository.Update(currentJob);
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
            var jobs = _jobRepository.GetJobs().Where(d=>d.ReportedById == stakeHolderId).ToList();
            ICollection<Job> jobsByStakeHolderId = new List<Job>();
            foreach(var job in jobs)
            {
                if(job.ReportedById == stakeHolderId)
                {
                    jobsByStakeHolderId.Add(job);
                }
            }
            return jobsByStakeHolderId;
        }
    }
}
