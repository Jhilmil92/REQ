using AutoMapper;
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
        private IMapper _viewModelToModelMapper;
        private readonly ILogChangeBLL _logChangeBLL;
        public JobController(IJobBLL _jobBLL, IStakeHolderBLL _stakeHolderBLL, ITakerBLL _takerBLL, 
            //IJobQueueService _jobQueueService, 
            IFileBLL _fileBLL)
        {
            this._jobBLL = _jobBLL;
            this._stakeHolderBLL = _stakeHolderBLL;
            this._takerBLL = _takerBLL;
            this._fileBLL = _fileBLL;
            this._logChangeBLL = new LogChangeBLL();
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
            var jobs = _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId]));
            return View(jobs);
        }

        public ActionResult UpdateJobs()
        {
            var jobs = _jobBLL.GetJobsByTakerId((int)(Session[Constants.TakerId])).ToList();
            return View(jobs);
        }

        public ActionResult EditJob(int jobId)
        {
            //List<string> folders = new List<string>();
            var job = _jobBLL.GetJobById(jobId);
            //Fetch uploaded files
            var folderPath = _fileBLL.GetFolderPath(job.JobId);
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            
            var model = new UpdateJobViewModel
            {
                JobId = jobId,
                JobTitle = job.JobTitle,
                JobDescription = job.JobDescription,
                JobType = job.JobCategory,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                EstimatedTimeHrPart = job.EstimatedTimeHour,
                ReportedBy = job.ReportedBy.ClientOrganization,
                AssignedTo = job.AssignedTo.TakerName,
                CreatedOn = job.CreatedOn,
                JobStatus = job.Status,
                Comments = job.Comments,
                LastUpdatedOn =  job.UpdatedOn,
                JobPriority = job.JobPriority,
                ReleaseVersion = job.ReleaseVersion,
                FileNames = files
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(UpdateJobViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                //if(viewModel.Files != null)
                //{
                //    foreach(var file in viewModel.Files)
                //    {
                //        if(file != null)
                //        {
                //            var fileName = _fileBLL.GetFileName(file.FileName);
                //            var filePath = _fileBLL.GetFolderPath(viewModel.JobId);
                //            if(!(Directory.Exists(filePath)))
                //            {
                //                Directory.CreateDirectory(filePath);
                //            }

                //            file.SaveAs(Path.Combine("{0}\\{1}",filePath,fileName));
                //            ViewBag.UploadStatus = string.Format("{0} {1}",viewModel.Files.Count().ToString(),"Files Uploaded Successfully");
                //        }
                //    }
                //}
                string pathName = string.Format("{0}\\{1}", Session.SessionID, (int)Session[Constants.CurrentUserId]);
                string actualPathName = string.Format("{0}\\{1}", viewModel.JobId, (int)Session[Constants.CurrentUserId]);
               // var temporaryJobFolderPath = _fileBLL.GetFolderPath(Session.SessionID);
                var temporaryJobFolderPath = _fileBLL.GetFolderPath(pathName);
                if(Directory.Exists(temporaryJobFolderPath))
                {
                    //Check whether the directory is empty or not.
                    if ((Directory.GetFiles(temporaryJobFolderPath) != null) && (Directory.GetFiles(temporaryJobFolderPath).Length != 0))
                    {
                        //Get the actual path where the files have to be moved from the existing temporary session folder.
                        //var actualJobFolderPath = _fileBLL.GetFolderPath(viewModel.JobId);
                        var actualJobFolderPath = _fileBLL.GetFolderPath(actualPathName);
                        string sourceFileName = string.Empty;
                        string destinationFileName = string.Empty;
                        string[] files = Directory.GetFiles(temporaryJobFolderPath);
                        if(!(Directory.Exists(actualJobFolderPath)))
                        {
                            Directory.CreateDirectory(actualJobFolderPath);
                        }
                        //Physically move files from the session folder
                        foreach (var file in files)
                        {
                            sourceFileName = System.IO.Path.GetFileName(file);
                            destinationFileName = System.IO.Path.Combine(actualJobFolderPath,sourceFileName);
                            System.IO.File.Copy(file,destinationFileName,true);
                        }
                        //Delete the session directory after the file transfer is done
                        //foreach(var file in files)
                        //{
                        //    System.IO.File.Delete(file);
                        //}
                        //Directory.Delete(temporaryJobFolderPath);
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
            //var filePath = _fileBLL.GetFolderPath(job.JobId);
            //if (Directory.Exists(filePath))
            //{
            //    files = Directory.GetFiles(filePath);
            //}
            var folderPath = _fileBLL.GetFolderPath(job.JobId);
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            var model = new UpdateStakeHolderJobViewModel
            {
                JobId = jobId,
                EstimatedTimeInHours = job.EstimatedTimeHour,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                AssignedTakerId = job.AssignedToId,
                ReportedBy = job.ReportedBy.ClientOrganization,
                CreatedOn = job.CreatedOn,
                JobTitle = job.JobTitle,
                JobType = job.JobCategory,
                JobStatus = job.Status,
                JobDescription = job.JobDescription,
                ReleaseVersion = job.ReleaseVersion,
                Comments = job.Comments,
                LastUpdatedOn = job.UpdatedOn,
                JobPriority = job.JobPriority,
                FileNames = files
            };

            ViewBag.Takers = _takerBLL.GetTakers();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJobForStakeHolder(UpdateStakeHolderJobViewModel viewModel)
        {
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById((int)(Session[Constants.StakeHolderId]));

            if (ModelState.IsValid)
            {
                //if (viewModel.Files != null) 
                //{
                //    foreach (var file in viewModel.Files)
                //    {
                //        if (file != null)
                //        {
                //            var fileName = _fileBLL.GetFileName(file.FileName);
                //            var jobFolderPath = _fileBLL.GetFolderPath(viewModel.JobId);

                //            if (!(Directory.Exists(jobFolderPath)))
                //            {
                //                Directory.CreateDirectory(jobFolderPath);
                //            }

                //            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));
                //            ViewBag.UploadStatus = string.Format("{0} {1}",viewModel.Files.Count().ToString(),"Files Uploaded Successfully");
                //        }
                //    }

                //}
                string pathName = string.Format("{0}\\{1}", Session.SessionID, (int)Session[Constants.CurrentUserId]);
                string actualPathName = string.Format("{0}\\{1}", viewModel.JobId, (int)Session[Constants.CurrentUserId]);

                var temporaryJobFolderPath = _fileBLL.GetFolderPath(pathName);
                if (Directory.Exists(temporaryJobFolderPath))
                {
                    //Check whether the directory is empty or not.
                    if ((Directory.GetFiles(temporaryJobFolderPath) != null) && (Directory.GetFiles(temporaryJobFolderPath).Length != 0))
                    {

                        var actualJobFolderPath = _fileBLL.GetFolderPath(actualPathName);
                        string sourceFileName = string.Empty;
                        string destinationFileName = string.Empty;
                        string[] files = Directory.GetFiles(temporaryJobFolderPath);
                        if (!(Directory.Exists(actualJobFolderPath)))
                        {
                            Directory.CreateDirectory(actualJobFolderPath);
                        }
                        //Physically move files from the session folder
                        foreach (var file in files)
                        {
                            sourceFileName = System.IO.Path.GetFileName(file);
                            destinationFileName = System.IO.Path.Combine(actualJobFolderPath, sourceFileName);
                            System.IO.File.Copy(file, destinationFileName, true);
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

        public ActionResult EditJobForClient(int jobId)
        {
            var job = _jobBLL.GetJobById(jobId);
            //var clientId = (int)(Session[Constants.]);
            //var stakeHolder = _stakeHolderBLL.GetStakeHolderById(stakeHolderId);
            //string[] files = null;
            //var filePath = _fileBLL.GetFolderPath(job.JobId);
            //if (Directory.Exists(filePath))
            //{
            //    files = Directory.GetFiles(filePath);
            //}

            //Fetch uploaded files
            var folderPath = _fileBLL.GetFolderPath(job.JobId);
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            var model = new UpdateClientJobViewModel
            {
                JobId = jobId,
                EstimatedTimeInHours = job.EstimatedTimeHour,
                ActualTimeTakenHrPart = job.ActualTimeTakenHour,
                AssignedTakerId = job.AssignedToId,
                ReportedBy = job.ReportedBy.ClientOrganization,
                CreatedOn = job.CreatedOn,
                JobTitle = job.JobTitle,
                JobType = job.JobCategory,
                JobStatus = job.Status,
                JobDescription = job.JobDescription,
                ReleaseVersion = job.ReleaseVersion,
                Comments = job.Comments,
                LastUpdatedOn = job.UpdatedOn,
                JobPriority = job.JobPriority,
                FileNames = files
            };

            ViewBag.Takers = _takerBLL.GetTakers();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJobForClient(UpdateStakeHolderJobViewModel viewModel)
        {
            //var stakeHolder = _stakeHolderBLL.GetStakeHolderById((int)(Session[Constants.StakeHolderId]));

            if (ModelState.IsValid)
            {
                //if (viewModel.Files != null)
                //{
                //    foreach (var file in viewModel.Files)
                //    {
                //        if (file != null)
                //        {
                //            var fileName = _fileBLL.GetFileName(file.FileName);
                //            var jobFolderPath = _fileBLL.GetFolderPath(viewModel.JobId);

                //            if (!(Directory.Exists(jobFolderPath)))
                //            {
                //                Directory.CreateDirectory(jobFolderPath);
                //            }

                //            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));
                //            ViewBag.UploadStatus = string.Format("{0} {1}", viewModel.Files.Count().ToString(), "Files Uploaded Successfully");
                //        }
                //    }

                //}

                string pathName = string.Format("{0}\\{1}", Session.SessionID, (int)Session[Constants.CurrentUserId]);
                string actualPathName = string.Format("{0}\\{1}", viewModel.JobId, (int)Session[Constants.CurrentUserId]);

                var temporaryJobFolderPath = _fileBLL.GetFolderPath(pathName);
                if (Directory.Exists(temporaryJobFolderPath))
                {
                    //Check whether the directory is empty or not.
                    if ((Directory.GetFiles(temporaryJobFolderPath) != null) && (Directory.GetFiles(temporaryJobFolderPath).Length != 0))
                    {

                        var actualJobFolderPath = _fileBLL.GetFolderPath(actualPathName);
                        string sourceFileName = string.Empty;
                        string destinationFileName = string.Empty;
                        string[] files = Directory.GetFiles(temporaryJobFolderPath);
                        if (!(Directory.Exists(actualJobFolderPath)))
                        {
                            Directory.CreateDirectory(actualJobFolderPath);
                        }
                        //Physically move files from the session folder
                        foreach (var file in files)
                        {
                            sourceFileName = System.IO.Path.GetFileName(file);
                            destinationFileName = System.IO.Path.Combine(actualJobFolderPath, sourceFileName);
                            System.IO.File.Copy(file, destinationFileName, true);
                        }
                    }

                }
                var job = _jobBLL.UpdateJob(viewModel);

                return RedirectToAction("ViewClientReportedRequests", "ReportRequest");
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields");
            }

            return View(viewModel);
        }

        public ActionResult ChangeLog(int jobId)
        {
            var changeLogs = _logChangeBLL.GetChangeLogsByJobId(jobId);
            return PartialView("_ChangeTrackingLogViewPartial", changeLogs);
        }
  }
}