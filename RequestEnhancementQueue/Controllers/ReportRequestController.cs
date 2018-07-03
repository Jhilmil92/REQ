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
    public class ReportRequestController : Controller
    {
        private readonly IJobBLL _jobBLL;
        private readonly IStakeHolderBLL _stakeHolderBLL;
        private readonly ITakerBLL _takerBLL;
        private readonly IFileBLL _fileBLL;
        private readonly IClientBLL _clientBLL;

        public ReportRequestController(IJobBLL jobBLL, IStakeHolderBLL stakeHolderBLL, ITakerBLL takerBLL, IFileBLL fileBLL)
        {
            this._jobBLL = jobBLL;
            _stakeHolderBLL = stakeHolderBLL;
            _takerBLL = takerBLL;
            _fileBLL = fileBLL;
            _clientBLL = new ClientBLL();
        }
        //public ReportRequestController(IJobBLL jobBLL)
        //{
        //    _jobBLL = jobBLL;
        //}
        // GET: ReportIssue
        public ActionResult Index()
        {
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById((int)Session[Constants.StakeHolderId]);
            return View(stakeHolder);
        }

        [HttpGet]
        public ActionResult ProcessRequest(int stakeHolderId, string stakeHolderOrganization)
        {
            var model = new ReportRequestViewModel();
            model.StakeHolderId = stakeHolderId;
            model.StakeHolderOrganization = stakeHolderOrganization;
            ViewBag.Takers = _takerBLL.GetTakers().ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessRequest(ReportRequestViewModel reportRequest)
        {

            if (ModelState.IsValid)
            {
                var job = _jobBLL.CreateJob(reportRequest);

                if(reportRequest.Files != null)
                {
                    foreach(var file in reportRequest.Files)
                    {
                        if(file != null)
                        {
                            var fileName = _fileBLL.GetFileName(file.FileName);
                            var jobFolderPath = _fileBLL.GetFolderPath(job.JobId);

                            if (!(Directory.Exists(jobFolderPath)))
                            {
                                     Directory.CreateDirectory(jobFolderPath);
                            }

                            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));                               
                            ViewBag.UploadStatus = reportRequest.Files.Count().ToString() + "Files Uploaded Successfully";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields");
            }
            return View(reportRequest);
        }

        public ActionResult ViewReportedRequests()
        {
           // var reportedRequests = _jobBLL.GetJobsByStakeHolderId((int)Session[Constants.StakeHolderId]);
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById((int)Session[Constants.StakeHolderId]);
            var reportedRequests = _jobBLL.GetJobsByClientId(stakeHolder.ClientId);
            return View(reportedRequests);
        }

        public ActionResult ReportClientRequest()
        {
            var model = new ClientReportRequestViewModel()
            {
                StaffId = (int)Session[Constants.StaffId]
            };
            ViewBag.Clients = _clientBLL.GetClients().ToList();
            ViewBag.Takers = _takerBLL.GetTakers().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportClientRequest(ClientReportRequestViewModel reportRequest)
        {
            if (ModelState.IsValid)
            {
                var job = _jobBLL.CreateJob(reportRequest);

                if (reportRequest.Files != null)
                {
                    foreach (var file in reportRequest.Files)
                    {
                        if (file != null)
                        {
                            var fileName = _fileBLL.GetFileName(file.FileName);
                            var jobFolderPath = _fileBLL.GetFolderPath(job.JobId);

                            if (!(Directory.Exists(jobFolderPath)))
                            {
                                Directory.CreateDirectory(jobFolderPath);
                            }

                            file.SaveAs(Path.Combine(string.Format("{0}\\{1}", jobFolderPath, fileName)));
                            ViewBag.UploadStatus = reportRequest.Files.Count().ToString() + "Files Uploaded Successfully";
                        }
                    }
                }
                return RedirectToAction("StaffInformation","Staff");
            }
            else
            {
                ModelState.AddModelError("", "Fill in all the fields");
            }
            return View(reportRequest);
        }
    }
}