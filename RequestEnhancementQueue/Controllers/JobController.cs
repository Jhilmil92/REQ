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
        //private readonly IJobQueueService _jobQueueService;
        private readonly IFileBLL _fileBLL;
        public JobController(IJobBLL _jobBLL, IStakeHolderBLL _stakeHolderBLL, ITakerBLL _takerBLL, 
            //IJobQueueService _jobQueueService, 
            IFileBLL _fileBLL)
        {
            this._jobBLL = _jobBLL;
            this._stakeHolderBLL = _stakeHolderBLL;
            this._takerBLL = _takerBLL;
            this._fileBLL = _fileBLL;
            //this._jobQueueService = _jobQueueService;
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
            string[] files = null;
            var job = _jobBLL.GetJobById(jobId);
            //Fetch uploaded files
            var folderPath = _fileBLL.GetFolderPath(job.JobId);
            if (Directory.Exists(folderPath))
            {
                files = Directory.GetFiles(folderPath);
            }

            var model = new UpdateJobViewModel
            {
                JobId = jobId,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                EstimatedTimeHrPart = job.EstimatedTimeHour,
                JobStatus = job.Status,
                Comments = job.Comments,
                FileNames = files
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(UpdateJobViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                if(viewModel.Files != null)
                {
                    foreach(var file in viewModel.Files)
                    {
                        if(file != null)
                        {
                            var fileName = _fileBLL.GetFileName(file.FileName);
                            var filePath = _fileBLL.GetFolderPath(viewModel.JobId);
                            if(!(Directory.Exists(filePath)))
                            {
                                Directory.CreateDirectory(filePath);
                            }

                            file.SaveAs(Path.Combine("{0}\\{1}",filePath,fileName));
                            ViewBag.UploadStatus = string.Format("{0} {1}",viewModel.Files.Count().ToString(),"Files Uploaded Successfully");
                        }
                    }
                }
                viewModel.LastUpdatedOn = DateTime.Now;
                _jobBLL.UpdateJob(viewModel);
                return RedirectToAction("ViewJobs");
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields");
            }
            return View(viewModel);
        }

        //public ActionResult AssignJob()
        //{
        //    var priorityJob = (Job)_jobQueueService.PriorityQue.Dequeue();
        //    var takerId = (int)(Session[Constants.TakerId]);
        //    priorityJob.AssignedToId = takerId;
        //    priorityJob.Status = Req.Enums.JobStatus.Assigned;
        //    _jobBLL.UpdateJob(priorityJob);
        //    var jobs = _jobBLL.GetJobsByTakerId(takerId).ToList();
        //    return View("ViewJobs", jobs);
        //}

        public ActionResult EditJobForStakeHolder(int jobId)
        {
            var job = _jobBLL.GetJobById(jobId);
            var stakeHolderId = (int)(Session[Constants.StakeHolderId]);
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById(stakeHolderId);
            string[] files = null;
            var filePath = _fileBLL.GetFolderPath(job.JobId);
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
                            var fileName = _fileBLL.GetFileName(file.FileName);
                            var jobFolderPath = _fileBLL.GetFolderPath(viewModel.JobId);

                            if (!(Directory.Exists(jobFolderPath)))
                            {
                                Directory.CreateDirectory(jobFolderPath);
                            }

                            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));
                            ViewBag.UploadStatus = string.Format("{0} {1}",viewModel.Files.Count().ToString(),"Files Uploaded Successfully");
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