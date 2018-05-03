using BusinessLogicLayer;
using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes.Req.Domain.ViewModels;
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
        public JobController()
        {
            _jobBLL = new JobBLL();
            _stakeHolderBLL = new StakeHolderBLL();
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
                _jobBLL.CreateJob(reportRequest);
            }
            var stakeHolder = _stakeHolderBLL.GetStakeHolderById(reportRequest.StakeHolderId);
            return RedirectToAction("index", "ReportRequest",new RouteValueDictionary(stakeHolder));           
        } 

        public ActionResult ViewJobs(int takerId)
        {
            var jobsByTakerId = _jobBLL.GetJobById(takerId);
            return View(jobsByTakerId);
        }

        public ActionResult UpdateJobs(int takerId)
        {
            var jobsByTakerId = _jobBLL.GetJobById(takerId);
            return View(jobsByTakerId);
        }

        public ActionResult EditJob(int jobId)
        {
            var model = new UpdateJobViewModel
            {
                JobId = jobId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(UpdateJobViewModel viewModel)
        {
            _jobBLL.UpdateJob(viewModel);
            var model = _jobBLL.GetJobById(viewModel.JobId);
            return View("TakerInformation",model.AssignedTo);
        }
    }
}