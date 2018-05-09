﻿using BusinessLogicLayer;
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

        public ReportRequestController()
        {
            _jobBLL = new JobBLL();
            _stakeHolderBLL = new StakeHolderBLL();
            _takerBLL = new TakerBLL();
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
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    reportRequest.File.InputStream.CopyTo(memoryStream);
                //}

                return RedirectToAction("CreateJob", "Job", new RouteValueDictionary(reportRequest));
            }
            return View(reportRequest);
        }

        public ActionResult ViewReportedRequests()
        {
            var reportedRequests = _jobBLL.GetJobsByStakeHolderId((int)Session[Constants.StakeHolderId]);
            return View(reportedRequests);
        }
    }
}