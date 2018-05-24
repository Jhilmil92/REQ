using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Req.Enums.Req.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileBLL _fileBLL;
        public FileController()
        {
            _fileBLL = new FileBLL();
        }
        // GET: File
        public ActionResult ViewFile(int jobId, string requiredFileName)
        {
            return new DisplayResult
            {
                VirtualPath = "~/Uploads/",
                DisplayFileName = requiredFileName,
                JobId = jobId
            };
        }

        public ActionResult DownloadFile(int jobId , string requiredFileName)
        {
            return new DownloadResult
            {
                VirtualPath = "~/Uploads/",
                DownloadFileName = requiredFileName,
                JobId = jobId
            };
        }
    }
}