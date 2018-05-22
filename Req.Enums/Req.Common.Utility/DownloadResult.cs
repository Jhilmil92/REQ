using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Req.Enums.Req.Common.Utility
{
    public class DownloadResult : ActionResult
    {
        public DownloadResult()
        {

        }

        public DownloadResult(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        public string VirtualPath { get; set; }
        public string DownloadFileName { get; set; }

        public int JobId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!(string.IsNullOrEmpty(DownloadFileName)))
            {
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename = " + this.DownloadFileName);
            }
            string filePath = context.HttpContext.Server.MapPath(string.Format("{0}{1}", this.VirtualPath, JobId));
            context.HttpContext.Response.TransmitFile(string.Format("{0}\\{1}",filePath,this.DownloadFileName));
        }
    }
}
