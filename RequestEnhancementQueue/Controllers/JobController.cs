using BusinessLogicLayer;
using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.IO;
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
            //_jobQueueService = JobQueueService.GetInstance();
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
                ModelState.AddModelError("", "Fill in all the fields");
            }
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById(reportRequest.StakeHolderId);
            return RedirectToAction("index", "ReportRequest", new RouteValueDictionary(stakeHolder));
        }

        public ActionResult ViewJobs()
        {
            var jobs = _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId])).ToList();
            return View(jobs);
        }

        public ActionResult UpdateJobs()
        {
            var jobs = _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId])).ToList();
            return View(jobs);
        }

        public ActionResult EditJob(int jobId)
        {
            var job = _jobBLL.GetJobById(jobId);
            var model = new UpdateJobViewModel
            {
                JobId = jobId,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                EstimatedTimeHrPart = job.EstimatedTimeHour,
                JobStatus = job.Status
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(UpdateJobViewModel viewModel)
        {
            _jobBLL.UpdateJob(viewModel);
            return RedirectToAction("TakerInformation", "Taker");
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
            return View("ViewJobs", jobs);
        }

        public ActionResult EditJobForStakeHolder(int jobId)
        {
            var job = _jobBLL.GetJobById(jobId);
            var stakeHolderId = (int)(Session[Constants.StakeHolderId]);
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById(stakeHolderId);
            string[] files = null;
            var filePath = Path.Combine(string.Format("{0}{1}\\{2}", Server.MapPath("~/Uploads/"), stakeHolder.StakeHolderOrganization, job.JobTitle));
            if (Directory.Exists(filePath))
            {
                files = Directory.GetFiles(filePath);
            }

            var model = new UpdateStakeHolderJobViewModel
            {
                JobId = jobId,
                EstimatedTimeInHours = job.EstimatedTimeHour,
                AssignedTakerId = job.AssignedToId,
                JobTitle = job.JobTitle,
                JobType = job.JobCategory,
                JobDescription = job.JobDescription,
                ReleaseVersion = job.ReleaseVersion,
                FileNames = files
            };

            ViewBag.Takers = _takerBLL.GetTakers().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJobForStakeHolder(UpdateStakeHolderJobViewModel viewModel)
        {
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById((int)(Session[Constants.StakeHolderId]));

            if (ModelState.IsValid)
            {
                if (viewModel.Files != null)
                {
                    foreach (var file in viewModel.Files)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var serverSavePath = Path.Combine(string.Format("{0}{1}", Server.MapPath("~/Uploads/"), stakeHolder.StakeHolderOrganization));
                            var jobFolderPath = Path.Combine(string.Format("{0}\\{1}", serverSavePath, viewModel.JobTitle));

                            if (!(Directory.Exists(jobFolderPath)))
                            {
                                Directory.CreateDirectory(jobFolderPath);
                            }

                            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));
                            ViewBag.UploadStatus = viewModel.Files.Count().ToString() + "Files Uploaded Successfully";
                        }
                    }
                }
                var job = _jobBLL.UpdateJob(viewModel);
                
                return RedirectToAction("ViewReportedRequests", "ReportRequest");
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields");
            }

            return View(viewModel);
    }
  }
}