using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class TakerController : Controller
    {
        private readonly ITakerBLL _takerBLL;
        public TakerController()
        {
            _takerBLL = new TakerBLL();
        }
        // GET: Taker
        public ActionResult TakerInformation()
        {
            var taker = _takerBLL.GetTakerById((int)Session[Constants.TakerId]);
            return View(taker);
        }

    }
}