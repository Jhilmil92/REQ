using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IJobBLL
    {
        Job CreateJob(ReportRequestViewModel requestViewModel);
        Job CreateJob(ClientReportRequestViewModel requestViewModel);
        IQueryable<Job> GetJobs();
        Job GetJobById(int jobID);
        IEnumerable<Job> GetJobsByTakerId(int takerID);
        Job UpdateJob(Job job);
        Job UpdateJob(UpdateJobViewModel job);
        Job UpdateJob(UpdateStakeHolderJobViewModel viewModel);
        void DeleteJob(int jobID);
        ICollection<Job> GetJobsByStakeHolderId(int stakeHolderId);
        IEnumerable<Job> GetJobsByClientId(int clientId);
    }
}
