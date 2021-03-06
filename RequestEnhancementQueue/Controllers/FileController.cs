﻿using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using Req.Enums.Req.Common.Constants;
using Req.Enums.Req.Common.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileBLL _fileBLL;
        public FileController(IFileBLL _fileBLL)
        {
            this._fileBLL = _fileBLL;
        }
        // GET: File
        public ActionResult ViewFile(int jobId,int userId, string requiredFileName)
        {
            return new DisplayResult
            {
                VirtualPath = "~/Uploads/",
                DisplayFileName = requiredFileName,
                JobId = jobId,
                UserId = userId
            };
        }

        public ActionResult DownloadFile(int jobId, int userId, string requiredFileName)
        {
            return new DownloadResult
            {
                VirtualPath = "~/Uploads/",
                DownloadFileName = requiredFileName,
                JobId = jobId,
                UserId = userId
            };
        }

        public ActionResult UploadFile(HttpPostedFileBase[] files)
        {
            if(files != null)
            {
                string pathName = string.Format("{0}\\{1}", Session.SessionID, (int)Session[Constants.CurrentUserId]);
                //var jobFolderPath = _fileBLL.GetFolderPath(Session.SessionID);
                var jobFolderPath = _fileBLL.GetFolderPath(pathName);
                if (!(Directory.Exists(jobFolderPath)))
                {
                    Directory.CreateDirectory(jobFolderPath);
                }
                else
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(jobFolderPath);
                    foreach(var file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                foreach(var file in files)
                {
                    if(file != null)
                    {
                        var fileName = _fileBLL.GetFileName(file.FileName);
                        file.SaveAs(Path.Combine(string.Format("{0}\\{1}",jobFolderPath,fileName)));
                    }
                }
                return Json (new {success = true , responseText = "File has been uploaded successfully"},JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false , responseText = "There was a problem in uploading the file" },JsonRequestBehavior.AllowGet);
        }
    }
}