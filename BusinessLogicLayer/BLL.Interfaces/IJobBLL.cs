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
        void CreateJob(ReportRequestViewModel requestViewModel);
        IQueryable<Job> GetJobs();
        Job GetJobById(int jobID);
        void UpdateJob(Job job);
        void DeleteJob(int jobID);
        void PushJobToQueue(Job job);
        ICollection<Job> GetJobsByStakeHolderId(int stakeHolderId);
    }
}
