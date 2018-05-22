using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IFileBLL
    {
        string GetFileName(string fileName);
        string GetFolderPath(int jobId);
    }
}
