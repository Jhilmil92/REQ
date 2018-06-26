using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface ILogChangeBLL
    {
        IQueryable<ChangeLog> GetChangeLogsByJobId(int jobId);
    }
}
