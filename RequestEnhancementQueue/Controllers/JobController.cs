using BusinessLogicLayer;
using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
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
        private readonly ITakerBLL _takerBLL;
        public JobController()
        {
            _jobBLL = new JobBLL();
            _stakeHolderBLL = new StakeHolderBLL();
            _takerBLL = new TakerBLL();
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
                var queueInstance = JobQueueService.GetInstance();
                queueInstance.Enqueue(job);
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
            var jobsByTakerId = _jobBLL.GetJobsByTakerId(((Taker)Session["Taker"]).TakerId);
            return View(jobsByTakerId);
        }

        public ActionResult UpdateJobs()
        {
            var jobsByTakerId = _jobBLL.GetJobById(((Taker)Session["Taker"]).TakerId);
            return View(jobsByTakerId);
        }

        public ActionResult EditJob()
        {
            var model = new UpdateJobViewModel
            {
                JobId = ((Job)Session["Taker"]).JobId
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

        public ActionResult AssignJob()
        {
            var queueInstance = JobQueueService.GetInstance();
            //Have to handle the scenario when the queue is empty and there are no jobs to be assigned.
            var priorityJob = (Job)queueInstance.PriorityQue.Peek();
            var taker = (Taker)Session["Taker"];
            priorityJob.AssignedTo = taker;
            priorityJob.Status = Req.Enums.JobStatus.Approved;
            _jobBLL.UpdateJob(priorityJob);
            taker.Jobs.Add(priorityJob);
            _takerBLL.UpdateTaker(taker);
            var jobs = _jobBLL.GetJobsByTakerId(taker.TakerId);
            return View("ViewJobs",jobs);
        }
    }
}