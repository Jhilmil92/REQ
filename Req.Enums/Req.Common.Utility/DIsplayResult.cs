using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Req.Enums.Req.Common.Utility
{
    public class DisplayResult:ActionResult
    {
        public DisplayResult()
        {

        }

        public DisplayResult(string virtualPath)
        {
            VirtualPath = virtualPath;
        }

        public string VirtualPath { get; set; }
        public string DisplayFileName { get; set; }

        public int JobId { get; set; }

        public int UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            string filePath = context.HttpContext.Server.MapPath(string.Format("{0}{1}\\{2}\\{3}", this.VirtualPath, JobId,UserId,this.DisplayFileName));
            string contentType = MimeMapping.GetMimeMapping(filePath);
            context.HttpContext.Response.AddHeader("content-disposition", "inline = true; filename = " + this.DisplayFileName);
            context.HttpContext.Response.AddHeader("content-type", contentType);
            context.HttpContext.Response.TransmitFile(filePath);
        }
    }
}
