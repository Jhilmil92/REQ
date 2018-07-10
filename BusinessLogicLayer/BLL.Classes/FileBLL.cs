using BusinessLogicLayer.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.BLL.Classes
{
    public class FileBLL:IFileBLL
    {
        public string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        public string GetFolderPath(int jobId)
        {
            return Path.Combine(string.Format("{0}{1}", HttpContext.Current.Server.MapPath("~/Uploads/"), jobId));
        }

        public string GetFolderPath(string sessionId)
        {
            return Path.Combine(string.Format("{0}{1}", HttpContext.Current.Server.MapPath("~/Uploads/"), sessionId));            
        }
    }
}
