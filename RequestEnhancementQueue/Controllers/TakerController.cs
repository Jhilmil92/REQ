using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class TakerController : Controller
    {
        // GET: Taker
        public ActionResult TakerInformation(Taker taker)
        {
            return View(taker);
        }

    }
}