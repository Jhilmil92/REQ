﻿using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RequestEnhancementQueue.Controllers
{
    public class ReportRequestController : Controller
    {
        public ReportRequestController()
        {

        }
        // GET: ReportIssue
        public ActionResult Index(StakeHolder stakeHolder)
        {
            return View(stakeHolder);
        }

        [HttpGet]
        public ActionResult ProcessRequest(int stakeHolderId)
        {
            var model = new ReportRequestViewModel();
            model.StakeHolderId = stakeHolderId;
            return View(model);
        }

        [HttpPost]
        public ActionResult ProcessRequest(ReportRequestViewModel reportRequest)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateJob", "JobController", new RouteValueDictionary(reportRequest));
            }
            return View(reportRequest);
        }
    }
}