using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class ReportRequestController : Controller
    {
        // GET: ReportIssue
        public ActionResult ProcessRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessRequest(ReportRequestViewModel reportRequest)
        {
            
        }
    }
}