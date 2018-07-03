using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffBLL _staffBLL;
        public StaffController()
        {
            _staffBLL = new StaffBLL();
        }
    
        public ActionResult StaffInformation()
        {
            var staff = _staffBLL.GetStaffById((int)Session[Constants.StaffId]);
            return View(staff);
        }

    }
}