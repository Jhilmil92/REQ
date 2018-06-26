using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Classes
{
    public class LogChangeBLL : ILogChangeBLL
    {
        private readonly ILogChangeRepository _logChangeRepository;
        public LogChangeBLL()
        {
            _logChangeRepository = new LogChangeRepository();
        }
        public IQueryable<Domain.Classes.Req.Domain.Entities.ChangeLog> GetChangeLogsByJobId(int jobId)
        {
            var changeLogs = _logChangeRepository.GetChangeLogsByJobId(jobId);
            return changeLogs;
        }
    }
}
