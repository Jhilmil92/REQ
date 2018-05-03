using BusinessLogicLayer;
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
    public class ReportRequestController : Controller
    {
        private readonly IJobBLL _jobBLL;

        public ReportRequestController()
        {
            _jobBLL = new JobBLL();
        }
        //public ReportRequestController(IJobBLL jobBLL)
        //{
        //    _jobBLL = jobBLL;
        //}
        // GET: ReportIssue
        public ActionResult Index()
        {
            return View((StakeHolder)Session["StakeHolder"]);
        }

        [HttpGet]
        public ActionResult ProcessRequest(int stakeHolderId, string stakeHolderOrganization)
        {
            var model = new ReportRequestViewModel();
            model.StakeHolderId = stakeHolderId;
            model.StakeHolderOrganization = stakeHolderOrganization;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessRequest(ReportRequestViewModel reportRequest)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateJob", "Job", new RouteValueDictionary(reportRequest));
            }
            return View(reportRequest);
        }

        public ActionResult ViewReportedRequests()
        {
            var reportedRequests = _jobBLL.GetJobsByStakeHolderId((int)((StakeHolder)Session["StakeHolder"]).StakeHolderId);
            return View(reportedRequests);
        }
    }
}