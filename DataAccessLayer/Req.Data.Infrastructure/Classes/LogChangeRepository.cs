using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Classes
{
    public class LogChangeRepository:ILogChangeRepository
    {
        private readonly IChangeLogDataSource _changeLogDataSource;
        public LogChangeRepository()
        {
            _changeLogDataSource = new ChangeLogDataContext();
        }
        public void AddChangeLog(Domain.Classes.Req.Domain.Entities.ChangeLog changeLog)
        {
            //Add the log to the context.
            ((DbSet<ChangeLog>)_changeLogDataSource.ChangeLogs).Add(changeLog);
        }

        public void Save()
        {
            Task.Run(() => {
                _changeLogDataSource.Save();
            });
        }
    }
}
