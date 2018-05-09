using BusinessLogicLayer;
using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RequestEnhancementQueue.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobBLL _jobBLL;
        private readonly IStakeHolderBLL _stakeHolderBLL;
        private readonly ITakerBLL _takerBLL;
        private readonly IJobQueueService _jobQueueService;
        public JobController()
        {
            _jobBLL = new JobBLL();
            _stakeHolderBLL = new StakeHolderBLL();
            _takerBLL = new TakerBLL();
            _jobQueueService = JobQueueService.GetInstance();
        }
        //public JobController(IJobBLL jobBLL)
        //{
        //    _jobBLL = jobBLL;
        //}
        // GET: Job

        public ActionResult CreateJob(ReportRequestViewModel reportRequest)
        {
            if (ModelState.IsValid)
            {
                var job = _jobBLL.CreateJob(reportRequest);
                //var queueInstance = JobQueueService.GetInstance();
                //queueInstance.Enqueue(job);
            }
            else
            {
                ModelState.AddModelError("","Fill in all the fields");
            }
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById(reportRequest.StakeHolderId);
            return RedirectToAction("index", "ReportRequest",new RouteValueDictionary(stakeHolder));           
        } 

        public ActionResult ViewJobs()
        {
            var jobs= _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId])).ToList();
            return View(jobs);
        }

        public ActionResult UpdateJobs()
        {
            var jobs= _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId])).ToList();
            return View(jobs);
        }

        public ActionResult EditJob(int jobId)
        {
            var job = _jobBLL.GetJobById(jobId);
            var model = new UpdateJobViewModel
            {
                JobId = jobId,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                ActualTimeTakenMinPart = job.ActualTimeTakenMinute,
                EstimatedTimeHrPart = job.EstimatedTimeHour,
                EstimatedTimeMinPart = job.EstimatedTimeMinute,
                JobStatus = job.Status
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(UpdateJobViewModel viewModel)
        {
            _jobBLL.UpdateJob(viewModel);
            return RedirectToAction("TakerInformation","Taker");
        }

        public ActionResult AssignJob()
        {
            var queueInstance = JobQueueService.GetInstance();
            var priorityJob = (Job)queueInstance.PriorityQue.Dequeue();
            var takerId = (int)(Session[Constants.TakerId]);
            priorityJob.AssignedToId = takerId;
            priorityJob.Status = Req.Enums.JobStatus.Assigned;
            _jobBLL.UpdateJob(priorityJob);
            var jobs = _jobBLL.GetJobsByTakerId(takerId).ToList();
            return View("ViewJobs",jobs);
        }
    }
}