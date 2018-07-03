using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
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
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IClientRepository _clientRepository;

        public JobBLL(IJobRepository _jobRepository, IStakeHolderRepository _stakeHolderRepository, ITakerRepository _takerRepository)
        {
            this._jobRepository = _jobRepository;
            this._stakeHolderRepository = _stakeHolderRepository;
            this._takerRepository = _takerRepository;
            this._userRepository = new UserRepository();
            this._staffRepository = new StaffRepository();
            this._clientRepository = new ClientRepository();
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
            var user = _userRepository.GetAllUsers().SingleOrDefault(d => d.UserType == UserType.StakeHolder && d.TargetUserID == requestViewModel.StakeHolderId);
            var job = new Job 
            { 
                JobTitle = requestViewModel.JobTitle,
                JobDescription = requestViewModel.JobDescription,
                JobCategory = requestViewModel.JobType,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                ReportedBy = stakeHolder.ClientDetails,
                ReportedById = stakeHolder.ClientId,
                CreatedBy = user,
                CreatedById = stakeHolder.StakeHolderId,
                JobPriority = requestViewModel.JobPriority,
                EstimatedTimeHour = requestViewModel.EstimatedTimeInHours,
                ReleaseVersion = requestViewModel.ReleaseVersion
            };

            if (requestViewModel.JobTakerId != 0)
            {
                job.AssignedToId = requestViewModel.JobTakerId;
                job.Status = JobStatus.Assigned;
            }
            else
            {
                job.Status = JobStatus.Queued;
            }

            return _jobRepository.Create(job);
        }


        public Job CreateJob(ClientReportRequestViewModel requestViewModel)
        {
            var staff = _staffRepository.GetStaffById(requestViewModel.StaffId);
            var user = _userRepository.GetAllUsers().SingleOrDefault(d => d.UserType == UserType.Staff && d.TargetUserID == requestViewModel.StaffId);
            var client = _clientRepository.GetClientById(requestViewModel.StakeholderClientId);
            var job = new Job
            {
                JobTitle = requestViewModel.JobTitle,
                JobDescription = requestViewModel.JobDescription,
                JobCategory = requestViewModel.JobType,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                ReportedById = requestViewModel.StakeholderClientId,
                CreatedById = requestViewModel.StaffId,
                JobPriority = requestViewModel.JobPriority,
                EstimatedTimeHour = requestViewModel.EstimatedTimeInHours,
                ReleaseVersion = requestViewModel.ReleaseVersion
            };

            if (requestViewModel.JobTakerId != 0)
            {
                job.AssignedToId = requestViewModel.JobTakerId;
                job.Status = JobStatus.Assigned;
            }
            else
            {
                job.Status = JobStatus.Queued;
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
                currentJob.EstimatedTimeHour = job.EstimatedTimeHrPart;
                currentJob.Comments = job.Comments;
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


        public IEnumerable<Job> GetJobsByTakerId(int takerID)
        {
            var jobsByTakerId = _jobRepository.GetJobs().Where(d=>d.AssignedTo.TakerId == takerID);
            return jobsByTakerId;
        }

        public IEnumerable<Job> GetJobsByClientId(int clientId)
        {
            var jobsByClientId = _jobRepository.GetJobs().Where(d => d.ReportedById == clientId).ToList();
            return jobsByClientId;
        }


        public Job UpdateJob(UpdateStakeHolderJobViewModel viewModel)
        {
            var currentJob = _jobRepository.GetJobById(viewModel.JobId);
            currentJob.EstimatedTimeHour = viewModel.EstimatedTimeInHours;
            if (currentJob.AssignedToId != viewModel.AssignedTakerId)
            {
                currentJob.AssignedToId = viewModel.AssignedTakerId;
            }
            if(!(currentJob.ReleaseVersion.Equals(viewModel.ReleaseVersion)))
            {
                currentJob.ReleaseVersion = viewModel.ReleaseVersion;
            }
            currentJob.JobTitle = viewModel.JobTitle;
            currentJob.JobDescription = viewModel.JobDescription;
            currentJob.JobCategory = viewModel.JobType;
            currentJob.UpdatedOn = DateTime.Now;
            return _jobRepository.Update(currentJob);
        }


    }
}
