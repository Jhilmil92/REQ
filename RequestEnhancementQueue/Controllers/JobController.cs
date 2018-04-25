using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobBLL _jobBLL;
        public JobController()
        {

        }
        public JobController(IJobBLL jobBLL)
        {
            _jobBLL = jobBLL;
        }
        // GET: Job
        //public ActionResult CreateJob(ReportRequestViewModel reportRequest)
        //{
        //    //_jobBLL.CreateJob(reportRequest);
        //}   
    }
}