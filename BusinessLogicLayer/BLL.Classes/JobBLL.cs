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

        public Job CreateJob(ReportRequestViewModel requestViewModel)
        {
            var stakeHolder = _stakeHolderRepository.GetStakeHolderById(requestViewModel.StakeHolderId);
            var job = new Job 
            { 
                JobTitle = requestViewModel.JobTitle,
                JobDescription = requestViewModel.JobDescription,
                JobCategory = requestViewModel.JobType,
                CreatedOn = DateTime.Now.Date,
                //ReportedBy = stakeHolder,
                ReportedById = stakeHolder.StakeHolderId,
                JobPriority = requestViewModel.JobPriority,
                EstimatedTimeHour = requestViewModel.EstimatedTimeInHours
            };

            if (requestViewModel.JobTakerId != 0)
            {
                job.AssignedToId = requestViewModel.JobTakerId;
                job.Status = JobStatus.Assigned;
            }
            else
            {
                job.Status = JobStatus.Unassigned;
            }

            return _jobRepository.Create(job);
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

        public Job UpdateJob(Job job)
        {
            return _jobRepository.Update(job);
        }

        public Job UpdateJob(UpdateJobViewModel job)
        {
            var currentJob = _jobRepository.GetJobById(job.JobId);
            if (currentJob != null)
            {
                currentJob.ActualTimeTakenHour = job.ActualTimeTakenHrPart;
                currentJob.ActualTimeTakenMinute = job.ActualTimeTakenMinPart;
                currentJob.EstimatedTimeHour = job.EstimatedTimeHrPart;
                currentJob.EstimatedTimeMinute = job.EstimatedTimeMinPart;
                if (job.JobStatus != 0)
                {
                    currentJob.Status = job.JobStatus;
                }
                else
                {
                    currentJob.Status = JobStatus.Assigned;
                }
            }
            return _jobRepository.Update(currentJob);
        }

        public void DeleteJob(int jobID)
        {
            _jobRepository.Delete(jobID);
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

        
        public IQueryable<Job> GetJobsByTakerId(int takerID)
        {
            var jobsByTakerId = _jobRepository.GetJobs().Where(d=>d.AssignedTo.TakerId == takerID);
            return jobsByTakerId;
        }
    }
}
